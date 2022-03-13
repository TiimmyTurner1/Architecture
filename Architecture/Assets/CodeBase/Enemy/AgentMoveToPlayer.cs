using System;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Enemy
{
    public class AgentMoveToPlayer : Follow
    {
        private const float _minimalDistance = 1.5f;
        
        [SerializeField] private NavMeshAgent _agent;

        private IGameFactory _gameFactory;
        private Transform _heroTransform;

        private void Start()
        {
            _gameFactory = AllServices.Container.Single<IGameFactory>();

            if (HeroExist())
                InitializeHeroTransform();
            else
                _gameFactory.HeroCreated += InitializeHeroTransform;
        }

        private void Update()
        {
            if(Initialized() && IsHeroReached())
                _agent.destination = _heroTransform.position;
        }

        private bool Initialized() => 
            _heroTransform != null;

        private bool HeroExist() => 
            _gameFactory.HeroGameObject != null;
        
        private void InitializeHeroTransform()=>
            _heroTransform = _gameFactory.HeroGameObject.transform;

        private bool IsHeroReached() => 
            Vector3.Distance(_agent.transform.position, _heroTransform.position) >= _minimalDistance;
    }
}