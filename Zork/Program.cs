﻿using System;

namespace Zork
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Zork!");

            bool isRunning = true;
            while (isRunning)
            {
                Console.Write("> ");
                string inputString = Console.ReadLine().ToUpper().Trim();
                Commands command = ToCommand(inputString);

                string outputString;
                switch (command)
                {
                    case Commands.Quit:
                        isRunning = false;
                        outputString = "Thank you for playing!";
                        break;

                    case Commands.Look:
                        outputString = "This is an open field west of a white house, with a boarded front door.\n A rubber mat saying 'Welcome to Zork!' lies by the door.";
                        break;

                    case Commands.North:
                    case Commands.South:                     
                    case Commands.East:                    
                    case Commands.West:
                        outputString = $"You moved {command}.";
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
