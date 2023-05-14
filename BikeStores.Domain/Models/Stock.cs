using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BikeStores.Domain.Models
{
    public partial class Stock
    {
        public int StoreId { get; set; }
        public int ProductId { get; set; }
        public int? Quantity { get; set; }
        [JsonIgnore]
        public virtual Product Product { get; set; }
        [JsonIgnore]
        public virtual Store Store { get; set; }
    }
}
