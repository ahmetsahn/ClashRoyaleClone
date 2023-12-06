using Enums;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Signal
{
    public class UISignals
    {
        public UnityAction<UIPanelType, int> OnOpenPanel;
        public UnityAction<int> OnClosePanel;
        public UnityAction OnCloseAllPanels;
    }
}