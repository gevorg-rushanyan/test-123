using Providers;

namespace UI
{
    public enum ViewType
    {
        MainMenu,
        MatchCards
    }
    
    public interface IUiManager
    {
        void Initialize(IViewProvider viewProvider);
        T ShowView<T>(ViewType viewType) where T : BaseViewController;
        void HideView(BaseViewController viewController);
        void SetLoadingState(bool state);
    }
}