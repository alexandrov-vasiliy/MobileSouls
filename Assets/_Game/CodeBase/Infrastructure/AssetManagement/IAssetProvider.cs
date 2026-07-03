using _Game.CodeBase.Infrastructure.Services;
using UnityEngine;

namespace _Game.CodeBase.Infrastructure.AssetManagement
{
    public interface IAssetProvider : IService
    {
        GameObject Instaniate(string path, Vector3 at);
        GameObject Instaniate(string path);
    }
}