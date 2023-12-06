using UnityEngine;
using Zenject;

namespace LevelSystem.View
{
    public class LevelView : MonoBehaviour
    {
        public class Factory : PlaceholderFactory<LevelView> { }
    }
}