using System;
using System.Collections;
using System.Collections.Generic;
using Board;
using Providers;
using UnityEngine;

namespace BoardModule
{
    public interface IGridController
    {
        public void GenerateGrid(int columns, int rows, IReadOnlyDictionary<Vector2Int, BoardItemData> itemsMapping,
            string coverImageName, ISpriteProvider spriteProvider);
        IEnumerator UpdateGridSizeCoroutine();

        void Show(Vector2Int key);
        void Hide(Vector2Int key);
        void ShowAll();
        void HideAll();
        void MarkAsMatched(List<Vector2Int> items);
        
        Action<Vector2Int> OnItemSelected { get; set; }
    }
}