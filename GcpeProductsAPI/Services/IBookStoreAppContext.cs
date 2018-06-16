using GcpeProductsAPI.Models;
using System.Collections.Generic;

namespace GcpeProductsAPI.Services
{
    public interface IBookStoreAppContext
    {
        IEnumerable<Product> GetAll();
        Product Get(int id);
        Product Add(Product product);
        Product Update(Product product);
        void Delete(Product product);
    }
}
