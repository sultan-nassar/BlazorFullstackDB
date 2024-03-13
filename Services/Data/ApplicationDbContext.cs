using BlazorFull.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;


namespace BlazorFull.Services.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<ToDoModel> Todos {  get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        //protected override async void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    modelBuilder.Entity<ToDoModel>().HasData(
        //        new ToDoModel { Id = Guid.NewGuid(), Title = "buy a Laptop" },
        //        new ToDoModel { Id = Guid.NewGuid(), Title = "send a message" },
        //        new ToDoModel { Id = Guid.NewGuid(), Title = "Do homework" }
        //    );

        //}
    }
}
