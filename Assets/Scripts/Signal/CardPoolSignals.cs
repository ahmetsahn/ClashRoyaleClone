using System;
using DamageableSystem.CardSystem.View.Abstract;
using Enums;
using UnityEngine.Events;

namespace Signal
{
    public class CardPoolSignals
    {
        public Func<CardType, CardView> OnGetCard;
        
        public UnityAction<CardView> OnReturnCardToPool;
    }
}