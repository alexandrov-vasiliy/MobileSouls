using _Game.CodeBase.Infrastructure.States;
using _Game.CodeBase.Ui;
using UnityEngine;

namespace _Game.CodeBase.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        public LoadingCurtain CurtainPrefab;
        
        private Game _game;

        private void Awake()
        {
            _game = new Game(this, Instantiate(CurtainPrefab));
            _game.StateMachine.Enter<BootstrapState>();
            
            DontDestroyOnLoad(this);
        }
    }
}
