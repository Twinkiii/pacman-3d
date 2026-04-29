namespace Pacman
{
    public interface ICollectible
    {
        int ScoreValue { get; }
        bool IsCollected { get; }
        void Collect();
    }
}