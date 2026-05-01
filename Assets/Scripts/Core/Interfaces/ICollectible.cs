namespace Pacman.Core.Interfaces
{
    public interface ICollectible
    {
        int ScoreValue { get; }
        bool IsCollected { get; }
        void Collect();
    }
}