using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HSIS_Web.Models
{
    public class HSISWebContext : DbContext
    {
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Storage> Storages { get; set; }
        public DbSet<Shell> Shells { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Assistant> Assistants { get; set; }
    }
}