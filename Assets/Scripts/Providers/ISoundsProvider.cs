using UnityEngine;

namespace Providers
{
    public enum SoundType
    {
        Flip,
        Match,
        Win,
        Lose
    }
    
    public interface ISoundsProvider
    {
        AudioSource AudioSourcePrefab { get; }
        AudioClip GetSound(SoundType type);
    }
}