namespace ChessHub.Domain.Model
{
    public class PlayerModel
    {
        public string Nickname { get; }

        public PlayerModel(string nickname)
        {
            Nickname = nickname;
        }
    }
}