using UI.MainMenu;
using UnityEngine;

namespace Containers
{
    [CreateAssetMenu(fileName = "ViewsContainer", menuName = "ScriptableObjects/ViewsContainer")]
    public class ViewContainer : ScriptableObject
    {
        [SerializeField] private MainMenuView _mainMenu;
        
        public MainMenuView MainMenu => _mainMenu;
    }
}