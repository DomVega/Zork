using System;
using System.Collections.Generic;
using System.Text;

namespace Zork
{
    internal class Player
    {
        public Room CurrentRoom
        {
            get
            {
                return _world.Rooms[Location.Row, Location.Column];

            }
        }

        public int Score { get; }

        public int Moves { get; }

        public Player(World world)
        {
            _world = world;
        }

        public bool Move(Commands command)
        {
            bool didMove = false;

            switch (command)
            {

                case Commands.North when Location.Row < _rooms.GetLength(0) - 1:
                    Location.Row++;
                    didMove = true;
                    break;
                case Commands.South when Location.Row > 0:
                    Location.Row--;
                    didMove = true;
                    break;

                case Commands.East when Location.Column < _rooms.GetLength(1) - 1:
                    Location.Column++;
                    didMove = true;
                    break;

                case Commands.West when Location.Column > 0:
                    Location.Column--;
                    didMove = true;
                    break;

            }

        private World _world;
        private static (int Row, int Column) Location = (1, 1);
    }
}
