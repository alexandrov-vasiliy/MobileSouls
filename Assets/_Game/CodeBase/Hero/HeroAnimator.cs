using System;
using UnityEngine;

namespace _Game.CodeBase.Hero
{
    public class HeroAnimator : MonoBehaviour
    {
        private static readonly int Speed = Animator.StringToHash("Speed");
        public Animator Animator;

        public CharacterController CharacterController;


        public void Update()
        {
            Animator.SetFloat(Speed, CharacterController.velocity.magnitude, 0.001f, Time.deltaTime);
        }
    }
}