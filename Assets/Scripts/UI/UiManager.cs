using UnityEngine;

namespace UI
{
    public class UiManager : MonoBehaviour, IUiManager
    {
        [SerializeField] private GameObject _loadingView;
        private void Start()
        {
        
        }

        public void SetLoadingState(bool state)
        {
            if (_loadingView.activeSelf == state)
            {
                return;
            }
            
            _loadingView.SetActive(state);
        }

        public void ShowView()
        {
            
        }
    }
}