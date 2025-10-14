using UnityEngine;
using UnityEngine.U2D;

namespace Containers
{
    [CreateAssetMenu(fileName = "CommonResourceContainer", menuName = "ScriptableObjects/CommonResourceContainer")]
    public class CommonResourceContainer : ScriptableObject
    {
        [SerializeField] private SpriteAtlas _boardAtlas;
        
        public SpriteAtlas BoardAtlas => _boardAtlas;
    }
}