namespace Pacman
{
    public interface IGameStateListener
    {
        void OnGameStateChanged(GameState state);
    }
}