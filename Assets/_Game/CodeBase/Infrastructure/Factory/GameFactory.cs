using System.Collections.Generic;
using _Game.CodeBase.Hero;
using _Game.CodeBase.Infrastructure.AssetManagement;
using UnityEngine;

namespace _Game.CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assets;
        
        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

        public GameFactory(IAssetProvider assets)
        {
            _assets = assets;
        }

        public void CreateHud() => InstantiateRegistred(AssetPath.ControlsHud);

        public GameObject CreateHero(GameObject at) => InstantiateRegistred(AssetPath.HeroPath, at.transform.position);

        private GameObject InstantiateRegistred(string path, Vector3 position)
        {
            GameObject gameObject = _assets.Instaniate(path, position);

            RegisterProgressWatcher(gameObject);
            return gameObject;
        }
        
        private GameObject InstantiateRegistred(string path)
        {
            GameObject gameObject = _assets.Instaniate(path);

            RegisterProgressWatcher(gameObject);
            return gameObject;
        }

        private void RegisterProgressWatcher(GameObject hero)
        {
            foreach (var progressReader in hero.GetComponentsInChildren<ISavedProgressReader>())
            {
                Register(progressReader);
            }
        }

        private void Register(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgress progressWriter)
            {
                ProgressWriters.Add(progressWriter);
            }
            
            ProgressReaders.Add(progressReader);
        }

        public void Cleanup()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }
    }
}