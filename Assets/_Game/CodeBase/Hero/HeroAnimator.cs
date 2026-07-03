using UnityEngine;

namespace _Game.CodeBase.Hero
{
    public class HeroAnimator : MonoBehaviour
    {
        private static readonly int Speed = Animator.StringToHash("Speed");

        [SerializeField] private Animator animator;
        private CharacterController _characterController;


        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        public void Update()
        {
            animator.SetFloat(Speed, _characterController.velocity.magnitude, 0.001f, Time.deltaTime);
        }
    }
}