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
        private const string DefaultCoverImageName = "Cover";
        [SerializeField] private RectTransform _container;
        [SerializeField] private GridLayoutGroup _gridLayout;
        [SerializeField] private BoardItem _cardPrefab;
        [SerializeField] private Vector2 _spacing = new Vector2(10, 10);

        private readonly Queue<BoardItemData> _selectedItems = new ();
        private readonly HashSet<Vector2Int> _selectedItemsKey = new();
        private IGridController _gridController;
        private ISpriteProvider _spriteProvider;
        private IReadOnlyDictionary<Vector2Int, BoardItemData> _items;
        private bool _isMatchCoroutineRunning;

        public void Initialize(int columns, int rows, IReadOnlyDictionary<Vector2Int, BoardItemData> itemsMapping, ISpriteProvider spriteProvider)
        {
            _items = itemsMapping;
            _spriteProvider = spriteProvider;
            _selectedItems.Clear();
            _selectedItemsKey.Clear();
            if (_gridController == null)
            {
                _gridController = new GridController(_container, _gridLayout, _cardPrefab, _spacing);
            }
            _gridController.GenerateGrid(columns, rows, itemsMapping, DefaultCoverImageName, _spriteProvider);
            _gridController.OnItemSelected += OnItemSelected;
            StartCoroutine(_gridController.UpdateGridSizeCoroutine());
            StartCoroutine(PlayStartAnimation());
            
        }

        private IEnumerator PlayStartAnimation()
        {
            _gridController.ShowAll();
            yield return new WaitForSeconds(1.2f);
            _gridController.HideAll();
        }

        private void OnItemSelected(Vector2Int key)
        {
            if (!_selectedItemsKey.Add(key))
            {
                return;
            }

            var selectedItem = _items[key];
            _selectedItems.Enqueue(selectedItem);
            _gridController.Show(key);

            if (_selectedItems.Count > 1 && !_isMatchCoroutineRunning)
            {
                StartCoroutine(MatchLogicCoroutine());
            }
        }

        private IEnumerator MatchLogicCoroutine()
        {
            _isMatchCoroutineRunning = true;
            while (_selectedItems.Count >= 2)
            {
                yield return new WaitForSeconds(0.6f);
                while (_selectedItems.Count >= 2)
                {
                    var item1 = _selectedItems.Dequeue();
                    var item2 = _selectedItems.Dequeue();
                    _selectedItemsKey.Remove(item1.Position);
                    _selectedItemsKey.Remove(item2.Position);
                    if (item1.Type == item2.Type)
                    {
                        List<Vector2Int> matchedItems = new List<Vector2Int> { item1.Position, item2.Position };
                        yield return new WaitForSeconds(0.1f);
                        _gridController.MarkAsMatched(matchedItems);
                    }
                    else
                    {
                        _gridController.Hide(item1.Position);
                        _gridController.Hide(item2.Position);
                        Debug.Log("Not Matched");
                    }
                }
            }
            _isMatchCoroutineRunning = false;
        }
    }
}