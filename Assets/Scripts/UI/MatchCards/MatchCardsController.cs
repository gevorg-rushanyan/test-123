using System;
using BoardModule;
using UnityEngine;

namespace UI.MatchCards
{
    public class MatchCardsController : BaseViewController
    {
        private MatchCardsView _viewPrefab;
        private BoardController _boardController;
        private MatchCardsView _view;
        
        public Action OnClickBack;
        public Action OnWinOrLoseClick;
        public BoardController BoardController => _boardController;
        
        public MatchCardsController(MatchCardsView viewPrefab, Transform root) : base(root)
        {
            _viewPrefab = viewPrefab;
        }

        public override void Show()
        {
            _view = base.Show(_viewPrefab);
            _boardController = _view.BoardController;
            _view.OnClickBack += () => { OnClickBack?.Invoke(); };
            _view.OnWinOrLoseClick += () => { OnWinOrLoseClick?.Invoke(); };
        }

        public void UpdateProgress(int matches, int turns)
        {
            _view.UpdateProgress(matches, turns);
        }

        public void ShowWinOrLoseView(bool win)
        {
            _view.ShowWinOrLoseView(win);
        }

        public void HideWinOrLoseView()
        {
            _view.HideWinOrLoseView();
        }
    }
}