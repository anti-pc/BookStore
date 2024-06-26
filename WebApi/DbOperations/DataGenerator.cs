using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.DbOperations{

    public class DataGenerator{

        public static void Initialize(IServiceProvider serviceProvider)
        {
             using (var context = new BookStoreDbContext(
        serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if(context.Books.Any())
                {
                    return;
                }

                context.Books.AddRange(           
                    new Book
                    {
                        //Id=1,
                        Title="Lean Startup",
                        GenreId=1,
                        PageCount=200,
                        PublishDate=new DateTime(2021,06,12)
                    },

                    new Book{
                        //Id=2,
                        Title="Herland",
                        GenreId=2,
                        PageCount=250,
                        PublishDate=new DateTime(2021,05,23)
                    },

                    new Book{
                        //Id=3,
                        Title="Dune",
                        GenreId=2,
                        PageCount=540,
                        PublishDate=new DateTime(2021,12,21)
                    }
                );

                context.SaveChanges();
            }
        }
    }
}