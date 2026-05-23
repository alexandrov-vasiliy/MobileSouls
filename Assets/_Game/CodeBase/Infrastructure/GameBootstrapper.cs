using UnityEngine;

namespace _Game.CodeBase.Infrastructure
{
    [DefaultExecutionOrder(-50)]
    public class GameBootstrapper : MonoBehaviour
    {
        private Game _game;

        private void Awake()
        {
            _game = new Game();
            DontDestroyOnLoad(this);
        }
    }
}
