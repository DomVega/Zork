using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Numerics;

namespace Zork
{
    public class Program
    {
        static void Main(string[] args) 
        {
            const string defaultRoomsFilename = @"Content\Game.json";
            string gameFilename = (args.Length > 0 ? args[(int)CommandLineArguments.GameFilename] : defaultRoomsFilename);
            Game game = JsonConvert.DeserializeObject<Game>(File.ReadAllText(gameFilename));

            Console.WriteLine("Welcome to Zork!");
            game.Run();
            Console.WriteLine("Finished");   
        }
        private enum CommandLineArguments
        {
            GameFilename = 0
        }
    }
}