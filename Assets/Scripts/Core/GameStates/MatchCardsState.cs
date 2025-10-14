using UI;

namespace Core.GameStates
{
    public class MatchCardsState : IGameState
    {
        private readonly IUiManager _uiManager;
        
        public MatchCardsState(IUiManager uiManager)
        {
            _uiManager = uiManager;
        }

        public void Start()
        {
            
        }

        public void End()
        {
            
        }
    }
}