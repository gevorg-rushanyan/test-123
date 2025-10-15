using Providers;

namespace Core.Sound
{
    public interface ISoundSystem
    {
        void PlaySound(SoundType soundType);
        void StopAllSounds();
    }
}