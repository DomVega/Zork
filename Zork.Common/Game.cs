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

        public IInputService Input { get; private set; }

        public IOutputService Output { get; private set; }

        public bool IsRunning { get; private set; }

        public Game(World world, string startingLocation)
        {
            World = world;
            Player = new Player(World, startingLocation);
        }



        public void Run(IInputService input, IOutputService output)
        {
            Input = input ?? throw new ArgumentNullException(nameof(input));
            Output = output ?? throw new ArgumentNullException(nameof(output));

            Input.InputReceived += OnInputReceived;
            IsRunning = true;
            Output.WriteLine(Player.CurrentRoom);
            Output.WriteLine(Player.CurrentRoom.Description);
        }

        private void OnInputReceived(object sender, string inputString)
        {

            Commands command = ToCommand(inputString);

            Room previousRoom = Player.CurrentRoom;
            string outputString;
            switch (command)
            {
                case Commands.Quit:
                    IsRunning = false;
                    outputString = "Thank you for playing!";
                    break;

                case Commands.Look:
                    outputString = Player.CurrentRoom.Description;
                    Output.WriteLine(outputString);
                    foreach (Item item in Player.CurrentRoom.Inventory)
                    {
                        outputString = $"{item.Description}";
                        Output.WriteLine(outputString);

                    }
                    outputString = null;
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
                        Output.WriteLine(outputString);
                    }
                    if (outputString == null)
                    {
                        outputString = "You are empty-handed!";
                        Output.WriteLine(outputString);
                    }
                    outputString = null;
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
