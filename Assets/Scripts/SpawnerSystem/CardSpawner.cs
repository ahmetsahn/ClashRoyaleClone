using System;
using DamageableSystem.CardSystem.View.Abstract;
using Enums;
using Extension;
using Signal;
using UnityEngine;

namespace SpawnerSystem
{
    public class CardSpawner : IDisposable
    {
        private readonly CardPoolSignals _cardPoolSignals;
        
        private readonly ButtonSignals _buttonSignals;
        
        private readonly InputSignals _inputSignals;
        
        private CardType _selectedCardType;

        public CardSpawner(
            CardPoolSignals cardPoolSignals,
            ButtonSignals buttonSignals,
            InputSignals inputSignals)
        {
            _cardPoolSignals = cardPoolSignals;
            _buttonSignals = buttonSignals;
            _inputSignals = inputSignals;
            
            SubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            _buttonSignals.OnSetSelectedCardButton += OnSetSelectedCardButton;
            _inputSignals.OnClickMouseButtonDown += OnSpawnCard;
        }
        
        private void OnSetSelectedCardButton(CardType cardType)
        {
            _selectedCardType = cardType;
        }
        
        private void OnSpawnCard()
        {
            CardView card = _cardPoolSignals.OnGetCard(_selectedCardType);
            InitializeCard(card);
        }
        
        private void InitializeCard(CardView card)
        {
            card.DamageableSide = DamageableSideType.Friendly;
            card.Transform.MatchPositionToRaycastHit(card.transform.position.y);
            card.Transform.rotation = Quaternion.Euler(0, 180, 0);
            card.gameObject.SetActive(true);
        }
        
        private void UnsubscribeEvents()
        {
            _buttonSignals.OnSetSelectedCardButton -= OnSetSelectedCardButton;
            _inputSignals.OnClickMouseButtonDown -= OnSpawnCard;
        }
        
        public void Dispose()
        {
            UnsubscribeEvents();
        }
    }
}