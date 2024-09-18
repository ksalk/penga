namespace Penga.Domain
{
    public class CostCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public CostCategory(string name)
        {
            Name = name;
        }
    }
}
