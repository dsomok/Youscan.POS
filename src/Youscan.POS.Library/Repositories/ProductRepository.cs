using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Youscan.POS.Library.Aggregates.ProductAggregate;

namespace Youscan.POS.Library.Repositories
{
    internal class ProductRepository : IProductRepository
    {
        private readonly IList<IProduct> _products;


        public ProductRepository()
            : this(new List<IProduct>())
        {
        }

        public ProductRepository(IEnumerable<IProduct> products)
        {
            this._products = products.ToList();
        }


        public IProduct Create(string name)
        {
            if (this.TryGet(name, out var existingProduct))
            {
                return existingProduct;
            }

            var newProduct = new Product(name);
            this._products.Add(newProduct);

            return newProduct;
        }

        public IProduct Get(string name)
        {
            return this._products.SingleOrDefault(p => p.Name == name);
        }

        public bool TryGet(string name, out IProduct product)
        {
            product = this.Get(name);
            return product != null;
        }


        public IEnumerator<IProduct> GetEnumerator()
        {
            return this._products.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this._products.GetEnumerator();
        }
    }
}