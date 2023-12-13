using UnityEngine.Events;

namespace Signal
{
    public class CoreGameSignals
    {
        public UnityAction OnPlayStart;
        public UnityAction OnGameEnd;
        public UnityAction OnWin;
        public UnityAction OnLose;
    }
}