using SportGoods.Domain.Entities;
using System.Collections.Generic;


namespace SportGoods.Domain.Abstract
{
    public interface ISportProductRepository
    {
        IEnumerable<Product> Products { get; }
        void SaveProduct(Product product);
    }
}
