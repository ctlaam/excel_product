namespace InsertProduct.Models
{
    public class Product
    {
        public Guid id { get; set; }
        public string productCode { get; set; }
        public string productName { get; set; }
        public DateTime timeProduct { get; set; }
        public string description { get; set; }
    }
}
