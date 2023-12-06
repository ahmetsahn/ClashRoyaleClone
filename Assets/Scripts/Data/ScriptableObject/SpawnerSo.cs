using Data.ValueType;
using UnityEngine;

namespace Data.ScriptableObject
{
    [CreateAssetMenu(fileName = "SpawnerSo", menuName = "Scriptable Objects/SpawnerSo", order = 0)]
    public class SpawnerSo : UnityEngine.ScriptableObject
    {
        public SpawnerData SpawnerData;
    }
}