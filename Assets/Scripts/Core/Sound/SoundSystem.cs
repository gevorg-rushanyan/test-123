using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Providers;
using UnityEngine;

namespace Core.Sound
{
    public class SoundSystem : MonoBehaviour, ISoundSystem
    {
        private const int PoolObjectCount = 5;
        private ISoundsProvider _soundsProvider;
        private Queue<AudioSource> _audioSourcePool;
        private LinkedList<AudioSource> _activeAudioSources;

        public void Initialize(ISoundsProvider soundsProvider)
        {
            _audioSourcePool = new Queue<AudioSource>();
            _activeAudioSources = new LinkedList<AudioSource>();
            _soundsProvider = soundsProvider;
            for (int i = 0; i < PoolObjectCount; i++)
            {
                var audioSource = Instantiate(_soundsProvider.AudioSourcePrefab, transform);
                audioSource.gameObject.SetActive(false);
                _audioSourcePool.Enqueue(audioSource);
            }

            StartCoroutine(ReleaseFinishedSoundsCoroutine());
        }

        private IEnumerator ReleaseFinishedSoundsCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                int count = _activeAudioSources.Count;
                if (count <= 0)
                {
                    continue;
                }
                
                List<AudioSource> finishedSources = new List<AudioSource>();
                for (int i = 0; i < count; i++)
                {
                    var source = _activeAudioSources.ElementAt(i);
                    if (!source.isPlaying)
                    {
                        finishedSources.Add(source);
                    }
                }

                if (finishedSources.Count > 0)
                {
                    for (int i = 0; i < finishedSources.Count; i++)
                    {
                        _activeAudioSources.Remove(finishedSources[i]);
                        ReleaseSource(finishedSources[i]);
                    }
                }
            }
        }

        public void PlaySound(SoundType soundType)
        {
            var audioClip = _soundsProvider.GetSound(soundType);
            if (audioClip == null)
            {
                return;
            }
            
            var source = GetSource();
            source.gameObject.SetActive(true);
            source.clip = audioClip;
            source.Play();
            _activeAudioSources.AddLast(source);
        }

        public void StopAllSounds()
        {
            if (_activeAudioSources.Count == 0)
            {
                return;
            }
            foreach (var source in _activeAudioSources)
            {
                source.Stop();
                source.gameObject.SetActive(false);
                ReleaseSource(source);
            }
            _activeAudioSources.Clear();
        }
        
        private AudioSource GetSource()
        {
            if (_audioSourcePool.Count == 0)
            {
                var audioSource = Instantiate(_soundsProvider.AudioSourcePrefab, transform);
                audioSource.gameObject.SetActive(false);
                return audioSource;
            }
            
            return _audioSourcePool.Dequeue();
        }

        private void ReleaseSource(AudioSource audioSource)
        {
            if (_activeAudioSources.Count >= PoolObjectCount)
            {
                Destroy(audioSource.gameObject);
                return;
            }

            audioSource.Stop();
            audioSource.gameObject.SetActive(false);
            _audioSourcePool.Enqueue(audioSource);
        }
    }
}