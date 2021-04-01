using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DomainModelEditor
{
    public class EntityStore : ObservableCollection<Entity>
    {
        private static int _uniqueId = 0;

        public void Load(IEnumerable<Tuple<int, string, int, int>> entities)
        {
            Clear();
            foreach (var e in entities)
            {
                Add(new Entity(e.Item1, e.Item2, e.Item3, e.Item4));
            }
        }

        public void Add(string name, int x , int y)
        {
            Add(new Entity(_uniqueId++, name, x, y));
        }
    }
}
