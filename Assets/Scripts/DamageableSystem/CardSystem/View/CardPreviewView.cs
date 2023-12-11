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
        
        private void SetFlag(bool value)
        {
            _isRed = value;
        }
        
        private void SetMaterialsColorRed()
        {
            if (_isRed)
            {
                return;
            }
            
            _inputSignals.OnEnableInput?.Invoke(false);
            
            SetColorOfMaterials(materialRedColor, materialEmissionRedColor);
            
            SetFlag(true);
        }
        
        private void SetMaterialsColorDefault()
        {
            if (!_isRed)
            {
                return;
            }
            
            _inputSignals.OnEnableInput?.Invoke(true);
            
            SetColorOfMaterials(materialDefaultColor, materialEmissionDefaultColor);
            
            SetFlag(false);
        }

        private void SetColorOfMaterials(Color baseMapColor, Color emissionColor)
        {
            foreach (Material material in materials)
            {
                material.color = baseMapColor;
                material.SetColor(EmissionColor, emissionColor);
            }
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
                if (!collider.TryGetComponent(out DamageableView damageableView))
                {
                    continue;
                }
                
                SetMaterialsColorRed();
                SetFlag(true);
                return;
            }

            SetMaterialsColorDefault();
        }

        private void OnDisable()
        {
            SetMaterialsColorDefault();
            ResetTransformPosition();
            SetFlag(false);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, overlapSphereRadius);
        }
    }
}