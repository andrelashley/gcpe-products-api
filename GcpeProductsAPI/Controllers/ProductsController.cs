using GcpeProductsAPI.Models;
using GcpeProductsAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace GcpeProductsAPI.Controllers
{
    /* 
     * Assumptions: 
     * 1. we'd have proper logging for exceptions in a production API
     * 2. We'd have a separate model class that takes input data (put/post) and validate against that before persisting the object
     * 3. On the put request for updating an object, the client has to send us back all the fields to be updated, ideally we would want to
     * use a PATCH document to update only the fields that have changed
     */


    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private IBookStoreAppContext _context;

        public ProductsController(IBookStoreAppContext bookStoreAppContext)
        {
            _context = bookStoreAppContext;
        }

        [HttpGet("")]
        public IActionResult Get()
        {
            var products = _context.GetAll();
            return Ok(products);
        }

        [HttpGet("{id}", Name = "ProductGet")]
        public IActionResult Get(int id)
        {
            try
            {
                Product product = null;
                product = _context.Get(id);

                if (product == null) return NotFound($"Product with {id} was not found");

                return Ok(product);
            }
            catch
            {
            }

            return BadRequest();
        }

        [HttpPost]
        public IActionResult Post([FromBody]Product model)
        {
            try
            {
                _context.Add(model);
                var newUri = Url.Link("ProductGet", new { id = model.Id });
                return Created(newUri, model);
            }
            catch (Exception)
            {
            }

            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Product model)
        {
            try
            {
                var oldProduct = _context.Get(id);
                if (oldProduct == null) return NotFound($"Could not find a product with an id of {id}");

                _context.Update(model);
                return Ok(model);
            }
            catch (Exception)
            {

            }

            return BadRequest("Couldn't update product");
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var oldProduct = _context.Get(id);
                if (oldProduct == null) return NotFound($"Could not find product with id of {id}");

                _context.Delete(oldProduct);
                return Ok();
            }
            catch (Exception)
            {
            }

            return BadRequest("Could not delete product");
        }
    }
}