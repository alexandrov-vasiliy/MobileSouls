using System;
using UnityEngine;

namespace _Game.CodeBase.Cameralogic
{
    public class CameraFollow : MonoBehaviour
    {
        public float RotationAngelX = 45f;
        public float Distance = 10f;
        public float OffsetY = 10f;


        [SerializeField] private Transform _following;


        private void LateUpdate()
        {
            if(_following is null)  return;

            Quaternion rotation = Quaternion.Euler(RotationAngelX, 0, 0);


            Vector3 position = rotation * new Vector3(0, 0, -Distance) + GetFollowingPointPosition();
            
            transform.rotation = rotation;
            transform.position = position;
        }

        public void Follow(GameObject target)
        {
            _following = target.transform;
        }
        
        private Vector3 GetFollowingPointPosition()
        {
            Vector3 followingPosition = _following.position;
            followingPosition.y += OffsetY;
            return followingPosition;
        }
    }
}