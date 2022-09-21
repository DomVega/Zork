using System;
using System.Collections.Generic;
using System.Text;

namespace Zork
{
    internal class Game
    {
        public World World { get; set; }

        public Player Player { get; set; }

        public void Run()
        {

            InitializeRoomDescriptions();

            Room previousRoom = null;
            bool isRunning = true;
            while (isRunning)
            {
                Console.WriteLine(Player.CurrentRoom.ToString());
                if(previousRoom != Player.CurrentRoom)
                {
                    Console.WriteLine()
                }
                Console.Write($"{_rooms[Location.Row, Location.Column]}\n > ");
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
                        if Player.(Move(command))
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

        private static Commands ToCommand(string commandString)
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

                case Commands.North when Location.Row < _world.Rooms.GetLength(0) - 1:
                    Location.Row++;
                    didMove = true;
                    break;
                case Commands.South when Location.Row > 0:
                    Location.Row--;
                    didMove = true;
                    break;

                case Commands.East when Location.Column < _world.Rooms.GetLength(1) - 1:
                    Location.Column++;
                    didMove = true;
                    break;

                case Commands.West when Location.Column > 0:
                    Location.Column--;
                    didMove = true;
                    break;

            }

            return didMove;
        }

        private void InitializeRoomDescriptions()
        {
            var roomMap = new Dictionary<string, Room>();
            foreach (Room room in _rooms)
            {
                roomMap.Add(room.Name, room);
            }
                {
                    _rooms[0, 0].Description = "You are onn a rock-strewn trail.";
                    _rooms[0, 1].Description = "You are facing the south side of a whir]te house. There is no door here, and all the windows are barred.";      // Rocky Trail
                    _rooms[0, 2].Description = "You are at the top of the Great Canyon on its south wall";                                                      // South of House

                    _rooms[1, 0].Description = "This is a forest, with threes in all directions around you.";                                                   // Forest
                    _rooms[1, 1].Description = "This is an open field west of a white house, with a boarded front door.";                                       // West of House
                    _rooms[1, 2].Description = "You are behind the white house. in one corner of the house there is a small window which is slightly ajar.";    // Behind House

                    _rooms[2, 0].Description = "This is a dimly lit forest, with large trees all around. To the east, there appears to be sunlight.";           // Dense Woods
                    _rooms[2, 1].Description = "You are facing the north side of a white house. There is no door here, and all the windows are barred.";        // North of House
                    _rooms[2, 2].Description = "You are in a clearing, with a froest surrounding yo on the west and south.";                                    // Clearing
                }
        }

        private static (int Row, int Column) Location = (1, 1);
    }
}
