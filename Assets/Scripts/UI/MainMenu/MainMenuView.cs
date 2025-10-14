using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MainMenu
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private Button _playButton;

        public Action OnClickPlay;

        private void Start()
        {
            _playButton.onClick.AddListener(() => OnClickPlay?.Invoke());
        }
    }
}