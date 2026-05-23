using UnityEngine;

namespace _Game.CodeBase.Services.Input
{
    public class MobileInputService : BaseInputService
    {
        public override Vector2 Axis => GetSimpleInputAxis();
    }
}