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

        private GridController _gridController;
        private ISpriteProvider _spriteProvider;

        public void Initialize(int columns, int rows, IReadOnlyDictionary<Vector2Int, BoardItemData> itemsMapping, ISpriteProvider spriteProvider)
        {
            _spriteProvider = spriteProvider;
            if (_gridController == null)
            {
                _gridController = new GridController(_container, _gridLayout, _cardPrefab, _spacing);
            }
            _gridController.GenerateGrid(columns, rows, itemsMapping, DefaultCoverImageName, _spriteProvider);
            StartCoroutine(_gridController.UpdateGridSizeCoroutine());
            StartCoroutine(PlayStartAnimation());
        }

        private IEnumerator PlayStartAnimation()
        {
            _gridController.ShowAll();
            yield return new WaitForSeconds(1.5f);
            _gridController.HideAll();
        }
    }
}