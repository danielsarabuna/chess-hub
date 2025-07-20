using UnityEngine;

namespace Chess.Domain
{
    public class Rook : ChessPiece
    {
        public Rook(PlayerColor color, Vector2Int initialPosition)
            : base(PieceType.Rook, color, initialPosition)
        {
        }

        public override bool CanMove(Vector2Int targetPosition, ChessBoard board)
        {
            if (targetPosition.x != Position.x && targetPosition.y != Position.y)
                return false;

            var direction = targetPosition.x == Position.x
                ? new Vector2Int(0, targetPosition.y > Position.y ? 1 : -1)
                : new Vector2Int(targetPosition.x > Position.x ? 1 : -1, 0);

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