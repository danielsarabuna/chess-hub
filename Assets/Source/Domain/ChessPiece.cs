using UnityEngine;

namespace Chess.Domain
{
    public enum PieceType
    {
        Pawn,
        Rook,
        Knight,
        Bishop,
        Queen,
        King
    }

    public enum PlayerColor
    {
        White,
        Black
    }

    public abstract class ChessPiece
    {
        public PieceType Type { get; private set; }
        public PlayerColor Color { get; private set; }
        public Vector2Int Position { get; set; } 

        protected ChessPiece(PieceType type, PlayerColor color, Vector2Int initialPosition)
        {
            Type = type;
            Color = color;
            Position = initialPosition;
        }

        public abstract bool CanMove(Vector2Int targetPosition, ChessBoard board);
    }
}