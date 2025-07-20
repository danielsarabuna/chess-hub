using Chess.Domain;
using MessagePipe;
using UnityEngine;

namespace Chess.Application
{
    public class GameManager
    {
        private readonly ChessBoard _chessBoard;
        private readonly MoveValidator _moveValidator;
        private readonly PlayerTurnManager _turnManager;
        private readonly IPublisher<string> _eventPublisher;

        public GameManager(ChessBoard chessBoard, MoveValidator moveValidator, PlayerTurnManager turnManager,
            IPublisher<string> eventPublisher)
        {
            _chessBoard = chessBoard;
            _moveValidator = moveValidator;
            _turnManager = turnManager;
            _eventPublisher = eventPublisher;

            _chessBoard.InitializeBoard();
        }

        public void OnPlayerMove(Vector2Int from, Vector2Int to)
        {
            var piece = _chessBoard.GetPiece(from);

            if (piece == null || !_turnManager.IsCurrentPlayerTurn(piece.Color) ||
                !_moveValidator.ValidateMove(piece, from, to, _chessBoard))
            {
                _eventPublisher.Publish("Invalid move!");
                return;
            }

            _chessBoard.PlacePiece(piece, to);
            _chessBoard.RemovePiece(from);

            var opponentColor = piece.Color == PlayerColor.White ? PlayerColor.Black : PlayerColor.White;
            if (_moveValidator.IsKingInCheck(opponentColor, _chessBoard))
            {
                _eventPublisher.Publish("Check!");

                if (_moveValidator.IsMate(opponentColor, _chessBoard))
                {
                    _eventPublisher.Publish($"{piece.Color} wins with a checkmate!");
                    return;
                }
            }

            _turnManager.SwitchTurn();
        }
    }
}