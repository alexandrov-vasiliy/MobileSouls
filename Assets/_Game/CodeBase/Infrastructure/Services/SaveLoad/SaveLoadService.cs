using _Game.CodeBase.Data;
using _Game.CodeBase.Hero;
using _Game.CodeBase.Infrastructure.Factory;
using _Game.CodeBase.Infrastructure.Services.PersistantProgress;
using UnityEngine;

namespace _Game.CodeBase.Infrastructure.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string ProgressKey = "progress";
        
        private readonly IPersistantProgressService _progressService;
        private readonly IGameFactory _gameFactory;


        public SaveLoadService(IPersistantProgressService progressService, IGameFactory gameFactory)
        {
            _progressService = progressService;
            _gameFactory = gameFactory;
        }
        
        public void SaveProgress()
        {
            foreach (ISavedProgress progressWriter in _gameFactory.ProgressWriters)
            {
                progressWriter.UpdateProgress(_progressService.PlayerProgress);
            }
            
            PlayerPrefs.SetString(ProgressKey, _progressService.PlayerProgress.ToJson());
        }

        public PlayerProgress LoadProgress() => PlayerPrefs.GetString(ProgressKey)?.ToDeserialize<PlayerProgress>();
    }
}