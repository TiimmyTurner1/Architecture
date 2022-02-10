using System;
using UnityEditor.Animations;
using UnityEngine;

namespace CodeBase.Hero
{
    public class HeroAnimator: MonoBehaviour
    {
        private const string Speed = "Speed";
        
        public CharacterController CharacterController;
        public Animator Animator;

        private void Update()
        {
            Animator.SetFloat(Speed, CharacterController.velocity.magnitude, 0.1f, Time.deltaTime);
        }
    }
}