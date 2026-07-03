using _Game.CodeBase.Infrastructure.Services;
using UnityEngine;

namespace _Game.CodeBase.Services.Input
{
    public interface IInputService : IService
    {
        Vector2 Axis { get; }
        
        bool IsAttackButtonUp();
    }
}