using UnityEngine;

namespace Chess.Domain
{
    public class Bishop : ChessPiece
    {
        public Bishop(PlayerColor color, Vector2Int initialPosition)
            : base(PieceType.Bishop, color, initialPosition)
        {
        }

        public override bool CanMove(Vector2Int targetPosition, ChessBoard board)
        {
            var deltaX = Mathf.Abs(targetPosition.x - Position.x);
            var deltaY = Mathf.Abs(targetPosition.y - Position.y);
            if (deltaX != deltaY)
                return false;

            var direction = new Vector2Int(
                targetPosition.x > Position.x ? 1 : -1,
                targetPosition.y > Position.y ? 1 : -1
            );

            var current = Position + direction;
            while (current != targetPosition)
            {
                if (board.GetPiece(current) != null)
                    return false;
                current += direction;
            }

            var targetPiece = board.GetPiece(targetPosition);
            return targetPiece == null || targetPiece.Color != Color;
        }
    }
}