using _Game.CodeBase.Data;
using _Game.CodeBase.Infrastructure.Services;
using _Game.CodeBase.Services.Input;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Game.CodeBase.Hero
{
    [RequireComponent(typeof(CharacterController))]
    public class HeroMove : MonoBehaviour, ISavedProgress
    {
        public float movementSpeed = 4;

        private CharacterController _characterController;
        private IInputService _inputService;
        private Camera _camera;

        private void Awake()
        {
            _inputService = AllServices.Container.Single<IInputService>();
            
            _characterController = GetComponent<CharacterController>();
        }

        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            Vector3 movementVector = Vector3.zero;

            if (_inputService.Axis.sqrMagnitude > Constants.Epsilon)
            {
                movementVector = _camera.transform.TransformDirection(_inputService.Axis);
                movementVector.y = 0;
                movementVector.Normalize();

                transform.forward = movementVector;
            }

            movementVector += Physics.gravity;

            _characterController.Move(movementSpeed * movementVector * Time.deltaTime);
        }

        public void LoadProgress(PlayerProgress playerProgress)
        {
            if (CurrentLevel() == playerProgress.WorldData.PositionOnLevel.Level)
            {
                var savedPosition = playerProgress.WorldData.PositionOnLevel.Position;

                if (savedPosition != null)
                {
                    Warp(to: savedPosition);
                }
            } 
            
        }

        private void Warp(Vector3Data to)
        {
            _characterController.enabled = false;
            transform.position = to.AsUnityVector().AddY(_characterController.height);
            _characterController.enabled = true;
        }

        public void UpdateProgress(PlayerProgress playerProgress)
        {
            playerProgress.WorldData.PositionOnLevel = new PositionOnLevel(CurrentLevel(),
                transform.position.AsVectorData());
        }

        private static string CurrentLevel()
        {
            return SceneManager.GetActiveScene().name;
        }
    }
}