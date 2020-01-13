using SportGoods.Domain.Entities;
using System.Collections.Generic;


namespace SportGoods.Domain.Abstract
{
    public interface ISportProductRepository
    {
        IEnumerable<SportProduct> Products { get; }
    }
}
