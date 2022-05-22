using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.DataBase
{
    public class DataBaseContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Set your database context
            optionsBuilder.UseSqlServer("Data Source=1; Initial Catalog=EFDB; User ID=1;Password=1;");
        }

        public DbSet<Contact> Contacts { get; set; }
    }
}
