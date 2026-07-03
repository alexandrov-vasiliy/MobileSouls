using _Game.CodeBase.Data;

namespace _Game.CodeBase.Hero
{
    public interface ISavedProgressReader
    {
        void LoadProgress(PlayerProgress playerProgress);
    }

    public interface ISavedProgress : ISavedProgressReader
    {
        void UpdateProgress(PlayerProgress playerProgress);
    }
}