using System;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Enemy
{
    public class AgentMoveToPlayer : MonoBehaviour
    {
        private const float _minimalDistance = 1.5f;
        
        [SerializeField] private NavMeshAgent _agent;

        private IGameFactory _gameFactory;
        private Transform _heroTransform;

        private void Start()
        {
            _gameFactory = AllServices.Container.Single<IGameFactory>();

            if (_gameFactory.HeroGameObject != null)
                InitializeHeroTransform();
            else
                _gameFactory.HeroCreated += HeroCreated;
        }

        private void Update()
        {
            if(_heroTransform!= null && IsHeroReached())
                _agent.destination = _heroTransform.position;
        }

        private void HeroCreated()=>
            InitializeHeroTransform();

        private void InitializeHeroTransform()=>
            _heroTransform = _gameFactory.HeroGameObject.transform;

        private bool IsHeroReached() => 
            Vector3.Distance(_agent.transform.position, _heroTransform.position) >= _minimalDistance;
    }
}