using _Game.CodeBase.Data;

namespace _Game.CodeBase.Infrastructure.Services.PersistantProgress
{
    public interface IPersistantProgressService : IService
    {
        PlayerProgress PlayerProgress { get; set; }
    }
}