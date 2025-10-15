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
        public void GenerateGrid(int columns, int rows, IReadOnlyDictionary<PositionInt, BoardItemData> itemsMapping,
            string coverImageName, ISpriteProvider spriteProvider);
        IEnumerator UpdateGridSizeCoroutine();

        void Show(PositionInt key);
        void Hide(PositionInt key);
        void ShowAll();
        void HideAll();
        void MarkAsMatched(List<PositionInt> items);
        
        Action<PositionInt> OnItemSelected { get; set; }
    }
}