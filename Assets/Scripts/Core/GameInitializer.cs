using UI;
using UnityEngine;

namespace Core
{
    public class GameInitializer : MonoBehaviour
    {
        [SerializeField] private UiManager _uiManager;
        
        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            _uiManager.gameObject.SetActive(true);
        }
    }
}
