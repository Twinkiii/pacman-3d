namespace Pacman.Core.Interfaces
{
    public interface IEnemy
    {
        void StartChase();
        void StopChase();
        void ResetToSpawn();
    }
}