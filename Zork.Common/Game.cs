using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Zork.Common
{
    public class Game
    {
        public World World { get; }

        public Player Player { get; }

        public Room Room { get; }

        public Item Item { get; }

        public IOutputService Output { get; private set; }

        public Game(World world, string startingLocation)
        {
            World = world;
            Player = new Player(World, startingLocation);
        }

        public void Run(IOutputService output)
        {
            Output = output;

            Room previousRoom = null;
            bool isRunning = true;
            while (isRunning)
            {
                Output.WriteLine(Player.CurrentRoom);
                if (previousRoom != Player.CurrentRoom)
                {
                    Output.WriteLine(Player.CurrentRoom.Description);
                    previousRoom = Player.CurrentRoom;
                }

                Output.Write("> ");

                string inputString = Console.ReadLine().Trim();
                // might look like:  "LOOK", "TAKE MAT", "QUIT"
                char separator = ' ';
                string[] commandTokens = inputString.Split(separator);

                string verb = null;
                string subject = null;
                if (commandTokens.Length == 0)
                {
                    continue;
                }
                else if (commandTokens.Length == 1)
                {
                    verb = commandTokens[0];

                }
                else
                {
                    verb = commandTokens[0];
                    subject = commandTokens[1];
                }

                Commands command = ToCommand(verb);
                string outputString;
                switch (command)
                {
                    case Commands.Quit:
                        isRunning = false;
                        outputString = "Thank you for playing!";
                        break;

                    case Commands.Look:
                        foreach (Item item in Player.CurrentRoom.Inventory)
                        {
                            outputString = $"{item.Description}";
                            Output.WriteLine(outputString);
                            
                        }
                        outputString = Player.CurrentRoom.Description;
                        break;

                    case Commands.North:
                    case Commands.South:
                    case Commands.East:
                    case Commands.West:
                        Directions direction = (Directions)command;
                        if (Player.Move(direction))
                        {
                            outputString = $"You moved {direction}.";
                        }
                        else
                        {
                            outputString = "The way is shut!";
                        }
                        break;

                    case Commands.Take:
                        Item itemToAdd = null;
                        foreach (Item item in Player.CurrentRoom.Inventory)
                        {
                            if (string.Compare(item.Name, subject, ignoreCase: true) == 0)
                            {
                                itemToAdd = item;
                                break;
                            }
                        }

                        bool itemIsInRoomInventory = false;
                        foreach (Item item in Player.CurrentRoom.Inventory)
                        {
                            if (item == itemToAdd)
                            {
                                itemIsInRoomInventory = true;
                                break;
                            }
                        }

                        if (itemIsInRoomInventory == false)
                        {
                            Output.WriteLine("You can't see any such thing");
                        }
                        else
                        {

                            Player.CurrentRoom.Inventory.Remove(itemToAdd);
                            Player.Inventory.Add(itemToAdd);
                            Output.WriteLine($"{subject} taken");
                        }

                
                outputString = null;
                        break;

                    case Commands.Drop:
                        
                        Item itemToDrop = null;
                        foreach (Item item in World.Items)
                        {
                            if (string.Compare(item.Name, subject, ignoreCase: true) == 0)
                            {
                                itemToDrop = item;
                                break;
                            }
                        }
                        if (itemToDrop == null)
                        {
                            throw new ArgumentException("No such item exists.");
                        }

                        bool itemIsInPlayerInventory = false;
                        foreach (Item item in Player.Inventory)
                        {
                            if (item == itemToDrop)
                            {
                                itemIsInPlayerInventory = true;
                                break;
                            }
                        }

                        if (itemIsInPlayerInventory == false)
                        {
                            Output.WriteLine("I can't see any such thing");
                        }
                        else
                        {
                            Player.CurrentRoom.Inventory.Add(itemToDrop);
                            Player.Inventory.Remove(itemToDrop);
                            Output.WriteLine($"{subject} dropped");
                        }
                        outputString = null;
                        break;

                    case Commands.Inventory:
                        outputString = null;
                        foreach (Item item in Player.Inventory)
                        {
                            outputString = $"{item.Description}";
                        }
                        if (outputString == null)
                        {
                            outputString = "You are empty-handed!";
                        }
                        break;

                    default:
                        outputString = "Unknown command.";
                        break;
                }

                Output.WriteLine(outputString);
            }
        }

        private static Commands ToCommand(string commandString) => Enum.TryParse(commandString, true, out Commands result) ? result : Commands.Unknown;
    }
}
