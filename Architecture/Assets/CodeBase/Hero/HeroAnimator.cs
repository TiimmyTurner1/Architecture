using System;
using UnityEditor.Animations;
using UnityEngine;

namespace CodeBase.Hero
{
    public class HeroAnimator: MonoBehaviour
    {
        private const string Speed = "Speed";
        
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Animator _animator;

        private void Update()
        {
            _animator.SetFloat(Speed, _characterController.velocity.magnitude, 0.1f, Time.deltaTime);
        }
    }
}