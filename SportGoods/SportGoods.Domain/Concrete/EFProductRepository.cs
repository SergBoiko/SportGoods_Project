using SportGoods.Domain.Abstract;
using SportGoods.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportGoods.Domain.Concrete
{
    public class EFProductRepository : ISportProductRepository
    {
        EFDbContext context = new EFDbContext();

        public IEnumerable<Product> Products
        {
            get { return context.Products; }
        }

        public void SaveProduct(Product product)
        {
            if (product.Id == 0)
                context.Products.Add(product);
            else
            {
                Product dbEntry = context.Products.Find(product.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = product.Name;
                    dbEntry.Description = product.Description;
                    dbEntry.Price = product.Price;
                    dbEntry.Category = product.Category;
                }
            }
            context.SaveChanges();
        }

        public Product DeleteProduct(int id)
        {
            Product dbEntry = context.Products.Find(id);
            if (dbEntry != null)
            {
                context.Products.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
