using UnityEngine;

namespace _Game.CodeBase.Infrastructure
{
    public class GameRunner : MonoBehaviour
    {
        public GameBootstrapper BootstrapperPrefab;
        
        public void Awake()
        {
            var bootstrapper = FindAnyObjectByType<GameBootstrapper>();

            if (bootstrapper == null)
            {
                Instantiate(BootstrapperPrefab);
            }
        }
    }
}