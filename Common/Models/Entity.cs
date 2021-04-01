using System;
using System.Collections.Generic;
using System.Text;

namespace Domains.Common.Models
{
    public class Entity : DbItem
    {
        public Entity()
        {
        }

        public Entity(int id, string name, int x, int y)
        {
            Id = id;
            Name = name;
            X = x;
            Y = y;
        }


        public string Name { get; }

        public int X { get; }

        public int Y { get; }
    }
}
