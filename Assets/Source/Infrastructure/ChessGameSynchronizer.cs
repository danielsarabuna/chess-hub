using ChessHub.Domain;
using UnityEngine;

namespace ChessHub.Infrastructure
{
    public class ChessGameSynchronizer
    {
        private readonly ChessNetworkManager _networkManager;
        private readonly ChessBoard _chessBoard;

        public ChessGameSynchronizer(ChessNetworkManager networkManager, ChessBoard chessBoard)
        {
            _networkManager = networkManager;
            _chessBoard = chessBoard;
        }

        public void SendMove(Vector2Int from, Vector2Int to)
        {
            string moveData = JsonUtility.ToJson(new MoveData(from, to));
            Debug.Log($"Sending move: {moveData}");
            _networkManager.SendMove(moveData);

            ApplyMove(from, to);
        }

        private void OnMoveReceived(string moveJson)
        {
            MoveData move = JsonUtility.FromJson<MoveData>(moveJson);
            Debug.Log($"Remote move applied: {move.From} -> {move.To}");

            ApplyMove(move.From, move.To);
        }

        private void ApplyMove(Vector2Int from, Vector2Int to)
        {
            ChessPiece piece = _chessBoard.GetPiece(from);
            if (piece != null && piece.CanMove(to, _chessBoard))
            {
                _chessBoard.PlacePiece(piece, to);
                _chessBoard.RemovePiece(from);
                Debug.Log($"Move applied locally: {from} -> {to}");
            }
            else
            {
                Debug.LogError("Invalid move received");
            }
        }
    }
}