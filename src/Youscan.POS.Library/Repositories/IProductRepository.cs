using System.Collections.Generic;
using Youscan.POS.Library.Aggregates.ProductAggregate;

namespace Youscan.POS.Library.Repositories
{
    internal interface IProductRepository : IEnumerable<IProduct>
    {
        IProduct Create(string name);
        IProduct Get(string name);
        bool TryGet(string name, out IProduct product);
    }
}
