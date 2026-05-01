using System;
using System.IO;
using UnityEngine;

namespace Pacman.Core.Infrastructure
{
    [Serializable]
    public class Saver<T>
    {
        public static void TryLoad(string filename, ref T data)
        {
            var path = FileHandler.Path(filename);
            if (File.Exists(path))
            {
                var dataString= File.ReadAllText(path);
                var saver = JsonUtility.FromJson<Saver<T>>(dataString);
                data = saver.data;
            }
        }

        internal static void Save(string filename, T data)
        {
            var wrapper = new Saver<T> { data = data };
            var dataString = JsonUtility.ToJson(wrapper);
            File.WriteAllText(FileHandler.Path(filename), dataString);
        }

        

        public T data;
        
    }

    public static class FileHandler
    {
        public static string Path(string filename)
        {
            return $"{Application.persistentDataPath}/{filename}";
        }

        public static void Reset(string filename)
        {
            if (File.Exists(FileHandler.Path(filename)))
            {
                File.Delete(FileHandler.Path(filename));
            }
        }

        public static bool isEmptyFile(string filename)
        {
            if (File.Exists(Path(filename)))
                return false;
            else 
                return true;
        }
    }
}