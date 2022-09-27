﻿using System;
using System.Numerics;

namespace Zork
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            InitializeRoomDescriptions();
            Console.WriteLine("Welcome to Zork!");

            bool isRunning = true;
            while (isRunning)
            {
                Console.Write($"{_rooms[_location.Row, _location.Column]}\n > ");
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

        private static void InitializeRoomDescriptions()
        {
            _rooms[0, 0].Description = "You are onn a rock-strewn trail.";
            _rooms[0, 1].Description = "You are facing the south side of a white house. There is no door here, and all the windows are barred.";      // Rocky Trail
            _rooms[0, 2].Description = "You are at the top of the Great Canyon on its south wall";                                                      // South of House

            _rooms[1, 0].Description = "This is a forest, with threes in all directions around you.";                                                   // Forest
            _rooms[1, 1].Description = "This is an open field west of a white house, with a boarded front door.";                                       // West of House
            _rooms[1, 2].Description = "You are behind the white house. in one corner of the house there is a small window which is slightly ajar.";    // Behind House

            _rooms[2, 0].Description = "This is a dimly lit forest, with large trees all around. To the east, there appears to be sunlight.";           // Dense Woods
            _rooms[2, 1].Description = "You are facing the north side of a white house. There is no door here, and all the windows are barred.";        // North of House
            _rooms[2, 2].Description = "You are in a clearing, with a froest surrounding yo on the west and south.";                                    // Clearing
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