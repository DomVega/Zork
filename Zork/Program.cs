using System;

namespace Zork
{
    internal class Program
    {
        private static Room CurrentRoom
        {
            get
            {
                return _rooms[_location.Row , _location.Column];
            }
        }


        private static void Main(string[] args)
        {
            //InitializeRoomDescriptions();

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
                        outputString = "This is an open field west of a white house, with a boarded front door.\n A rubber mat saying 'Welcome to Zork!' lies by the door.";
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

                case Commands.North:
                case Commands.South:
                    didMove = false;
                    break;

                case Commands.East when _currentRoom < _rooms.Length - 1:
                    _currentRoom++;
                    didMove = true;
                    break;

                case Commands.West when _currentRoom > 0:
                    _currentRoom--;
                    didMove = true;
                    break;
               
            }

            return didMove;
        }

        private static void InitializeRoomDescriptions()
        {

        }


        private static readonly Room[,] _rooms = 
        {
            { new Room ("Rocky Trail"), new Room ("South of House"), new Room ("Canyon View") } ,
            { new Room ("Forest"), new Room ("West of House"), new Room ("Behind House") } ,
            { new Room ("Dense Woods"), new Room ("North of House"), new Room ("Clearing") }
        };
        private static int _currentRoom = 1;
    }
}