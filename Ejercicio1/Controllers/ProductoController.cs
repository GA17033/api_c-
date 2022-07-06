using Ejercicio1.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Ejercicio1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        
       
        private readonly DataDbContext _dbContext;

        public ProductoController(DataDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("get")]
        
        public IActionResult get()
        {

            var producto = _dbContext.productos.Where(c=> c.State==true).ToList();

            var response = new
            {
                Status = 200,
                Message = "Productos",
                Data = producto
            };

            return Ok(response);
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var producto = _dbContext.productos.Where(c => c.ProductoId == id && c.State == true).FirstOrDefault();
                if (producto == null)
                {
                    return NotFound("Producto No Encontrado");
                }
                else
                {
                    var response = new
                    {
                        Status = 200,
                        Message = "Producto Encontrado",
                        Data = producto
                    };

                    return Ok(response);
                }
            }
            catch (System.Exception)
            {

                return BadRequest("Ingresa un numero valido");
            }

        }

        [HttpGet("GetByName/{Name}")]

        public IActionResult GetByName(string Name)
        {
            var producto = _dbContext.productos.Where(c=> c.Name == Name).FirstOrDefault();

            if(producto == null)
            {
                return NotFound("Producto no encontrado");
            }
            else
            {
                var response = new 
                {
                    Status = 200,
                    Message = "Producto Encontrado",
                    Data = producto
                };

                return Ok(response);
                
            }

        }

        [HttpPost("post")]

        public IActionResult Store([FromForm] Producto request)
        {
            var category = _dbContext.categorias.Where(c => c.CategoriaId == request.CategoriaId).FirstOrDefault();

            if (category == null)
            {
                return NotFound("NO poseees categorias");
            }
            else {

                _dbContext.productos.Add(request);
                _dbContext.SaveChanges();

                var response = new
                {
                    Status = 200,
                    Message = "Producto Creado",
                    Data = request
                };

                return Ok(response);
            }
               
        }

        [HttpPut("put/{id}")]

        public IActionResult Update([FromForm] Producto request , int id)
        {
            var producto = _dbContext.productos.Where(c => c.ProductoId == id && c.State==true).FirstOrDefault();

            if (producto==null)
            {
                return NotFound("id producto no encontrado");
            }
            else
            {
                try
                {
                    var categoria = _dbContext.categorias.Where(c => c.CategoriaId == request.CategoriaId).FirstOrDefault();
                    if (categoria == null)
                    {
                        return NotFound("id categoria no encontrado");
                    }

                    producto.Name = request.Name;
                    producto.Price = request.Price;
                    producto.Quantify = request.Quantify;
                    producto.CategoriaId = request.CategoriaId;
                    _dbContext.Update(producto);

                    _dbContext.SaveChanges();
                    var response = new
                    {
                        Status = 200,
                        Message = "Producto Actualizado",
                        Data = producto
                    };

                    return Ok(response);

                    ///return Ok(producto);

                }
                catch (System.Exception)
                {

                    throw;
                }
            }
        }
        [HttpPut("delete/{id}")]   
        

        public IActionResult Delete([FromForm] Producto request, int id)
        {
            var producto = _dbContext.productos.Where(c => c.ProductoId == id).FirstOrDefault();


            if (producto == null)
            {
                return NotFound("Id de producto no encontrado");
            }
            else
            {
                producto.State = false;
                _dbContext.Update(producto);

                _dbContext.SaveChanges();

                return Ok("Producto Eliminado");
                
            }
        }

        
    }
}
