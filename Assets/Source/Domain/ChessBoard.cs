using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ChessHub.Domain
{
    public class ChessBoard
    {
        public Vector2Int BoardSize;
        public ChessPiece[,] Board { get; private set; }

        public ChessBoard(Vector2Int boardSize)
        {
            BoardSize = boardSize;
            Board = new ChessPiece[BoardSize.x, BoardSize.y];
        }

        public void InitializeBoard()
        {
            for (var i = 0; i < BoardSize.x; i++)
            {
                PlacePiece(new Pawn(PlayerColor.White, new Vector2Int(i, 1)), new Vector2Int(i, 1));
                PlacePiece(new Pawn(PlayerColor.Black, new Vector2Int(i, 6)), new Vector2Int(i, 6));
            }

            PlacePiece(new Rook(PlayerColor.White, new Vector2Int(0, 0)), new Vector2Int(0, 0));
            PlacePiece(new Knight(PlayerColor.White, new Vector2Int(1, 0)), new Vector2Int(1, 0));
            PlacePiece(new Bishop(PlayerColor.White, new Vector2Int(2, 0)), new Vector2Int(2, 0));
            PlacePiece(new Queen(PlayerColor.White, new Vector2Int(3, 0)), new Vector2Int(3, 0));
            PlacePiece(new King(PlayerColor.White, new Vector2Int(4, 0)), new Vector2Int(4, 0));
            PlacePiece(new Bishop(PlayerColor.White, new Vector2Int(5, 0)), new Vector2Int(5, 0));
            PlacePiece(new Knight(PlayerColor.White, new Vector2Int(6, 0)), new Vector2Int(6, 0));
            PlacePiece(new Rook(PlayerColor.White, new Vector2Int(7, 0)), new Vector2Int(7, 0));

            PlacePiece(new Rook(PlayerColor.Black, new Vector2Int(0, 7)), new Vector2Int(0, 7));
            PlacePiece(new Knight(PlayerColor.Black, new Vector2Int(1, 7)), new Vector2Int(1, 7));
            PlacePiece(new Bishop(PlayerColor.Black, new Vector2Int(2, 7)), new Vector2Int(2, 7));
            PlacePiece(new Queen(PlayerColor.Black, new Vector2Int(3, 7)), new Vector2Int(3, 7));
            PlacePiece(new King(PlayerColor.Black, new Vector2Int(4, 7)), new Vector2Int(4, 7));
            PlacePiece(new Bishop(PlayerColor.Black, new Vector2Int(5, 7)), new Vector2Int(5, 7));
            PlacePiece(new Knight(PlayerColor.Black, new Vector2Int(6, 7)), new Vector2Int(6, 7));
            PlacePiece(new Rook(PlayerColor.Black, new Vector2Int(7, 7)), new Vector2Int(7, 7));
        }

        public void PlacePiece(ChessPiece piece, Vector2Int position)
        {
            Board[position.x, position.y] = piece;
            piece.Position = position;
        }

        public bool IsInsideBounds(Vector2Int position)
        {
            return position.x >= 0 && position.x < BoardSize.x && position.y >= 0 && position.y < BoardSize.y;
        }

        public ChessPiece GetPiece(Vector2Int position)
        {
            return !IsInsideBounds(position) ? null : Board[position.x, position.y];
        }

        public void RemovePiece(Vector2Int position)
        {
            if (IsInsideBounds(position))
            {
                Board[position.x, position.y] = null;
            }
        }

        public ChessPiece FindKing(PlayerColor color)
        {
            return Board.Cast<ChessPiece>()
                .FirstOrDefault(piece => piece is { Type: PieceType.King } && piece.Color == color);
        }

        public IList<ChessPiece> GetPieces()
        {
            return Board.Cast<ChessPiece>().Where(piece => piece != null).ToList();
        }
    }
}