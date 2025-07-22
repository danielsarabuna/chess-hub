using ChessHub.Domain;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ChessHub.Application
{
    public class MoveValidator : IMoveValidator
    {
        public bool ValidateMove(ChessPiece piece, Vector2Int from, Vector2Int to, ChessBoard board)
        {
            if (!piece.CanMove(to, board)) return false;
            return !WouldKingBeInCheck(piece, from, to, board);
        }

        public bool WouldKingBeInCheck(ChessPiece piece, Vector2Int from, Vector2Int to, ChessBoard board)
        {
            var capturedPiece = board.GetPiece(to);

            board.PlacePiece(piece, to);
            board.RemovePiece(from);

            var isInCheck = IsKingInCheck(piece.Color, board);

            board.PlacePiece(piece, from);
            if (capturedPiece != null)
                board.PlacePiece(capturedPiece, to);

            return isInCheck;
        }

        public bool IsKingInCheck(PlayerColor kingColor, ChessBoard board)
        {
            var king = board.FindKing(kingColor);
            return king != null && board.GetPieces()
                .Any(piece => piece.Color != kingColor && piece.CanMove(king.Position, board));
        }

        public bool IsMate(PlayerColor kingColor, ChessBoard board)
        {
            return !(from piece in board.GetPieces()
                where piece.Color == kingColor
                from move in GetPossibleMoves(piece, board)
                where !WouldKingBeInCheck(piece, piece.Position, move, board)
                select piece).Any();
        }

        private IList<Vector2Int> GetPossibleMoves(ChessPiece piece, ChessBoard board)
        {
            var moves = new List<Vector2Int>();

            for (var x = 0; x < board.BoardSize.x; x++)
            {
                for (var y = 0; y < board.BoardSize.y; y++)
                {
                    var position = new Vector2Int(x, y);
                    if (piece.CanMove(position, board)) moves.Add(position);
                }
            }

            return moves;
        }
    }
}