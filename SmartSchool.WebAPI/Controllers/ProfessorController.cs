using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessorController : ControllerBase
    {
        private readonly SmartContext _context;

        public ProfessorController(SmartContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Professores);
        }

        
        [HttpGet("byId/{id}")]
        public IActionResult GetById(int id)
        {
            Professor professor = _context.Professores.FirstOrDefault(a => a.Id == id);

            if(professor == null) return BadRequest("O Professor não foi encontrado.");

            return Ok(professor);
        }

        [HttpGet("{ByName}")]
        public IActionResult GetByName(string nome, string sobrenome)
        {
            Professor professor = _context.Professores.FirstOrDefault(a => 
                  a.Nome.Contains(nome));

            if(professor == null) return BadRequest("O Professor não foi encontrado.");

            return Ok(professor);
        }

        [HttpPost]
        public IActionResult Post(Professor professor)
        {
            _context.Add(professor);
            _context.SaveChanges();

            return Ok(professor);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Professor professor)
        {
            var prof = _context.Professores.AsNoTracking().FirstOrDefault(x => x.Id == id);

            if(prof == null)
                return BadRequest("Professor não encontrado.");

            _context.Update(professor);
            _context.SaveChanges();

            return Ok(professor);
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Professor professor)
        {
            var prof = _context.Professores.AsNoTracking().FirstOrDefault(x => x.Id == id);

            if(prof == null)
                return BadRequest("Professor não encontrado.");

            _context.Update(professor);
            _context.SaveChanges();

            return Ok(professor);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var professor = _context.Professores.FirstOrDefault(x => x.Id == id);

            if(professor == null)
                return BadRequest("Professor não encontrado.");

            _context.Remove(professor);
            _context.SaveChanges();

            return Ok();
        }
    }
}