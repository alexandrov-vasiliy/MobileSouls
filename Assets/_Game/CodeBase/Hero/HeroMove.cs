using _Game.CodeBase.Cameralogic;
using _Game.CodeBase.Infrastructure;
using _Game.CodeBase.Services.Input;
using UnityEngine;

namespace _Game.CodeBase.Hero
{
    public class HeroMove : MonoBehaviour
    {
        public CharacterController CharacterController;
        public float movementSpeed = 4;
        
        private IInputService _inputService;
        private Camera _camera;

        private void Awake()
        {
            _inputService = Game.InputService;
        }

        private void Start()
        {
            _camera = Camera.main;
            
            _camera.GetComponent<CameraFollow>().Follow(gameObject);
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
            
            CharacterController.Move(movementSpeed * movementVector * Time.deltaTime);
        }
    }
}