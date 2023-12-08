using System;
using DamageableSystem.CardSystem.View;
using Enums;
using UnityEngine;

namespace DamageableSystem.CardSystem.Handler.AnimationHandler
{
    public class TroopCardAnimationHandler : IDisposable
    {
        private readonly TroopCardView _troopCardView;
        
        private static readonly int Idle = Animator.StringToHash("Idle");
        private static readonly int Movement = Animator.StringToHash("Movement");
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int AttackAnimationSpeed = Animator.StringToHash("AttackAnimationSpeed");
        
        public TroopCardAnimationHandler(
            TroopCardView troopCardView)
        {
            _troopCardView = troopCardView;
            
            SubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            _troopCardView.OnPlayAnimation += OnPlayAnimation;
            _troopCardView.OnInitializeTroopCard += OnSetAttackAnimationSpeed;
        }

        private void OnPlayAnimation(TroopCardAnimationType animationType)
        {
            switch (animationType)
            {
                case TroopCardAnimationType.Idle:
                    CrossAnimation(Idle);
                    break;
                case TroopCardAnimationType.Movement:
                    CrossAnimation(Movement);
                    break;
                case TroopCardAnimationType.Attack:
                    CrossAnimation(Attack);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(animationType), animationType, null);
            }
        }
        
        private void CrossAnimation(int animationHash)
        {
            _troopCardView.Animator.CrossFade(animationHash, 0f, 0);
        }
        
        private void OnSetAttackAnimationSpeed()
        {
            _troopCardView.Animator.SetFloat(AttackAnimationSpeed, _troopCardView.DamageableSo.DamageableAttackData.AttackSpeed);
        }
        
        private void UnsubscribeEvents()
        {
            _troopCardView.OnPlayAnimation -= OnPlayAnimation;
            _troopCardView.OnInitializeTroopCard -= OnSetAttackAnimationSpeed;
        }

        public void Dispose()
        {
            UnsubscribeEvents();
        }
    }
}