using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BikeStores.Domain.Models
{
    public partial class Product
    {
        public Product()
        {
            OrderItems = new HashSet<OrderItem>();
            Stocks = new HashSet<Stock>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        public short ModelYear { get; set; }
        public decimal ListPrice { get; set; }
        [JsonIgnore]
        public virtual Brand Brand { get; set; }
        [JsonIgnore]
        public virtual Category Category { get; set; }
        [JsonIgnore]
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        [JsonIgnore]
        public virtual ICollection<Stock> Stocks { get; set; }
    }
}
