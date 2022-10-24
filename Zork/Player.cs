﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Zork
{
    internal class Player
    {
        public Room CurrentRoom
        {
            get => _currentRoom;
            set => _currentRoom = value;
        }

        public Item[] Inventory { get; }

        public Player (World world, string startingLocation)
        {
            _world = world;
            
            if (_world.RoomsByName.TryGetValue(startingLocation, out _currentRoom) == false)
            {
                throw new Exception($"Invalid starting location: {startingLocation}");
            }
        }

        public bool Move(Directions direction)
        {
            bool didMove = CurrentRoom.Neighbors.TryGetValue(direction, out Room destination);
            if (didMove)
            {
                CurrentRoom = neighbor;
            }

            return didMove;
        }

        private World _world;
        private Room _currentRoom;
    }
}
