using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;

namespace Zork
{
    internal class Program
    {
        private static readonly Dictionary<string, Room> RoomMap;

        private static void Main(string[] args)
        {

            string roomsFilename = "Rooms.txt";
            InitializeRoomDescriptions(roomsFilename);
            Console.WriteLine("Welcome to Zork!");


            Room previousRoom = null;
            bool isRunning = true;
            while (isRunning)
            {
                Console.WriteLine(_rooms[_location.Row, _location.Column].ToString());
                if (previousRoom != _rooms[_location.Row, _location.Column])
                {
                    Console.WriteLine(_rooms[_location.Row, _location.Column].Description);
                    previousRoom = _rooms[_location.Row, _location.Column];
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
                        outputString = $"{_rooms[_location.Row, _location.Column].Description}";
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

                case Commands.North when _location.Row < _rooms.GetLength(0) - 1:
                    _location.Row++;
                    didMove = true;
                    break;
                case Commands.South when _location.Row > 0:
                    _location.Row--;
                    didMove = true;
                    break;

                case Commands.East when _location.Column < _rooms.GetLength(1) - 1:
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

        static Program()
        {
            RoomMap = new Dictionary<string, Room>();
            foreach (Room room in _rooms)
            {
                RoomMap.Add(room.Name, room);
            }
        }

        private enum Fields
        {
            Name = 0,
            Description
        }
        private static void InitializeRoomDescriptions(string roomsFilename)
        {
            const string fieldDelimiter = "##";
            const int expectedFieldCount = 2;

            string[] lines = File.ReadAllLines(roomsFilename);
            foreach (string line in lines)
            {
                string[] fields = line.Split(fieldDelimiter);
                if (fields.Length != expectedFieldCount)
                {
                    throw new InvalidDataException("Invalid record.");
                }

                string name = fields[(int)Fields.Name];
                string description = fields[(int)Fields.Description];

                RoomMap[name].Description = description;
            }                            
        }

        private static readonly Room[,] _rooms =
       {
                    { new Room ("Rocky Trail"), new Room("South of House"), new Room("Canyon View") },
                    { new Room("Forest"), new Room("West of House"), new Room("Behind House") },
                    { new Room("Dense Woods"), new Room("North of House"), new Room("Clearing") }
                };

        private static (int Row, int Column) _location = (1, 1);
    }
}