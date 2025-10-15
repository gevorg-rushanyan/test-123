using System;
using System.Collections.Generic;
using Board;
using Providers;

namespace BoardModule
{
    public interface IBoardController
    {
        Action OnItemClicked { get; set; }
        Action<List<PositionInt>> OnItemsMatch { get; set; }
        Action OnMatchFail { get; set;  }

        void Initialize(int columns,
            int rows,
            IReadOnlyDictionary<PositionInt, BoardItemData> itemsMapping,
            ISpriteProvider spriteProvider);
    }
}