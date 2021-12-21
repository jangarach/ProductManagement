using ProductManagementApi.Models.DAL.DTO;
using ProductManagementApi.Models.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ProductManagementApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ProductController : ApiController
    {
        ProductRepository _repository = new ProductRepository();
        // GET: api/Product
        public IEnumerable<Product> Get()
        {
            return _repository.GetProducts();
        }

        // GET: api/Product/5
        public Product Get(Guid id)
        {
            return _repository.GetProduct(id);
        }

        // POST: api/Product
        public Product Post([FromBody]Product product)
        {
            if (product != null)
                product.Id = Guid.NewGuid(); 
            return _repository.AddProduct(product);
        }

        // PUT: api/Product/5

        public Product Put(Guid id, [FromBody] Product product)
        {
            return _repository.UpdateProduct(id, product); 
        }

        // DELETE: api/Product/5
        public void Delete(Guid id)
        {
            _repository.DeleteProduct(id);
        }
    }
}
