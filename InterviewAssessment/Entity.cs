namespace DomainModelEditor
{
    public class Entity
    {
        public Entity(int id, string name, int x, int y)
        {
            Id = id;
            Name = name;
            X = x;
            Y = y;
        }

        public int Id { get; }

        public string Name { get; }

        public int X { get; }

        public int Y { get; }
    }
}
