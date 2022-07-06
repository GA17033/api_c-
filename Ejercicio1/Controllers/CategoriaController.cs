using Ejercicio1.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Ejercicio1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly DataDbContext _dbContext;

        public CategoriaController(DataDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var categoria = _dbContext.categorias.Where(c => c.State == true).ToList();
            var response = new
            {
                Status = 200,
                Message ="Categorias",
                Data = categoria

            };
            return Ok(response);

        }
        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var categoria = _dbContext.categorias.Where(c => c.CategoriaId == id && c.State == true).FirstOrDefault();
                if (categoria == null)
                {
                    return NotFound("Categoria No Encontrada");
                }
                else
                {
                    var response = new
                    {
                        Status = 200,
                        Message = "Categorias",
                        Data = categoria

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
            var categoria = _dbContext.categorias.Where(c => c.Name == Name && c.State == true).FirstOrDefault();
            if (categoria == null)
            {
                return NotFound("Categoria No Encontrada");
            }
            else
            {
                var response = new
                {
                    Status = 200,
                    Message = "Categorias",
                    Data = categoria

                };
                return Ok(response);
            }

        }
        [HttpPost("new")]

        public IActionResult Store([FromForm] Categoria request)
        {
            var name = request.Name.ToUpper();
            var categoria = _dbContext.categorias.Where(c=> c.Name == name).FirstOrDefault();

            if( categoria !=null )
            {
                return Ok($"El nombre de la categoria ya existe: {name}");
            }
            
                _dbContext.categorias.Add(request);
                _dbContext.SaveChanges();
                //return Ok(request);

                var response = new
                {
                    Status = 200,
                    Message = "Categoria Creada",
                    Data = request

                };
                return Ok(response);

        }

        [HttpPut("UpdateCategoria/{id}")]

        public IActionResult UpdateCategoria([FromForm] Categoria request, int id)
        {
            
            var categoria = _dbContext.categorias.Where(c => c.CategoriaId == id && c.State == true).FirstOrDefault();
            if (categoria == null)
            {
                return NotFound("Categoria NO Encontrada");
            }
            else
            {
                var name = request.Name.ToUpper();
                var categorianame = _dbContext.categorias.Where(c => c.Name == name).FirstOrDefault();
                if (categorianame != null)
                {
                    return Ok($"El nombre de la categoria ya existe: {name}");
                }

                    categoria.Name = name;
                    _dbContext.Update(categoria);
                    _dbContext.SaveChanges();
                    var response = new
                    {
                        Status = 200,
                        Message = "Categoria Actualizada",
                        Data = categoria

                    };
                    return Ok(response);
                

            }
        }
        [HttpPut("DeleteCategoria/{id}")]

        public IActionResult DeleteCategoria(int id)
        {

            try
            {
                var categoria = _dbContext.categorias.Where(c => c.CategoriaId == id && c.State == true).FirstOrDefault();
                if (categoria == null)
                {
                    return NotFound("Categoria NO Encontrada");
                }
                else
                {
                    categoria.State = false;
                    _dbContext.categorias.Update(categoria);
                    _dbContext.SaveChanges();

                    return Ok("Categoria Eliminada");

                }
            }
            catch (System.Exception)
            {

                return BadRequest("Ingresa un numero valido");
            }
        }

    }
}
