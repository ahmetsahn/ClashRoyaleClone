using System;
using System.Collections.Generic;
using DamageableSystem.CardSystem.View.Abstract;
using Enums;
using Signal;

namespace PoolSystem
{
    public class CardPool : IDisposable
    {
        private readonly CardView.Factory _cardFactory;
        
        private readonly CardPoolSignals _cardPoolSignals;
    
        private readonly List<CardView> _objectPool = new();
        
        public CardPool(
            CardView.Factory cardFactory, 
            CardPoolSignals cardPoolSignals)
        {
            _cardFactory = cardFactory;
            _cardPoolSignals = cardPoolSignals;
        
            SubscribeEvents();
        }
    
        private void SubscribeEvents()
        {
            _cardPoolSignals.OnGetCard += OnGetCard;
            _cardPoolSignals.OnReturnCardToPool += OnReturnCardToPool;
        }
    
        private CardView CreateCard(CardType cardType)
        {
            CardView newObject = _cardFactory.Create(cardType);
            newObject.name = cardType.ToString();
            return newObject;
        }

        private CardView OnGetCard(CardType cardType)
        {
            foreach (CardView card in _objectPool)
            {
                if (card.gameObject.activeSelf == false && card.name == cardType.ToString())
                {
                    card.gameObject.SetActive(true);
                    return card;
                }
            }
        
            CardView newCard = CreateCard(cardType);
            _objectPool.Add(newCard);
            return newCard;
        }

        private void OnReturnCardToPool(CardView cardView)
        {
            cardView.gameObject.SetActive(false);
        }
    
        private void UnsubscribeEvents()
        {
            _cardPoolSignals.OnGetCard -= OnGetCard;
            _cardPoolSignals.OnReturnCardToPool -= OnReturnCardToPool;
        }

        public void Dispose()
        {
            UnsubscribeEvents();
        }
    }
}