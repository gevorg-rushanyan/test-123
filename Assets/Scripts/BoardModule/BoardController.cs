using System.Collections;
using System.Collections.Generic;
using Board;
using Providers;
using UnityEngine;
using UnityEngine.UI;

namespace BoardModule
{
    public class BoardController : MonoBehaviour
    {
        [SerializeField] private RectTransform _container;
        [SerializeField] private GridLayoutGroup _gridLayout;
        [SerializeField] private BoardItem _cardPrefab;
        [SerializeField] private Vector2 _spacing = new Vector2(10, 10);

        private ICommonResourceProvider _resourceProvider;
        private Dictionary<Vector2Int, BoardItem> _items = new ();

        public void Initialize(
            int columns,
            int rows,
            IReadOnlyDictionary<Vector2Int, BoardItemData> itemsMapping,
            ICommonResourceProvider commonResourceProvider)
        {
            _resourceProvider = commonResourceProvider;
            GenerateGrid(columns, rows, itemsMapping);
        }

        private void GenerateGrid(int columns, int rows, IReadOnlyDictionary<Vector2Int, BoardItemData> itemsMapping)
        {
            DeleteItems();

            _gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            _gridLayout.constraintCount = columns;

            for (int i = 0; i < columns; ++i)
            {
                for (int j = 0; j < rows; j++)
                {
                    var key = new Vector2Int(i, j);
                    var item = CreateItem(key, itemsMapping);
                    if (!_items.TryAdd(key, item))
                    {
                        Debug.LogError($"Duplicate Key x: {key.x} y: {key.y}");
                    }
                }
            }
            StartCoroutine(UpdateCellSizeCoroutine(columns, rows));
        }

        private BoardItem CreateItem(Vector2Int key, IReadOnlyDictionary<Vector2Int, BoardItemData> itemsMapping)
        {
            var item = Instantiate(_cardPrefab, _gridLayout.transform);
            if (itemsMapping.TryGetValue(key, out var itemData))
            {
                _resourceProvider.TryGetBoardItemSprite(itemData.Type, out Sprite sprite);
                item.Initialize(key, sprite);
            }
            else
            {
                item.Initialize();
            }
            item.gameObject.SetActive(true);
            item.OnClick += OnItemClicked;

            return item;
        }

        private void OnItemClicked(Vector2Int key)
        {
        }

        private IEnumerator UpdateCellSizeCoroutine(int columns, int rows)
        {
            yield return null;
            UpdateCellSize(columns, rows);
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
                Destroy(item.Value.gameObject);
            }
            _items.Clear();
        }
    }
}