namespace EconomicManagementAPP.Models
{
    public class Categories
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int OperationTypeId { get; set; }
        public string OperationType { get; set; }
        public int UserId { get; set; }
    }
}
