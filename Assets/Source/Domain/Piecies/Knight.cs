using UnityEngine;

namespace Chess.Domain
{
    public class Knight : ChessPiece
    {
        public Knight(PlayerColor color, Vector2Int initialPosition)
            : base(PieceType.Knight, color, initialPosition)
        {
        }

        public override bool CanMove(Vector2Int targetPosition, ChessBoard board)
        {
            var deltaX = Mathf.Abs(targetPosition.x - Position.x);
            var deltaY = Mathf.Abs(targetPosition.y - Position.y);

            if (!((deltaX == 2 && deltaY == 1) || (deltaX == 1 && deltaY == 2)))
                return false;

            var targetPiece = board.GetPiece(targetPosition);
            return targetPiece == null || targetPiece.Color != Color;
        }
    }
}