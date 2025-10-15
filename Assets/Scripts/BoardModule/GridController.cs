using System;
using System.Collections;
using System.Collections.Generic;
using Board;
using Providers;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace BoardModule
{
    public class GridController : IGridController
    {
        private readonly Dictionary<PositionInt, BoardItem> _items;
        private RectTransform _container;
        private GridLayoutGroup _gridLayout;
        private BoardItem _cardPrefab;
        private Vector2 _spacing;
        private ISpriteProvider _spriteProvider;
        private int _columns;
        private int _rows;
        
        public Action<PositionInt> OnItemSelected { get; set; }

        public GridController(RectTransform container, GridLayoutGroup gridLayout, BoardItem cardPrefab, Vector2 spacing)
        {
            _items = new Dictionary<PositionInt, BoardItem>();
            
            _container = container;
            _gridLayout = gridLayout;
            _cardPrefab = cardPrefab;
            _spacing = spacing;
        }

        public void GenerateGrid(int columns, int rows, IReadOnlyDictionary<PositionInt, BoardItemData> itemsMapping, string coverImageName, ISpriteProvider spriteProvider)
        {
            _columns = columns;
            _rows = rows;
            _spriteProvider = spriteProvider;
            DeleteItems();

            _gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            _gridLayout.constraintCount = columns;
            var coverImage = _spriteProvider.GetSprite(coverImageName);

            for (int i = 0; i < columns; ++i)
            {
                for (int j = 0; j < rows; ++j)
                {
                    var key = new PositionInt(i, j);
                    var item = CreateItem(key, itemsMapping, coverImage);
                    if (!_items.TryAdd(key, item))
                    {
                        Debug.LogError($"Duplicate Key x: {key.X} y: {key.Y}");
                    }
                }
            }
        }
        
        private BoardItem CreateItem(PositionInt key, IReadOnlyDictionary<PositionInt, BoardItemData> itemsMapping, Sprite coverImage = null)
        {
            var item = Object.Instantiate(_cardPrefab, _gridLayout.transform);
            if (itemsMapping.TryGetValue(key, out var itemData))
            {
                _spriteProvider.TryGetBoardItemSprite(itemData.Type, out Sprite sprite);
                item.Initialize(itemData.Type, key, sprite, coverImage);
            }
            else
            {
                item.Initialize();
            }
            item.gameObject.SetActive(true);
            item.OnClick += selectedItemKey => { OnItemSelected?.Invoke(selectedItemKey); };

            return item;
        }
        
        public IEnumerator UpdateGridSizeCoroutine()
        {
            yield return null;
            UpdateCellSize(_columns, _rows);
        }

        public void Show(PositionInt key)
        {
            if (_items.TryGetValue(key, out var item))
            {
                item.Show();
            }
        }
        
        public void Hide(PositionInt key)
        {
            if (_items.TryGetValue(key, out var item))
            {
                item.Hide();
            }
        }

        public void ShowAll()
        {
            foreach (var itemData in _items.Values)
            {
                itemData.Show();
            }
        }
        
        public void HideAll()
        {
            foreach (var itemData in _items.Values)
            {
                itemData.Hide();
            }
        }

        private void UpdateCellSize(int columns, int rows)
        {
            var rect = _container.rect;

            float totalSpacingX = _spacing.x * (columns - 1);
            float totalSpacingY = _spacing.y * (rows - 1);

            float cellWidth = (rect.width - totalSpacingX - _gridLayout.padding.left - _gridLayout.padding.right) / columns;
            float cellHeight = (rect.height - totalSpacingY - _gridLayout.padding.top - _gridLayout.padding.bottom) / rows;

            _gridLayout.cellSize = new Vector2(cellWidth, cellHeight);
        }
        
        private void DeleteItems()
        {
            foreach (var item in _items)
            {
                Object.Destroy(item.Value.gameObject);
            }
            _items.Clear();
        }
        
        public void MarkAsMatched(List<PositionInt> items)
        {
            foreach (var itemKey in items)
            {
                if (_items.TryGetValue(itemKey, out var item))
                {
                    item.Matched();
                }
            }
        }
    }
}