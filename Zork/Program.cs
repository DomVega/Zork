using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Numerics;

namespace Zork
{
    internal class Program
    {
        private static void Main(string[] args)
        {

            string roomsFilename = @"Content\Rooms.json";
            InitializeRooms(roomsFilename);
            Console.WriteLine("Welcome to Zork!");


            Room previousRoom = null;
            bool isRunning = true;
            while (isRunning)
            {
                Console.WriteLine(Rooms[_location.Row, _location.Column].ToString());
                if (previousRoom != Rooms[_location.Row, _location.Column])
                {
                    Console.WriteLine(Rooms[_location.Row, _location.Column].Description);
                    previousRoom = Rooms[_location.Row, _location.Column];
                }
                Console.Write(" > ");
                string inputString = Console.ReadLine().Trim();
                Commands command = ToCommand(inputString);

                string outputString;
                switch (command)
                {
                    case Commands.Quit:
                        isRunning = false;
                        outputString = "Thank you for playing!";
                        break;

                    case Commands.Look:
                        outputString = $"{Rooms[_location.Row, _location.Column].Description}";
                        break;

                    case Commands.North:
                    case Commands.South:
                    case Commands.East:
                    case Commands.West:
                        if (Move(command))
                        {
                            outputString = $"You moved {command}.";
                        }
                        else
                        {
                            outputString = "The way is shut!";
                        }
                        break;

                    default:
                        outputString = "Unknown command.";
                        break;
                }

                Console.WriteLine(outputString);

            }
        }

        static Commands ToCommand(string commandString)
        {
            //Commands command;
            if (Enum.TryParse<Commands>(commandString, true, out Commands command))
            {
                return command;
            }
            else
            {
                return Commands.Unknown;
            }

        }

        private static bool Move(Commands command)
        {
            bool didMove = false;

            switch (command)
            {

                case Commands.North when _location.Row < Rooms.GetLength(0) - 1:
                    _location.Row++;
                    didMove = true;
                    break;
                case Commands.South when _location.Row > 0:
                    _location.Row--;
                    didMove = true;
                    break;

                case Commands.East when _location.Column < Rooms.GetLength(1) - 1:
                    _location.Column++;
                    didMove = true;
                    break;

                case Commands.West when _location.Column > 0:
                    _location.Column--;
                    didMove = true;
                    break;

            }

            return didMove;
        }

        private enum Fields
        {
            Name = 0,
            Description
        }
        private static void InitializeRooms(string roomsFilename) =>
            Rooms = JsonConvert.DeserializeObject<Room[,]>(File.ReadAllText(roomsFilename));
        

        private static Room[,] Rooms;

        private static (int Row, int Column) _location = (1, 1);
    }
}