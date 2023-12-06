using DamageableSystem.CardSystem.View;
using UnityEngine;

namespace Extension
{
    public static class TransformExtension
    {
        private static readonly int LayerMask = 1 << UnityEngine.LayerMask.NameToLayer("Ground");
        
        public static void MatchPositionToRaycastHit(this Transform transform)
        {
            if (!Physics.Raycast(Util.Utils.Ray, out var hit, Mathf.Infinity,LayerMask))
            {
                return;
            }
            
            Vector3 hitPoint = hit.point;
            transform.position = new Vector3(hitPoint.x, hitPoint.y, hitPoint.z);
        }
        
        public static void MatchPositionToRaycastHit(this Transform transform, float yValue)
        {
            if (!Physics.Raycast(Util.Utils.Ray, out var hit, Mathf.Infinity,LayerMask))
            {
                return;
            }
            Vector3 hitPoint = hit.point;
            transform.position = new Vector3(hitPoint.x, yValue, hitPoint.z);
        }
        
        public static void MatchPositionToSpawnPoint(this Transform transform, Transform spawnPoint)
        {
            transform.position = spawnPoint.position;
            transform.rotation = spawnPoint.rotation;
        }
    }
}