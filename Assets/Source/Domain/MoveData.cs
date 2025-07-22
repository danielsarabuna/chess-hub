using UnityEngine;

namespace ChessHub.Domain
{
    [System.Serializable]
    public struct MoveData
    {
        public Vector2Int From { get; }
        public Vector2Int To { get; }

        public MoveData(Vector2Int from, Vector2Int to)
        {
            ValidatePosition(from);
            ValidatePosition(to);
            From = from;
            To = to;
        }

        private static void ValidatePosition(Vector2Int position)
        {
            if (position.x < 0 || position.x >= 8 || position.y < 0 || position.y >= 8)
            {
                throw new System.ArgumentOutOfRangeException(
                    $"Position {position} is out of bounds of a standard chessboard (0-7).");
            }
        }

        public override string ToString()
        {
            return $"Move: {From} -> {To}";
        }
    }
}