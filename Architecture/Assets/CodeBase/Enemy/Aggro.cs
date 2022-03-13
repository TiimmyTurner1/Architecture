using System;
using System.Collections;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class Aggro : MonoBehaviour
    {
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private Follow _follow;
        
        [SerializeField] private float _followCooldown;
        
        private Coroutine _aggroCoroutine;
        private bool _hasAggroTarget;

        private void Start()
        {
            _triggerObserver.TriggerEnter += TriggerEnter;
            _triggerObserver.TriggerExit += TriggerExit;

            _follow.enabled = false;
        }

        private void TriggerEnter(Collider obj)
        {
            
            if (!_hasAggroTarget)
            {
                _hasAggroTarget = true;
                
                StopAggroCoroutine();
            
                SwitchFollow(true);
            }
        }

        private void TriggerExit(Collider obj)
        {
            if (_hasAggroTarget)
            {
                _hasAggroTarget = false;
                
                _aggroCoroutine = StartCoroutine(SwitchFollowOffAfterCooldown());
            }
        }

        private void StopAggroCoroutine()
        {
            if (_aggroCoroutine != null)
            {
                StopCoroutine(SwitchFollowOffAfterCooldown());
                _aggroCoroutine = null;
            }
        }

        private IEnumerator SwitchFollowOffAfterCooldown()
        {
            yield return new WaitForSeconds(_followCooldown);
            
            SwitchFollow(false);
        }


        private void SwitchFollow(bool isFollow) => 
            _follow.enabled = isFollow;
    }
}