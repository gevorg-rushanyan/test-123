using System.Collections.Generic;
using Board;
using Containers;
using UnityEngine;

namespace Providers
{
    public class CommonResourceProvider : ISpriteProvider, ISoundsProvider
    {
        private const string Path = "Containers/CommonResourceContainer";
        private CommonResourceContainer _commonResourceContainer;
        private Dictionary<SoundType, AudioClip> _audioClips;
        
        public AudioSource AudioSourcePrefab => _commonResourceContainer.AudioSourcePrefab;
        
        public void Initialize()
        {
            _commonResourceContainer = Resources.Load<CommonResourceContainer>(Path);
            _audioClips = new Dictionary<SoundType, AudioClip>
            {
                {SoundType.Flip, _commonResourceContainer.FlipSound},
                {SoundType.Match, _commonResourceContainer.MatchSound},
                {SoundType.Win, _commonResourceContainer.WinSound},
                {SoundType.Lose, _commonResourceContainer.LoseSound}
            };
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

        public AudioClip GetSound(SoundType type)
        {
            return _audioClips.GetValueOrDefault(type);
        }
    }
}