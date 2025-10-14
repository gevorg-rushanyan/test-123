using System;
using System.Collections.Generic;
using Containers;
using UI.MainMenu;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Providers
{
    public class ViewProvider : IViewProvider
    {
        private const string Path = "Containers/ViewsContainer";
        private ViewContainer _viewContainer;
        private Dictionary<Type, Object> _viewsMapping;
        
        public void Initialize()
        {
            _viewContainer = Resources.Load<ViewContainer>(Path);
            _viewsMapping = new Dictionary<Type, Object>()
            {
                {typeof(MainMenuView), _viewContainer.MainMenu}
            };
        }

        public T GetView<T>() where T : class
        {
            if (_viewsMapping.TryGetValue(typeof(T), out Object view))
            {
                return view as T;
            }

            return null;
        }
    }
}