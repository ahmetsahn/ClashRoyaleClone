using DamageableSystem.CardSystem.View.Abstract;
using Data.ScriptableObject;
using Enums;
using Signal;
using UnityEngine;
using Zenject;


namespace SpawnerSystem
{
    public class AICardSpawner : MonoBehaviour
    {
        [SerializeField]
        private SpawnerSo spawnerSo;
        
        private ButtonSignals _buttonSignals;
        
        private CardPoolSignals _cardPoolSignals;
        
        private CoreGameSignals _coreGameSignals;
        
        private readonly CardType[] _cardTypes = new CardType[4];

        private readonly Vector3[] _cardSpawnPoints =
        {
            new Vector3(0, 0, -34.25f),
            new Vector3(-3.85f, 0, -34.25f),
            new Vector3(3.85f, 0, -34.25f)
        };
        
        [Inject]
        public void Construct(
            ButtonSignals buttonSignals,
            CardPoolSignals cardPoolSignals,
            CoreGameSignals coreGameSignals)
        {
            _buttonSignals = buttonSignals;
            _cardPoolSignals = cardPoolSignals;
            _coreGameSignals = coreGameSignals;
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _buttonSignals.OnAddToAICardList += OnAddToAICardList;
            _coreGameSignals.OnPlayStart += StartSpawnCard;
            _coreGameSignals.OnGameEnd += StopSpawnCard;
        }
        
        private void OnAddToAICardList(CardType cardType)
        {
            for (int i = 0; i < _cardTypes.Length; i++)
            {
                if (_cardTypes[i] != CardType.None)
                {
                    continue;
                }
                _cardTypes[i] = cardType;
                break;
            }
        }
        
        private void SpawnCard()
        {
            int cardRandomIndex = Random.Range(0, _cardTypes.Length);
            int positionRandomIndex = Random.Range(0, _cardSpawnPoints.Length);
            CardView card = _cardPoolSignals.OnGetCard(_cardTypes[cardRandomIndex]);
            InitializeCard(card, positionRandomIndex);
        }

        private void InitializeCard(CardView card, int positionRandomIndex)
        {
            card.DamageableSide = DamageableSideType.Enemy;
            var cardTransform = card.transform;
            var spawnPosition = new Vector3(_cardSpawnPoints[positionRandomIndex].x, cardTransform.position.y, _cardSpawnPoints[positionRandomIndex].z);
            cardTransform.position = spawnPosition;
            card.OnSetInitialRotation?.Invoke();
            card.gameObject.SetActive(true);
        }

        private void StartSpawnCard()
        {
            InvokeRepeating(nameof(SpawnCard), spawnerSo.SpawnerData.TimeToStartSpawn, spawnerSo.SpawnerData.SpawnInterval);
        }
        
        private void StopSpawnCard()
        {
            CancelInvoke(nameof(SpawnCard));
        }
        
        private void UnsubscribeEvents()
        {
            _buttonSignals.OnAddToAICardList -= OnAddToAICardList;
            _coreGameSignals.OnPlayStart -= StartSpawnCard;
            _coreGameSignals.OnGameEnd -= StopSpawnCard;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }
    }
}