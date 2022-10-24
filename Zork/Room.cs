using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace Zork
{
    public class Room
    {
        public string Name { get; }

        public string Description { get; set; }

        [JsonIgnore]
        public Dictionary<Directions, Room> Neighbors { get; private set; }

        [JsonProperty]
        private Dictionary<Directions, string> NeighborNames { get; set; }

        public List<Item> Inventory { get; private set; }

        [JsonProperty]
        private string[] InventoryNames { get; set; }

        public Room(string name, string description, Dictionary<Directions, string> neighborNames, string[] inventoryNames)
        {
            Name = name;
            Description = description;
            NeighborNames = neighborNames ?? new Dictionary<Directions, string>();
            InventoryNames = inventoryNames ?? new string[0];
        }

        public void UpdateNeighbors(World world)
        {
            Neighbors = new Dictionary<Directions, Room>();
            foreach (var neighborName in NeighborNames)
            {
                Neighbors.Add(neighborName.Key, world.RoomsByName[neighborName.Value]);
            }

            NeighborNames = null;
        }

        public void UpdateInventory(World world)
        {
            Inventory = new List<Item>();
            foreach (var inventoryName in InventoryNames)
            {
                Inventory.Add(world.ItemsByName[inventoryName]);
            }
            InventoryNames = null;
        }
        public override string ToString() => Name;
    }
}
