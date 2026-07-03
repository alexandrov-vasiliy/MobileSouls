using UnityEngine;

namespace _Game.CodeBase.Infrastructure.AssetManagement
{
    public class AssetProvider : IAssetProvider
    {
        public GameObject Instaniate(string path, Vector3 at)
        {
            var heroPrefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(heroPrefab, at, Quaternion.identity);
        }

        public GameObject Instaniate(string path)
        {
            var heroPrefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(heroPrefab);
        }
    }
}