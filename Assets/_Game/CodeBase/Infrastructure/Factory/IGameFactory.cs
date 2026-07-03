using System.Collections.Generic;
using _Game.CodeBase.Hero;
using _Game.CodeBase.Infrastructure.Services;
using UnityEngine;

namespace _Game.CodeBase.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        void CreateHud();
        GameObject CreateHero(GameObject at);
        void Cleanup();
        
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }
    }
}