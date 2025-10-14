using System;
using UnityEngine;
using UnityEngine.UI;

namespace BoardModule
{
    public class BoardItem : MonoBehaviour
    {
        [SerializeField] private Image _coverImage;
        [SerializeField] private Image _itemImage;
        [SerializeField] private Button _button;
        private Vector2Int _key;
        
        public Action<Vector2Int> OnClick;
        
        public void Initialize(Vector2Int key, Sprite itemImage)
        {
            _key = key;
            _itemImage.sprite = itemImage;
            _button.onClick.AddListener(OnClicked);
            
            _itemImage.gameObject.SetActive(false);
            _coverImage.gameObject.SetActive(true);
        }

        public void Initialize()
        {
            _itemImage.gameObject.SetActive(false);
            _coverImage.gameObject.SetActive(false);
        }

        private void OnClicked()
        {
            _itemImage.gameObject.SetActive(true);
            _coverImage.gameObject.SetActive(false);
            OnClick?.Invoke(_key);
        }
    }
}