using _Game.CodeBase.Cameralogic;
using _Game.CodeBase.Infrastructure.Factory;
using _Game.CodeBase.Infrastructure.Services.PersistantProgress;
using _Game.CodeBase.Ui;
using UnityEngine;

namespace _Game.CodeBase.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string PlayerInitialPoint = "PlayerInitialPoint";

        private readonly GameStateMachine _gameStateMachine;
        private readonly IGameFactory _gameFactory;
        private readonly IPersistantProgressService _progressService;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain curtain, IGameFactory gameFactory, IPersistantProgressService progressService)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _gameFactory = gameFactory;
            _progressService = progressService;
        }

        public void Enter(string payload)
        {
            _curtain.Show();
            _gameFactory.Cleanup();
            _sceneLoader.Load(payload, OnLoaded);
        }

        public void Exit()
        {
            _curtain.Hide();
        }

        private void OnLoaded()
        {
            InitGameWorld();
            InformProgressReaders();

            _gameStateMachine.Enter<GameLoopState>();
        }

        private void InformProgressReaders()
        {
            foreach (var progressReader in _gameFactory.ProgressReaders)
            {
                progressReader.LoadProgress(_progressService.PlayerProgress);
            }
        }

        private void InitGameWorld()
        {
            var initialPoint = GameObject.FindGameObjectWithTag(PlayerInitialPoint);
            GameObject hero = _gameFactory.CreateHero(at: initialPoint);

            _gameFactory.CreateHud();

            CameraFollow(hero);
        }

        private static void CameraFollow(GameObject hero)
        {
            Camera.main.GetComponent<CameraFollow>().Follow(hero);
        }
    }
}