using System;

namespace Zork
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Zork!");

            Game game = new Game();
            game.Run();















            //InitializeRoomDescriptions();

            //    bool isRunning = true;
            //    while (isRunning)
            //    {
            //        Console.Write($"{_rooms[Location.Row, Location.Column]}\n > ");
            //        string inputString = Console.ReadLine().Trim();
            //        Commands command = ToCommand(inputString);

            //        string outputString;
            //        switch (command)
            //        {
            //            case Commands.Quit:
            //                isRunning = false;
            //                outputString = "Thank you for playing!";
            //                break;

            //            case Commands.Look:
            //                outputString = "This is an open field west of a white house, with a boarded front door.\n A rubber mat saying 'Welcome to Zork!' lies by the door.";
            //                break;

            //            case Commands.North:
            //            case Commands.South:                     
            //            case Commands.East:                    
            //            case Commands.West:
            //                if (Move(command))
            //                {
            //                    outputString = $"You moved {command}.";
            //                }
            //                else
            //                {
            //                    outputString = "The way is shut!";
            //                }
            //                break;

            //            default:
            //                outputString = "Unknown command.";
            //                break;
            //        }

            //        Console.WriteLine(outputString);

            //    }
            //}

            //static Commands ToCommand(string commandString)
            //{
            //    //Commands command;
            //    if (Enum.TryParse<Commands>(commandString, true, out Commands command))
            //    {
            //        return command;
            //    }
            //    else
            //    {
            //        return Commands.Unknown;
            //    }

            //}

            //public  bool Move(Commands command)
            //{
            //    bool didMove = false;

            //    switch (command)
            //    {

            //        case Commands.North when Location.Row < _rooms.GetLength(0) - 1:
            //            Location.Row++;
            //            didMove = true;
            //            break;
            //        case Commands.South when Location.Row > 0:
            //            Location.Row--;
            //            didMove = true;
            //            break;

            //        case Commands.East when Location.Column < _rooms.GetLength(1) - 1:
            //            Location.Column++;
            //            didMove = true;
            //            break;

            //        case Commands.West when Location.Column > 0:
            //            Location.Column--;
            //            didMove = true;
            //            break;

            //    }

            //    return didMove;
            //}

            //    private static void InitializeRoomDescriptions()
            //    {
            //        _rooms[0, 0].Description = "You are onn a rock-strewn trail.";
            //        _rooms[0, 1].Description = "You are facing the south side of a whir]te house. There is no door here, and all the windows are barred.";      // Rocky Trail
            //        _rooms[0, 2].Description = "You are at the top of the Great Canyon on its south wall";                                                      // South of House

            //        _rooms[1, 0].Description = "This is a forest, with threes in all directions around you.";                                                   // Forest
            //        _rooms[1, 1].Description = "This is an open field west of a white house, with a boarded front door.";                                       // West of House
            //        _rooms[1, 2].Description = "You are behind the white house. in one corner of the house there is a small window which is slightly ajar.";    // Behind House

            //        _rooms[2, 0].Description = "This is a dimly lit forest, with large trees all around. To the east, there appears to be sunlight.";           // Dense Woods
            //        _rooms[2, 1].Description = "You are facing the north side of a white house. There is no door here, and all the windows are barred.";        // North of House
            //        _rooms[2, 2].Description = "You are in a clearing, with a froest surrounding yo on the west and south.";                                    // Clearing
            //    }

            //    private static (int Row, int Column) Location = (1,1 ); 
            //}
        }