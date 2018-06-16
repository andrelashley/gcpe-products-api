using System;
using System.Collections.Generic;
using System.Linq;
using GcpeProductsAPI.Models;

namespace GcpeProductsAPI.Services
{
    public class BookStoreAppContext : IBookStoreAppContext
    {
        List<Product> _products;

        public BookStoreAppContext()
        {
            _products = new List<Product>
           {
                new Product { Id = 1, Name = "The Catcher and the Rye", Price = 10.00M  },
                new Product { Id = 2, Name = "The Giver", Price = 7.50M  },
                new Product { Id = 3, Name = "Watership Down", Price = 3.50M  },
            };
        }

        public Product Add(Product product)
        {
            product.Id = _products.Max(r => r.Id) + 1;
            _products.Add(product);
            return product;
        }

        public void Delete(Product product)
        {
            _products.Remove(product);
        }

        public Product Get(int id)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Product> GetAll()
        {
            return _products.OrderBy(p => p.Name);
        }

        public Product Update(Product updatedProduct)
        {
            var product = _products.FirstOrDefault(p => p.Id == updatedProduct.Id);
            // ids will be the same between objects
            product.Id = updatedProduct.Id;
            product.Name = updatedProduct.Name;
            product.Price = updatedProduct.Price;

            // poor man's sql update
            _products.Remove(product);
            _products.Add(updatedProduct);

            return updatedProduct;
        }
    }
}
