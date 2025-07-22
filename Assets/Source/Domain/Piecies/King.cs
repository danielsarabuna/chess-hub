using UnityEngine;

namespace ChessHub.Domain
{
    public class King : ChessPiece
    {
        public King(PlayerColor color, Vector2Int initialPosition)
            : base(PieceType.King, color, initialPosition)
        {
        }

        public override bool CanMove(Vector2Int targetPosition, ChessBoard board)
        {
            var deltaX = Mathf.Abs(targetPosition.x - Position.x);
            var deltaY = Mathf.Abs(targetPosition.y - Position.y);

            if (deltaX > 1 || deltaY > 1)
                return false;

            var targetPiece = board.GetPiece(targetPosition);
            return targetPiece == null || targetPiece.Color != Color;
        }
    }
}