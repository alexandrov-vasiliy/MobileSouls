using UnityEngine;

namespace _Game.CodeBase.Services.Input
{
    public class StandaloneInputService : BaseInputService
    {
        public override Vector2 Axis
        {
            get
            {
                Vector2 axis = GetSimpleInputAxis();

                if (axis == Vector2.zero)
                {
                    axis = GetOldInputAxis();
                }
                
                return axis;
            }
        }

        private static Vector2 GetOldInputAxis()
        {
            return new Vector2(UnityEngine.Input.GetAxis(Horizontal), UnityEngine.Input.GetAxis(Vertical));
        }
    }
}