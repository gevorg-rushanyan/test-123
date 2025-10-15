using System;
using Board;
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
        private ItemType _type;
        
        public Action<Vector2Int> OnClick;
        
        public void Initialize(ItemType type, Vector2Int key, Sprite itemImage, Sprite coverImage)
        {
            _type = type;
            if (_type == ItemType.None)
            {
                Initialize();
                return;
            }

            _key = key;
            _itemImage.sprite = itemImage;
            _coverImage.sprite = coverImage;
            _button.onClick.AddListener(OnClicked);
            Hide();
        }

        public void Initialize()
        {
            _itemImage.gameObject.SetActive(false);
            _coverImage.gameObject.SetActive(false);
            _type = ItemType.None;
        }

        private void OnClicked()
        {
            OnClick?.Invoke(_key);
        }

        public void Show()
        {
            if (_type == ItemType.None)
            {
                return;
            }
            
            _itemImage.gameObject.SetActive(true);
            _coverImage.gameObject.SetActive(false);
        }

        public void Hide()
        {
            if (_type == ItemType.None)
            {
                return;
            }
            
            _itemImage.gameObject.SetActive(false);
            _coverImage.gameObject.SetActive(true);
        }

        public void Matched()
        {
            _type = ItemType.None;
            _itemImage.gameObject.SetActive(false);
            _coverImage.gameObject.SetActive(false);
        }
    }
}