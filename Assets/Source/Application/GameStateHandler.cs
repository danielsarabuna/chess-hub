using ChessHub.Domain;

namespace ChessHub.Application
{
    public class GameStateHandler
    {
        private ChessBoard _chessBoard;

        public GameStateHandler(ChessBoard chessBoard)
        {
            _chessBoard = chessBoard;
        }

        public bool IsGameOver(out PlayerColor winner)
        {
            winner = PlayerColor.White;

            bool whiteKingAlive = false;
            bool blackKingAlive = false;

            foreach (var piece in _chessBoard.Board)
            {
                if (piece != null)
                {
                    if (piece.Type == PieceType.King)
                    {
                        if (piece.Color == PlayerColor.White) whiteKingAlive = true;
                        if (piece.Color == PlayerColor.Black) blackKingAlive = true;
                    }
                }
            }

            if (!whiteKingAlive)
            {
                winner = PlayerColor.Black;
                return true;
            }

            if (!blackKingAlive)
            {
                winner = PlayerColor.White;
                return true;
            }

            return false;
        }
    }
}