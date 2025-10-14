using UnityEngine;

namespace UI
{
    public abstract class BaseViewController
    {
        private Transform _root;
        private GameObject _viewGameObject;
        
        public BaseViewController(Transform root)
        {
            _root = root;
        }
        
        public abstract void Show();
        
        protected T Show<T>(T viewPrefab) where T : MonoBehaviour
        {
            var view = Object.Instantiate(viewPrefab, _root);
            _viewGameObject = view.gameObject;
            
            return view;
        }

        public void Hide()
        {
            Object.Destroy(_viewGameObject);
        }
    }
}