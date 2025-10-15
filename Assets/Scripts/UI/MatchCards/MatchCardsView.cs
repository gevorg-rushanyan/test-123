using System;
using BoardModule;
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
        [Header("Win or Lose")]
        [SerializeField] private GameObject _winOrLoseContainer;
        [SerializeField] private TMP_Text _winOrLoseText;
        [SerializeField] private Button _winOrLoseButton;
        [SerializeField] private TMP_Text _winOrLoseButtonText;
        
        public Action OnClickBack;
        public Action OnWinOrLoseClick;
        public BoardController BoardController => _boardController;

        private void Start()
        {
            HideWinOrLoseView();
            _backButton.onClick.AddListener(() => OnClickBack?.Invoke());
            _winOrLoseButton.onClick.AddListener(() => OnWinOrLoseClick?.Invoke());
        }

        public void UpdateProgress(int matches, int turns)
        {
            _matchesCount.text = matches.ToString();
            _turnsCount.text = turns.ToString();
        }

        public void ShowWinOrLoseView(bool win)
        {
            _winOrLoseContainer.SetActive(true);
            _winOrLoseButtonText.text = win ? "Next Level" : "Try Again";
            _winOrLoseText.text = win ? "You Win!!!!" : "Too Many Turns\nGame Over :(";
        }

        public void HideWinOrLoseView()
        {
            _winOrLoseContainer.SetActive(false);
        }
    }
}