using Board;
using UnityEngine;

namespace Providers
{
    public interface ISpriteProvider
    {
        bool TryGetBoardItemSprite(ItemType type, out Sprite sprite);
        Sprite GetSprite(string name);
    }
}