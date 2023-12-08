using System;
using Enums;
using Signal;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace UISystem
{
    public class UIPanelHandler : IDisposable
    {
        private readonly IInstantiator _instantiator;
        
        private readonly UISignals _uiSignals;
        
        private readonly Transform[] _layers;
        
        private const string PANELS_PATH = "Prefab/UIPanel/";
        
        public UIPanelHandler(
            IInstantiator instantiator,
            UISignals uiSignals,
            Transform[] layers)
        {
            _instantiator = instantiator;
            _uiSignals = uiSignals;
            _layers = layers;
            
            SubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            _uiSignals.OnOpenPanel += OnOpenPanel;
            _uiSignals.OnClosePanel += OnClosePanel;
            _uiSignals.OnCloseAllPanels += OnCloseAllPanels;
        }
        
        private void OnOpenPanel(UIPanelType panelType, int panelIndex)
        {
            OnClosePanel(panelIndex);
            _instantiator.InstantiatePrefab(Resources.Load<GameObject>(PANELS_PATH + panelType), _layers[panelIndex]);
        }

        private void OnClosePanel(int panelIndex)
        {
            if (_layers[panelIndex].childCount <= 0) return;
            foreach (Transform child in _layers[panelIndex])
            {
                Object.Destroy(child.gameObject);
            }
        }
        
        private void OnCloseAllPanels()
        {
            foreach (Transform layer in _layers)
            {
                if (layer.childCount <= 0) return;
                foreach (Transform child in layer)
                {
                    Object.Destroy(child.gameObject);
                }
            }
        }

        private void UnsubscribeEvents()
        {
            _uiSignals.OnOpenPanel -= OnOpenPanel;
            _uiSignals.OnClosePanel -= OnClosePanel;
            _uiSignals.OnCloseAllPanels -= OnCloseAllPanels;
        }

        public void Dispose()
        {
            UnsubscribeEvents();
        }
    }
}