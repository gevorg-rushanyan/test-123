using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MatchCards
{
    public class MatchCardsView : MonoBehaviour
    {
        [SerializeField] private Button _backButton;
        
        public Action OnClickBack;

        private void Start()
        {
            _backButton.onClick.AddListener(() => OnClickBack?.Invoke());
        }
    }
}