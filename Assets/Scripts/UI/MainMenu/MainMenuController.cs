using UnityEngine;

namespace UI.MainMenu
{
    public class MainMenuController : BaseViewController
    {
        private MainMenuView _viewPrefab;
        
        public MainMenuController(MainMenuView viewPrefab, Transform root) : base(root)
        {
            _viewPrefab = viewPrefab;
        }

        public override void Show()
        {
            var view = base.Show(_viewPrefab);    
        }
    }
}