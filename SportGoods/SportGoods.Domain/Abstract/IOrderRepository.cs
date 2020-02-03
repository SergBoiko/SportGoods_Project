using SportGoods.Domain.Entities;
using System;
using System.Collections.Generic;


namespace SportGoods.Domain.Abstract
{
    public interface IOrderRepository
    {
        IEnumerable<Order> Orders { get; }
        void SaveOrder(Product p, string userId);
        
    }
}
