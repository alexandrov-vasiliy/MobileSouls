using System;
using _Game.CodeBase.Data;
using _Game.CodeBase.Infrastructure.Services.PersistantProgress;
using _Game.CodeBase.Infrastructure.Services.SaveLoad;

namespace _Game.CodeBase.Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IPersistantProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;

        public LoadProgressState(GameStateMachine gameStateMachine, IPersistantProgressService progressService,
            ISaveLoadService saveLoadService)
        {
            _gameStateMachine = gameStateMachine;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        public void Enter()
        {
            LoadProgressOrInitNew();
            string level = _progressService.PlayerProgress.WorldData.PositionOnLevel.Level;

            _gameStateMachine.Enter<LoadLevelState, string>(level);
        }

        private void LoadProgressOrInitNew()
        {
            _progressService.PlayerProgress = _saveLoadService.LoadProgress() ?? NewProgress();
        }

        private PlayerProgress NewProgress() => new(initialLevel: "Main");

        public void Exit()
        {
        }
    }
}