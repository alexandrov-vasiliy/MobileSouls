using _Game.CodeBase.Infrastructure.Services;
using _Game.CodeBase.Infrastructure.States;
using _Game.CodeBase.Ui;

namespace _Game.CodeBase.Infrastructure
{
    public class Game
    {
        public GameStateMachine StateMachine;

        public Game(ICoroutineRunner coroutineRunner, LoadingCurtain curtain)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), curtain, AllServices.Container);
        }
    }
}