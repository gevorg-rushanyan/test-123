using System.Collections;
using System.Collections.Generic;
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
        
        private readonly List<BoardItem> _items = new ();

        public void Initialize(int columns, int rows)
        {
            GenerateGrid(columns, rows);
        }

        private void GenerateGrid(int columns, int rows)
        {
            DeleteItems();

            _gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            _gridLayout.constraintCount = columns;

            for (int i = 0; i < rows * columns; i++)
            {
                var item = Instantiate(_cardPrefab, _gridLayout.transform);
                item.gameObject.SetActive(true);
                _items.Add(item);
            }
            StartCoroutine(UpdateCellSizeCoroutine(columns, rows));
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
                Destroy(item.gameObject);
            }
            _items.Clear();
        }
    }
}