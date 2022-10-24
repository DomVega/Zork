﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Zork
{
    public class Game
    {
        public World World { get; }

        public Player Player { get; }

        public Game(World world, string startingLocation)
        {
            World = world;
            Player = new Player(World, startingLocation);
        }

        public void Run()
        {
            Room previousRoom = null;
            bool isRunning = true;
            while (isRunning)
            {
                Console.WriteLine(Player._currentRoom);//Player.CurrentRoom?
                if (previousRoom != Rooms[_location.Row, _location.Column])// != Player.CurrentRoom
                {
                    Console.WriteLine(Rooms[_location.Row, _location.Column].Description);
                    previousRoom = Rooms[_location.Row, _location.Column];
                }

                Console.Write(" > ");

                
                Commands command = ToCommand(Console.ReadLine().Trim());

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
                        if (Player.Move(direction))
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
    }
}