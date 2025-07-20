using Chess.Domain;

namespace Chess.Application
{
    public class PlayerTurnManager
    {
        public PlayerColor CurrentTurn { get; private set; } = PlayerColor.White;

        public bool IsCurrentPlayerTurn(PlayerColor color) => color == CurrentTurn;

        public void SwitchTurn()
        {
            CurrentTurn = CurrentTurn == PlayerColor.White ? PlayerColor.Black : PlayerColor.White;
        }
    }
}