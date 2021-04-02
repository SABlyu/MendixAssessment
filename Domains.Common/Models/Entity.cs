using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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


        [Column("Name")]
        public string Name { get; }

        [Column("X")]
        public int X { get; }

        [Column("Y")]
        public int Y { get; }

        public override void ClearNavigationProperties()
        {
            // do nothing for now...
        }
    }
}
