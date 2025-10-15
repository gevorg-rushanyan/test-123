using System;
using BoardModule;
using Containers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MatchCards
{
    public class MatchCardsView : MonoBehaviour
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private BoardController _boardController;
        [SerializeField] private TMP_Text _matchesCount;
        [SerializeField] private TMP_Text _turnsCount;
        
        public Action OnClickBack;
        public BoardController BoardController => _boardController;

        private void Start()
        {
            _backButton.onClick.AddListener(() => OnClickBack?.Invoke());
        }

        public void UpdateProgress(int matches, int turns)
        {
            _matchesCount.text = matches.ToString();
            _turnsCount.text = turns.ToString();
        }
    }
}