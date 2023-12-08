using System;
using System.Threading;
using System.Threading.Tasks;
using DamageableSystem.CardSystem.View.Abstract;
using Data.ScriptableObject;
using Enums;
using Signal;
using UnityEngine;
using Random = UnityEngine.Random;


namespace SpawnerSystem
{
    public class AICardSpawner : IDisposable
    {
        private readonly SpawnerSo _spawnerSo;
        
        private readonly ButtonSignals _buttonSignals;
        
        private readonly CardPoolSignals _cardPoolSignals;
        
        private readonly CoreGameSignals _coreGameSignals;
        
        private readonly CardType[] _cardTypes = new CardType[4];

        private readonly Vector3[] _cardSpawnPoints =
        {
            new Vector3(0, 0, -34.25f),
            new Vector3(-3.85f, 0, -34.25f),
            new Vector3(3.85f, 0, -34.25f)
        };
        
        private readonly CancellationTokenSource _cancellationTokenSource;
        
        public AICardSpawner(
            ButtonSignals buttonSignals,
            CardPoolSignals cardPoolSignals,
            CoreGameSignals coreGameSignals,
            SpawnerSo spawnerSo)
        {
            _buttonSignals = buttonSignals;
            _cardPoolSignals = cardPoolSignals;
            _coreGameSignals = coreGameSignals;
            _spawnerSo = spawnerSo;
            
            _cancellationTokenSource = new CancellationTokenSource();
            
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
        
        private async void SpawnCard()
        {
            while (!_cancellationTokenSource.Token.IsCancellationRequested)
            {
                int cardRandomIndex = Random.Range(0, _cardTypes.Length);
                int positionRandomIndex = Random.Range(0, _cardSpawnPoints.Length);
                CardView card = _cardPoolSignals.OnGetCard(_cardTypes[cardRandomIndex]);
                InitializeCard(card, positionRandomIndex);
                await Task.Delay(TimeSpan.FromSeconds(_spawnerSo.SpawnerData.SpawnInterval));
            }
        }

        private void InitializeCard(CardView card, int positionRandomIndex)
        {
            card.DamageableSide = DamageableSideType.Enemy;
            Transform cardTransform = card.transform;
            Vector3 spawnPosition = new Vector3(_cardSpawnPoints[positionRandomIndex].x, cardTransform.position.y, _cardSpawnPoints[positionRandomIndex].z);
            cardTransform.position = spawnPosition;
            card.OnSetInitialRotation?.Invoke();
            card.gameObject.SetActive(true);
        }

        private async void StartSpawnCard()
        {
            await Task.Delay(TimeSpan.FromSeconds(_spawnerSo.SpawnerData.TimeToStartSpawn));

            SpawnCard();
        }
        
        private void StopSpawnCard()
        {
            CancelToken();
        }
        
        private void CancelToken()
        {
            _cancellationTokenSource.Cancel();
        }
        private void UnsubscribeEvents()
        {
            _buttonSignals.OnAddToAICardList -= OnAddToAICardList;
            _coreGameSignals.OnPlayStart -= StartSpawnCard;
            _coreGameSignals.OnGameEnd -= StopSpawnCard;
        }

        public void Dispose()
        {
            UnsubscribeEvents();
            CancelToken();
        }
    }
}