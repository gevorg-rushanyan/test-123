using System;
using UnityEngine;

namespace UI.MatchCards
{
    public class MatchCardsController : BaseViewController
    {
        private MatchCardsView _viewPrefab;
        
        public Action OnClickBack;
        
        public MatchCardsController(MatchCardsView viewPrefab, Transform root) : base(root)
        {
            _viewPrefab = viewPrefab;
        }

        public override void Show()
        {
            var view = base.Show(_viewPrefab);
            view.OnClickBack += () => { OnClickBack?.Invoke(); };
        }
    }
}