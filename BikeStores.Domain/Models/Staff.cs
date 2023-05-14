using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BikeStores.Domain.Models
{
    public partial class Staff
    {
        public Staff()
        {
            InverseManager = new HashSet<Staff>();
            Orders = new HashSet<Order>();
        }

        public int StaffId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public byte Active { get; set; }
        public int StoreId { get; set; }
        public int? ManagerId { get; set; }

        public virtual Staff Manager { get; set; }
        public virtual Store Store { get; set; }
        [JsonIgnore]
        public virtual ICollection<Staff> InverseManager { get; set; }
        [JsonIgnore]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
