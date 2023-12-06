using System.Collections.Generic;
using DamageableSystem.CardSystem.View.Abstract;
using Enums;
using UnityEngine;
using Zenject;

namespace DamageableSystem.CardSystem.Factory
{
    public class CardFactory : IFactory<CardType, CardView>
    {
        private readonly IInstantiator _instantiator;
        
        private const string CARD_PREFAB_PATH = "Prefab/Card/";
        
        private readonly Dictionary<CardType, GameObject> _cardTypeCardPrefabDictionary = new()
        {
            { CardType.Pekka, Resources.Load<GameObject>(CARD_PREFAB_PATH + CardType.Pekka) },
            { CardType.ArcherQueen, Resources.Load<GameObject>(CARD_PREFAB_PATH + CardType.ArcherQueen) },
            { CardType.BarbarianKing, Resources.Load<GameObject>(CARD_PREFAB_PATH + CardType.BarbarianKing) },
            { CardType.Golem, Resources.Load<GameObject>(CARD_PREFAB_PATH + CardType.Golem) },
            { CardType.Wizard, Resources.Load<GameObject>(CARD_PREFAB_PATH + CardType.Wizard) },
            { CardType.Sparky, Resources.Load<GameObject>(CARD_PREFAB_PATH + CardType.Sparky) },
            { CardType.InfernoTower, Resources.Load<GameObject>(CARD_PREFAB_PATH + CardType.InfernoTower) },
            { CardType.Skeleton, Resources.Load<GameObject>(CARD_PREFAB_PATH + CardType.Skeleton) }
        };
        
        public CardFactory(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }
        
        public CardView Create(CardType cardType)
        {
            var card = _instantiator.InstantiatePrefabForComponent<CardView>(_cardTypeCardPrefabDictionary[cardType]); 
            return card;
        }
    }
}