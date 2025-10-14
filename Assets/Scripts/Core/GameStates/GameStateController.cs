namespace Core.GameStates
{
    public class GameStateController : IGameStateController
    {
        private IGameState _gameState;
        
        public void SetState(GameState state)
        {
            switch (state)
            {
                case GameState.MainMenu:
                    StartMainMenuState();
                    break;
                case GameState.MatchCards:
                    StartCardMatchState();
                    break;
            }
        }

        private void StartMainMenuState()
        {
            _gameState?.End();
            _gameState = new MainMenuState();
            _gameState.Start();
        }
        
        private void StartCardMatchState()
        {
        }
    }
}