using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {
        private readonly IRepository _repository;

        public AlunoController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _repository.GetAllAlunos(true);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Aluno aluno = _repository.GetAlunoById(id);

            if(aluno == null) return BadRequest("O Aluno não foi encontrado.");

            return Ok(aluno);
        }

        [HttpPost]
        public IActionResult Post(Aluno aluno)
        {
            _repository.Add(aluno);
            
            if(_repository.SaveChanges())
                return Ok(aluno);

            return BadRequest("Aluno não cadastrado");          
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Aluno aluno)
        {
            var alu = _repository.GetAlunoById(id);

            if(alu == null)
                return BadRequest("Aluno não encontrado.");

            _repository.Update(aluno);
            
            if(_repository.SaveChanges())
                return Ok(aluno);

            return BadRequest("Aluno não atualizado"); 
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Aluno aluno)
        {
             var alu = _repository.GetAlunoById(id);

            if(alu == null)
                return BadRequest("Aluno não encontrado.");

            _repository.Update(aluno);
            
            if(_repository.SaveChanges())
                return Ok(aluno);

            return BadRequest("Aluno não atualizado"); 
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var aluno = _repository.GetAlunoById(id);

            if(aluno == null)
                return BadRequest("Aluno não encontrado.");

           _repository.Delete(aluno);
            
            if(_repository.SaveChanges())
                return Ok("Aluno deletado");

            return BadRequest("Aluno não deletado");    
        }
    }
}