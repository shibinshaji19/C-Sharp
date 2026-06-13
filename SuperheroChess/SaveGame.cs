using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

namespace MarvelDcChess
{
    internal class SaveGame
    {
        public static void Save(string filePath, Board board, bool isMarvelTurn)
        {
            GameData data = new GameData
            {
                Board = board.GetBoardStateJagged(),
                IsMarvelTurn = isMarvelTurn
            };

            string json = JsonSerializer.Serialize(data, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(filePath, json);
        }
    }
}