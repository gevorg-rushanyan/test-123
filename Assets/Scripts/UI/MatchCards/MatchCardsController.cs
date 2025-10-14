using System;
using BoardModule;
using UnityEngine;

namespace UI.MatchCards
{
    public class MatchCardsController : BaseViewController
    {
        private MatchCardsView _viewPrefab;
        private BoardController _boardController;
        
        public Action OnClickBack;
        public BoardController BoardController => _boardController;
        
        public MatchCardsController(MatchCardsView viewPrefab, Transform root) : base(root)
        {
            _viewPrefab = viewPrefab;
        }

        public override void Show()
        {
            var view = base.Show(_viewPrefab);
            _boardController = view.BoardController;
            view.OnClickBack += () => { OnClickBack?.Invoke(); };
        }
    }
}