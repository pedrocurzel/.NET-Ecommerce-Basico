using System;
using _net.Models;
using Microsoft.EntityFrameworkCore;

namespace _net.DataContext;

public class MyDataContext(DbContextOptions opts) : DbContext(opts)
{
    public DbSet<UserModel> Users {get; set;}
    public DbSet<AddressModel> Addresses {get;set;}
    public DbSet<ProductModel> Products {get; set;}
    public DbSet<OrderModel> Orders {get;set;}
    public DbSet<OrderItemModel> OrderItems {get; set;}
}
