using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductBL.DTOs.Product;
using ProductBL.ProductBL;
using System.Net.Http.Headers;

namespace ProductTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductBLayer productBL;

        public ProductController(IProductBLayer productBL)
        {
            this.productBL = productBL;
        }


        // GET: api/Products
        [HttpGet]
        public ActionResult<IEnumerable<ProductReadDTO>> GetProducts()
        {

            return productBL.GetProducts();

        }

        // POST: api/Product
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<ProductReadDTO> PostProduct(ProductWriteDTO product)
        {
            var productDTO = productBL.PostProduct(product);
            return Created("Product Created", productDTO);
        }


        // PUT: api/Product/id
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public ActionResult<ProductReadDTO> PutProduct(Guid id, ProductWriteDTO product)
        {

            if (productBL.PutProduct(id, product) == 0)
            {
                return NotFound();
            }

            var returnedProduct = productBL.GetProductById(id);

            return Ok(returnedProduct);
        }


        // DELETE: api/Product/id
        [HttpDelete("{id}")]
        public ActionResult<ProductReadDTO> DeleteProduct(Guid id)
        {
            var returnedProduct = productBL.DeleteProduct(id);

            return Ok(returnedProduct);
        }



        [HttpPost, DisableRequestSizeLimit]
        [Route("Upload/{id}")]
        public ActionResult<ProductReadDTO> UploadImage(Guid id)
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resources", "img");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue
                        .Parse(file.ContentDisposition).FileName.Replace("\"", String.Empty);

                    fileName = Guid.NewGuid().ToString() + fileName;
                    var fullPath = System.IO.Path.Combine(pathToSave, path2: fileName.ToString());
                    var dbPath = Path.Combine(folderName, fileName.ToString());
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    productBL.AsssignImageToProduct(id, dbPath);

                    return Ok(new { dbPath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}
