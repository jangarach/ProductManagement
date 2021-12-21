using ProductManagementApi.Models.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProductManagementApi.Models.DAL.Repository
{
    /// <summary>
    /// Данный репозиторий работает с продуктами
    /// </summary>
    public interface IProductRepository
    {
        Product GetProduct(Guid id);
        List<Product> GetProducts();
        Product AddProduct(Product product);
        Product UpdateProduct(Guid id, Product product);
        void DeleteProduct(Guid id);

    }
    public class ProductRepository : IProductRepository, IDisposable
    {
        PoductContext db = new PoductContext();

        public Product GetProduct(Guid id)
        {
            return db.Products.FirstOrDefault(p => p.Id == id);
        }
        public List<Product> GetProducts()
        {
            return db.Products.ToList(); 
        }
        public Product AddProduct(Product product)
        {
            if (product == null)
                return default;

            var addedProduct = db.Products.Add(product);
            db.SaveChanges();
            return addedProduct;
        }
        public Product UpdateProduct(Guid id, Product product)
        {
            if (product == null)
                return default;

            var changeProduct = db.Products.SingleOrDefault(p => p.Id == id);
            changeProduct.Name = product.Name;
            changeProduct.Description = product.Description;
            db.SaveChanges();

            return changeProduct;
        }
        public void DeleteProduct(Guid id)
        {
            var product = db.Products.SingleOrDefault(p => p.Id == id);
            if (product != null)
                db.Products.Remove(product);
            db.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}