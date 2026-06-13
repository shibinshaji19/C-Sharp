using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

namespace MarvelDcChess
{
    internal class LoadGame
    {
        public static GameData Load(string filePath)
        {
            if (!File.Exists(filePath))
                return null;

            string json = File.ReadAllText(filePath);

            return JsonSerializer.Deserialize<GameData>(json);
        }
    }
}