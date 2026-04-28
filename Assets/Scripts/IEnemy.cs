namespace Pacman
{
    public interface IEnemy
    {
        void StartChase();
        void StopChase();
        void ResetToSpawn();
    }
}