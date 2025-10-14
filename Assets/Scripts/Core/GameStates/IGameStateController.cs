namespace Core.GameStates
{
    public enum GameState
    {
        MainMenu,
        MatchCards
    }
    
    public interface IGameStateController
    {
        void SetState(GameState state);
    }
}