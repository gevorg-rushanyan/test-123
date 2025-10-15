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
        [SerializeField] private TMP_Text _scoreCount;
        [SerializeField] private Transform _multiplierContainer;
        [SerializeField] private TMP_Text _scoreMultiplier;
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

        public void UpdateProgress(int matches, int turns, int score, int multiplier)
        {
            _matchesCount.text = matches.ToString();
            _turnsCount.text = turns.ToString();
            _scoreCount.text = score.ToString();
            SetMultiplier(multiplier);
        }

        private void SetMultiplier(int multiplier)
        {
            if (multiplier <= 1)
            {
                _multiplierContainer.gameObject.SetActive(false);
                return;
            }
            _multiplierContainer.gameObject.SetActive(true);
            _scoreMultiplier.text = $"x{multiplier}";
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