using System;
using CodeBase.CameraLogic;
using CodeBase.Data;
using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Input;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Hero
{
    [RequireComponent(typeof(CharacterController))]
    public class HeroMove : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private float _movementSpeed = 10f;
        
        private CharacterController _characterController;
        private IInputService _input;
        private HeroAnimator _heroAnimator;
        

        private void Awake()
        {
            _input = AllServices.Container.Single<IInputService>();

            _characterController = GetComponent<CharacterController>();
            _heroAnimator = GetComponent<HeroAnimator>();
        }
        
        private void Update()
        {
            Vector3 movementVector = Vector3.zero;

            if (_input.Axis.sqrMagnitude > Constants.Epsilon)
            {
                movementVector = Camera.main.transform.TransformDirection(_input.Axis);
                movementVector.y = 0f;
                movementVector.Normalize();

                transform.forward = movementVector;
            }

            movementVector += Physics.gravity;
            _characterController.Move(movementVector * _movementSpeed * Time.deltaTime);
        }

        public void UpdateProgress(PlayerProgress progress)=>
            progress.WorldData.PositionOnLevel = new PositionOnLevel(CurrentLevelName(), transform.position.AsVector3Data());
        
        public void LoadProgress(PlayerProgress progress)
        {
            if (CurrentLevelName() == progress.WorldData.PositionOnLevel.LevelName)
            {
                Vector3Data savedPosition = progress.WorldData.PositionOnLevel.Position;
                
                if (savedPosition != null)
                    Warp(savedPosition);
            }
        }

        private void Warp(Vector3Data to)
        {
            _characterController.enabled = false;
            transform.position = to.AsUnityVector3().AddY(_characterController.height);
            _characterController.enabled = true;
        }

        private static string CurrentLevelName() => 
            SceneManager.GetActiveScene().name;
    }
}