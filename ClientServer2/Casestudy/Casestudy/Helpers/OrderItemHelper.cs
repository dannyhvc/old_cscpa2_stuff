using Casestudy.DAL.DomainClasses;

namespace Casestudy.Helpers
{
    public class OrderItemHelper
    {
        public string Id { get; set; }
        public Product? Item { get; set; }
        public int Qty { get; set; }
    }
}
