using DamageableSystem.Abstract.View;
using Extension;
using Signal;
using UnityEngine;
using Zenject;

namespace DamageableSystem.CardSystem.View
{
    public class CardPreviewView : MonoBehaviour
    {
        [SerializeField]
        private Material[] materials;
        
        [SerializeField]
        private Color materialDefaultColor;
        
        [SerializeField]
        private Color materialRedColor;
        
        [SerializeField]
        private Color materialEmissionDefaultColor;
        
        [SerializeField]
        private Color materialEmissionRedColor;
        
        [SerializeField]
        private float overlapSphereRadius;

        private const string EmissionColor = "_EmissionColor";
        
        private InputSignals _inputSignals;
        
        private bool _isRed; 
        
        [Inject]
        private void Construct(
            InputSignals inputSignals)
        {
            _inputSignals = inputSignals;
        }
        
        private void ResetTransformPosition()
        {
            transform.transform.position = Vector3.zero;
        }
        
        private void ResetFlag()
        {
            _isRed = false;
        }
        
        private void SetMaterialsColorRed()
        {
            if (_isRed)
            {
                return;
            }
            
            _inputSignals.OnEnableInput?.Invoke(false);
            
            foreach (Material material in materials)
            {
                material.color = materialRedColor;
                material.SetColor(EmissionColor, materialEmissionRedColor);
            }
            
            _isRed = true;
        }
        
        private void SetMaterialsColorDefault()
        {
            if (!_isRed)
            {
                return;
            }
            
            _inputSignals.OnEnableInput?.Invoke(true);
            
            foreach (Material material in materials)
            {
                material.color = materialDefaultColor;
                material.SetColor(EmissionColor, materialEmissionDefaultColor);
            }
            
            _isRed = false;
        }

        private void Update()
        {
            transform.MatchPositionToRaycastHit();
            DetectionColliders();
        }

        private void DetectionColliders()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, overlapSphereRadius);
            foreach (Collider collider in colliders)
            {
                if (!collider.TryGetComponent(out DamageableView damageableView)) continue;
                SetMaterialsColorRed();
                _isRed = true;
                return;
            }

            SetMaterialsColorDefault();
        }

        private void OnDisable()
        {
            SetMaterialsColorDefault();
            ResetTransformPosition();
            ResetFlag();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, overlapSphereRadius);
        }
    }
}