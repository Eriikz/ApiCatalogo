using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("Produtos")]
        public ActionResult<IEnumerable<Categoria>> GetCategoriasProdutos()
        {
            var categorias = _context.Categoria.Include(p => p.Produtos).Where(c => c.CategoriaId <= 5).ToList();
            if (categorias is null)
                return NotFound("categoria não encontradas");

            return categorias;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> Get()
        {
            //AsNoTracking melhora a performance po não armazenar no cache
            //usar só em consultas somente leitura
            return _context.Categoria.AsNoTracking().ToList();
        }


        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public ActionResult<Categoria> Get(int id)
        {
            try
            {
                var categoria = _context.Categoria.FirstOrDefault(p => p.CategoriaId == id);
                if (categoria is null)
                    return NotFound($"categoria id {id} não encontrada");

                return Ok(categoria);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Fudeu, deu erro nessa bagaça e a culda é sua usuario");
            }
        }

        [HttpPost]
        public ActionResult Post(Categoria categoria)
        {
            if (categoria is null)
                return BadRequest();

            _context.Categoria.Add(categoria);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterCategoria",
                new { id = categoria.CategoriaId }, categoria);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Categoria categoria)
        {
            if (id != categoria.CategoriaId)
                return BadRequest();

            _context.Entry(categoria).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(categoria);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var categoria = _context.Categoria.FirstOrDefault(p => p.CategoriaId == id);

            if (categoria is null)
            {
                return NotFound($"Categoria com id {id} não localizada...");
            }
            _context.Categoria.Remove(categoria);
            _context.SaveChanges();

            return Ok(categoria);
        }
    }
}
