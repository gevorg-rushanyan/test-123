using UI.MainMenu;
using UI.MatchCards;
using UnityEngine;

namespace Containers
{
    [CreateAssetMenu(fileName = "ViewsContainer", menuName = "ScriptableObjects/ViewsContainer")]
    public class ViewContainer : ScriptableObject
    {
        [SerializeField] private MainMenuView _mainMenu;
        [SerializeField] private MatchCardsView _matchCards;
        
        public MainMenuView MainMenu => _mainMenu;
        public MatchCardsView MatchCards => _matchCards;
    }
}