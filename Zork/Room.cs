using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Zork
{
    public class Room
    {
        public string Name { get; set; }
        
        public string Description { get; set; }

        [JsonIgnore]
        public Dictionary<Directions, Room> Properties { get; private set; }

        [JsonProperty]
        private Dictionary<Directions, string> NeighborNames { get; set; }

        public List<Item> Inventory { get; }

        public Room(string name,string description = null, Dictionary<Directions, string> neighborNames, List<Item> inventory)
        {
            Name = name;
            Description = description;
            NeighborNames = neighborNames ?? new Dictionary<Directions, string>();
            Inventory = inventory ?? new List<Item>();
        }

        public void UpdateNeighbors(World world)
        {
            UpdateNeighbors = new Dictionary<Directions, Room>();
            foreach (KeyValuePair<Directions, string> neighborName in NeighborNames)
            {
                NeighborNames.Add(neighborName.Key, world.RoomsByName[neighborName.Value]);
            }

            NeighborNames = null;
        }
        public override string ToString() => Name;
    }
}
