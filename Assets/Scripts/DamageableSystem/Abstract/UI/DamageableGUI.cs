using DamageableSystem.Abstract.View;
using UnityEngine;
using UnityEngine.UI;

namespace DamageableSystem.Abstract.UI
{
    public abstract class DamageableGUI : MonoBehaviour
    {
        protected DamageableView DamageableView;
        
        [SerializeField]
        protected GameObject HealthBar;
        
        [SerializeField]
        protected Image HealthBarFillAmountImage;
        
        protected virtual void OnEnable()
        {
            SubscribeEvents();
        }
        
        protected virtual void SubscribeEvents()
        {
            DamageableView.OnTakeDamage += OnTakeDamage;
        }
        
        private void OnTakeDamage(float damage)
        {
            SetHealthBarVisibility(true);
            ReduceHealthBarFillAmountImage(damage);
        }
        
        private void ReduceHealthBarFillAmountImage(float damage)
        {
            HealthBarFillAmountImage.fillAmount -= damage / DamageableView.DamageableSo.DamageableHealthData.MaxHealth;
        }
        
        protected void SetHealthBarVisibility(bool value)
        {
            HealthBar.SetActive(value);
        }
        
        private void UnsubscribeEvents()
        {
            DamageableView.OnTakeDamage -= OnTakeDamage;
        }
        
        protected virtual void OnDisable()
        {
            UnsubscribeEvents();
        }
    }
}