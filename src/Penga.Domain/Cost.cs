namespace Penga.Domain
{
    public class Cost
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateOnly? Date { get; set; }
        public decimal Amount { get; set; }
        public int? CostCategoryId { get; set; }
        public CostCategory CostCategory { get; set; }

        public Cost(string name, string description, DateOnly? date, decimal amount, int? costCategoryId)
        {
            Name = name;
            Description = description;
            Date = date;
            Amount = amount;
            CostCategoryId = costCategoryId;
        }

        public void Update(string name, string description, DateOnly? date, decimal amount, int? costCategoryId)
        {
            Name = name;
            Description = description;
            Date = date;
            Amount = amount;
            CostCategoryId = costCategoryId;
        }
    }
}
