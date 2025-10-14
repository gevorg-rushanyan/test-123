using Board;
using UnityEngine;

namespace Providers
{
    public interface ICommonResourceProvider
    {
        void Initialize();
        bool TryGetBoardItemSprite(ItemType type, out Sprite sprite);
        Sprite GetSprite(string name);
    }
}