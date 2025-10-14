using Providers;
using UI.MainMenu;
using UnityEngine;

namespace UI
{
    public class UiManager : MonoBehaviour, IUiManager
    {
        [SerializeField] private GameObject _loadingView;
        [SerializeField] private Transform _viewContainer;
        private IViewProvider _viewProvider;
        private BaseViewController _viewController;

        public void Initialize(IViewProvider viewProvider)
        {
            _viewProvider = viewProvider;
        }

        public void SetLoadingState(bool state)
        {
            if (_loadingView.activeSelf == state)
            {
                return;
            }
            
            _loadingView.SetActive(state);
        }

        public T ShowView<T>(ViewType viewType) where T : BaseViewController
        {
            switch (viewType)
            {
                case ViewType.MainMenu:
                    var mainMenuView = _viewProvider.GetView<MainMenuView>();
                    _viewController = new MainMenuController(mainMenuView, _viewContainer);
                    _viewController.Show();
                    return _viewController as T;
                    
                case ViewType.MatchCards:
                    break;
            }

            return null;
        }
        
        public void HideView(BaseViewController viewController)
        {
            viewController?.Hide();
        }
    }
}