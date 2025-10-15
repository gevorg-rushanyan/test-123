using UnityEngine;
using UnityEngine.U2D;

namespace Containers
{
    [CreateAssetMenu(fileName = "CommonResourceContainer", menuName = "ScriptableObjects/CommonResourceContainer")]
    public class CommonResourceContainer : ScriptableObject
    {
        [SerializeField] private SpriteAtlas _boardAtlas;
        [Header("Sounds"), Space]
        [SerializeField] private AudioClip _flipSound;
        [SerializeField] private AudioClip _matchSound;
        [SerializeField] private AudioClip _winSound;
        [SerializeField] private AudioClip _loseSound;
        
        [SerializeField] private AudioSource _audioSourcePrefab;
        
        public AudioSource AudioSourcePrefab => _audioSourcePrefab;
        public SpriteAtlas BoardAtlas => _boardAtlas;
        public AudioClip FlipSound => _flipSound;
        public AudioClip MatchSound => _matchSound;
        public AudioClip WinSound => _winSound;
        public AudioClip LoseSound => _loseSound;
    }
}