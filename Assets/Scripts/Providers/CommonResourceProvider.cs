using Board;
using Containers;
using UnityEngine;

namespace Providers
{
    public class CommonResourceProvider : ISpriteProvider
    {
        private const string Path = "Containers/CommonResourceContainer";
        private CommonResourceContainer _commonResourceContainer;
        
        public void Initialize()
        {
            _commonResourceContainer = Resources.Load<CommonResourceContainer>(Path);
        }

        public bool TryGetBoardItemSprite(ItemType type, out Sprite sprite)
        {
            string name = ItemTypeToString(type);
            var result = _commonResourceContainer.BoardAtlas.GetSprite(name);

            sprite = result;
            
            return result != null;
        }

        public Sprite GetSprite(string name)
        {
            return _commonResourceContainer.BoardAtlas.GetSprite(name);
        }

        private string ItemTypeToString(ItemType type)
        {
            return type switch
            {
                ItemType.Alchemy => "Alchemy",
                ItemType.Curse => "Curse",
                ItemType.FireRain => "FireRain",
                ItemType.IceNova => "IceNova",
                ItemType.LightningBolt => "LightningBolt",
                ItemType.Warrior => "Warrior",
                _ => string.Empty
            };
        }
    }
}