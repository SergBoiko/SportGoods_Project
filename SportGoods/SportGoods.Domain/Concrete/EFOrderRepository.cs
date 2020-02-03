using SportGoods.Domain.Abstract;
using SportGoods.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportGoods.Domain.Concrete
{
    public class EFOrderRepository : IOrderRepository
    {
        EFDbContext context = new EFDbContext();

        public IEnumerable<Order> Orders
        {
            get { return context.Orders; }
        }

        public void SaveOrder(Product product, string userId)
        {
            var orderProduct = context.Products.FirstOrDefault(p => p.Id == product.Id); ;
            var orderUser = context.Users.FirstOrDefault(u => u.Id == userId);

            var order = new Order()
            {
                OrderDate = DateTime.Now,
                Product = orderProduct,
                User = orderUser
            };

            context.Orders.Add(order);
            context.SaveChanges();
        }

        
    }
}
