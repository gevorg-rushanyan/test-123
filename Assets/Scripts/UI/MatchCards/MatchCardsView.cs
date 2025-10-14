using System;
using BoardModule;
using Containers;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MatchCards
{
    public class MatchCardsView : MonoBehaviour
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private BoardController _boardController;
        
        public Action OnClickBack;
        public BoardController BoardController => _boardController;

        private void Start()
        {
            _backButton.onClick.AddListener(() => OnClickBack?.Invoke());
        }
    }
}