using UnityEngine;

namespace ChessHub.Domain
{
    public class Pawn : ChessPiece
    {
        public Pawn(PlayerColor color, Vector2Int initialPosition)
            : base(PieceType.Pawn, color, initialPosition) { }

        public override bool CanMove(Vector2Int targetPosition, ChessBoard board)
        {
            int direction = (Color == PlayerColor.White) ? 1 : -1; // Белые двигаются вверх, черные вниз

            Vector2Int forwardMove = Position + new Vector2Int(0, direction);
            if (targetPosition == forwardMove && board.GetPiece(targetPosition) == null)
                return true;

            Vector2Int leftCapture = Position + new Vector2Int(-1, direction);
            Vector2Int rightCapture = Position + new Vector2Int(1, direction);
            
            if ((targetPosition == leftCapture || targetPosition == rightCapture) &&
                board.GetPiece(targetPosition)?.Color != Color)
            {
                return true;
            }

            return false;
        }
    }
}