using DamageableSystem.Abstract.UI;
using DamageableSystem.CardSystem.View.Abstract;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace DamageableSystem.CardSystem.UI.Abstract
{
    public abstract class CardGUI : DamageableGUI
    {
        [SerializeField]
        protected GameObject Canvas;
        
        [SerializeField] 
        protected Image CardInstallationFillImage;
        
        private CardView CardView => (CardView) DamageableView;

        private Tween _fillTween;
        
        protected override void OnEnable()
        {
            base.OnEnable();
            InstallCardWithFillAnimation();
        }

        protected override void SubscribeEvents()
        {
            base.SubscribeEvents();
            CardView.OnSetInitialRotation += SetCanvasRotation;
        }

        private void Update()
        {
            SetCanvasRotation();
        }

        private void SetCanvasRotation()
        {
            var cardViewRotationY = DamageableView.Transform.rotation.eulerAngles.y;
            Canvas.transform.localRotation = Quaternion.Euler(0f, -cardViewRotationY + 180, 0f);
        }

        private void InstallCardWithFillAnimation()
        {
            _fillTween = DOTween.To(() => CardInstallationFillImage.fillAmount, x => CardInstallationFillImage.fillAmount = x, 1f, 
                    CardView.DamageableSo.CardInstallationData.InstallationTime).SetEase(Ease.Linear).OnComplete(OnCardInstallationComplete);
        }
        
        private void OnCardInstallationComplete()
        {
            SetCardInstallationImageVisibility(false);
        }

        private void ResetCardInstallationImageFillAmount()
        {
            CardInstallationFillImage.fillAmount = 0f;
        }

        private void SetCardInstallationImageVisibility(bool value)
        {
            CardInstallationFillImage.gameObject.SetActive(value);
        }
        
        private void ResetHealthBarFillAmount()
        {
            HealthBarFillAmountImage.fillAmount = 1f;
        }
        
        private void KillTween()
        {
            if (_fillTween != null && _fillTween.IsActive())
            {
                _fillTween.Kill();
            }
        }
        
        private void ResetGUI()
        {
            KillTween();
            ResetCardInstallationImageFillAmount();
            ResetHealthBarFillAmount();
            SetCardInstallationImageVisibility(true);
            SetHealthBarVisibility(false);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            ResetGUI();
        }
    }
}