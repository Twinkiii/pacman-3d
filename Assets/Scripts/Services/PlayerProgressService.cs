using Pacman.Core.Infrastructure;
using Pacman.Models;

namespace Pacman.Services
{
    public static class PlayerProgressService
    {
        private const string FileName = "player_progress.json";

        public static PlayerProgressModel Load()
        {
            var data = new PlayerProgressData();
            Saver<PlayerProgressData>.TryLoad(FileName, ref data);
            return new PlayerProgressModel(data);
        }

        public static void Save(PlayerProgressModel model)
        {
            Saver<PlayerProgressData>.Save(FileName, model.GetData());
        }

        public static void Reset()
        {
            FileHandler.Reset(FileName);
        }
    }
}