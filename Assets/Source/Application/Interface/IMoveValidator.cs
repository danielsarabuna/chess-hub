using ChessHub.Domain;
using UnityEngine;

namespace ChessHub.Application
{
    public interface IMoveValidator
    {
        bool ValidateMove(ChessPiece piece, Vector2Int from, Vector2Int to, ChessBoard board);
        bool IsMate(PlayerColor kingColor, ChessBoard board);
        bool WouldKingBeInCheck(ChessPiece piece, Vector2Int from, Vector2Int to, ChessBoard board);
        bool IsKingInCheck(PlayerColor kingColor, ChessBoard board);
    }
}