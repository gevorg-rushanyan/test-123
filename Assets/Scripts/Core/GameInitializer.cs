using Core.GameStates;
using UI;
using UnityEngine;

namespace Core
{
    public class GameInitializer : MonoBehaviour
    {
        [SerializeField] private UiManager _uiManager;
        private GameStateController _gameStateController;
        
        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            _uiManager.gameObject.SetActive(true);
            
            _gameStateController = new GameStateController(_uiManager);
            _gameStateController.SetState(GameState.MainMenu);
            
            _uiManager.gameObject.SetActive(false);
        }
    }
}
