using _Game.CodeBase.Infrastructure.AssetManagement;
using _Game.CodeBase.Infrastructure.Factory;
using _Game.CodeBase.Infrastructure.Services;
using _Game.CodeBase.Infrastructure.Services.PersistantProgress;
using _Game.CodeBase.Infrastructure.Services.SaveLoad;
using _Game.CodeBase.Services.Input;
using UnityEngine;

namespace _Game.CodeBase.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string Initial = "Initial";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;

        public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, AllServices services)
        {
            _stateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _services = services;

            RegisterServices();
        }

        public void Enter()
        {
            _sceneLoader.Load(Initial, onLoaded: EnterLoadLevel);
        }

        private void EnterLoadLevel()
        {
            _stateMachine.Enter<LoadProgressState>();
        }

        public void Exit()
        {
        }

        private void RegisterServices()
        {
            _services.RegisterSingle<IInputService>(InputService());
            _services.RegisterSingle<IAssetProvider>(new AssetProvider());
            _services.RegisterSingle<IPersistantProgressService>(new PersistantProgressService());
            _services.RegisterSingle<IGameFactory>(new GameFactory(_services.Single<IAssetProvider>()));

            _services.RegisterSingle<ISaveLoadService>(
                new SaveLoadService(_services.Single<IPersistantProgressService>(), _services.Single<IGameFactory>()));
        }

        private IInputService InputService()
        {
            if (Application.isEditor)
                return new StandaloneInputService();

            return new MobileInputService();
        }
    }
}