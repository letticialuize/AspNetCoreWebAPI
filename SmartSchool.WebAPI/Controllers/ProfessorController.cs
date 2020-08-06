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
        private readonly IRepository _repository;

        public ProfessorController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_repository.GetAllProfessores(true));
        }

        
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Professor professor = _repository.GetProfessorByID(id);

            if(professor == null) return BadRequest("O Professor não foi encontrado.");

            return Ok(professor);
        }

        [HttpPost]
        public IActionResult Post(Professor professor)
        {
            _repository.Add(professor);
            
            if(_repository.SaveChanges())
                return Ok(professor);

            return BadRequest("Professor não cadastrado"); 
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Professor professor)
        {
            var prof = _repository.GetProfessorByID(id);

            if(prof == null)
                return BadRequest("Professor não encontrado.");

             _repository.Update(professor);
            
            if(_repository.SaveChanges())
                return Ok(professor);

            return BadRequest("Professor não atualizado"); 
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Professor professor)
        {
            var prof = _repository.GetProfessorByID(id);

            if(prof == null)
                return BadRequest("Professor não encontrado.");

            _repository.Update(professor);
            _repository.SaveChanges();

            return Ok(professor);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var professor = _repository.GetProfessorByID(id);

            if(professor == null)
                return BadRequest("Professor não encontrado.");

            _repository.Delete(professor);
            _repository.SaveChanges();

            return Ok();
        }
    }
}