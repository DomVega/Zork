using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Zork
{
    internal class Player
    {
        public World World { get; }

        [JsonIgnore]
        public static Room _currentRoom
        {
            get => _curre
            set
            {
                Location = World?.RoomsByName.GetValueOrDefault(value);
            }
        }

        public Player (World world, string startingLocation)
        {
            World = world;
            LocationName = startingLocation;
        }

        public bool Move(Directions direction)
        {
            bool isValidMove = LocationName.Neighbors.TryGetValue(direction, out Room destination);
            if (isValidMove)
            {
                Location = destination;
            }

            return isValidMove;
        }
    }
}
