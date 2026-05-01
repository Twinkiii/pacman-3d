using Pacman.Common;

namespace Pacman.Core.Interfaces
{
    public interface IGameStateListener
    {
        void OnGameStateChanged(GameState state);
    }
}