using _Game.CodeBase.Data;

namespace _Game.CodeBase.Infrastructure.Services.PersistantProgress
{
    public class PersistantProgressService : IPersistantProgressService
    {
        public PlayerProgress PlayerProgress {get; set;}
    }
}