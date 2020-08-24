using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data

{
    public class DataContext : DbContext
    {
        //base(options) ile inherit ettigimiz class in option larini cekmis oluyoruz
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        //DataContext e Models klasorundeki Value veriliyor
        //Asagidaki Values, tablo adi olacak
        public DbSet<Value> Values { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Photo> Photos { get; set; }

    }
}