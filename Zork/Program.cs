using System;

namespace Zork
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Zork!");

            string inputString = Console.ReadLine().ToUpper().Trim();
            Commands command = ToCommand(inputString);

            if (command == Commands.Quit)
            {
                Console.WriteLine("Thank you for playing ");
            }
            else if (command == Commands.North)
            {
                Console.WriteLine("You move north.");
            }
            else if (command == Commands.East)
            {
                Console.WriteLine("You move east.");
            }
            else if (command == Commands.South)
            {
                Console.WriteLine("You move south.");
            }
            else if (command == Commands.West)
            {
                Console.WriteLine("You move west.");
            }
            //Add in north south east and west
            else
            {
                Console.WriteLine("Unrecognized command.");
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

        //static bool IsEvem(int value)
        //{
        //    return value % 2 == 0 ? true : false;
        //
        //    if (value % 2 == 0)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return Commands.Unknown;
        //    }
        //
        //    return Enum.TryParse<Commands>(commandString, true, out Commands command) ? command : Commands.Unknown;
        //}
    }
}
//switch (commandString)
//{
//    case "QUIT":
//        command = Commands.Quit;
//        break;
//
//    case "LOOK":
//        command = Commands.Look;
//        break;
//
//    case "NORTH":
//        command = Commands.North;
//        break;
//
//    case "SOUTH":
//        command = Commands.South;
//        break;
//
//    case "EAST":
//        command = Commands.East;
//        break;
//
//    case "WEST":
//        command = Commands.West;
//        break;
//
//    default:
//        command = Commands.Unknown;
//        break;
//}
