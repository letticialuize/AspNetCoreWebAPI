using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.DTOs;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public AlunoController(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var alunos = _repository.GetAllAlunos(true);
            
            return Ok(_mapper.Map<IEnumerable<AlunoDTO>>(alunos));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Aluno aluno = _repository.GetAlunoById(id);

            if(aluno == null) return BadRequest("O Aluno não foi encontrado.");

            var alunoDTO = _mapper.Map<AlunoDTO>(aluno);

            return Ok(alunoDTO);
        }

        [HttpPost]
        public IActionResult Post(AlunoRegistrarDTO model)
        {
            var aluno = _mapper.Map<Aluno>(model);

            _repository.Add(aluno);
            
            if(_repository.SaveChanges())
            {
                return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDTO>(aluno));
            }

            return BadRequest("Aluno não cadastrado");          
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id,  AlunoRegistrarDTO model)
        {
            var aluno = _repository.GetAlunoById(id);

            if(aluno == null)
                return BadRequest("Aluno não encontrado.");

            _mapper.Map(model, aluno);

            _repository.Update(aluno);
            
            if(_repository.SaveChanges())
                return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDTO>(aluno));

            return BadRequest("Aluno não atualizado"); 
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, AlunoRegistrarDTO model)
        {
             var alu = _repository.GetAlunoById(id);

            if(alu == null)
                return BadRequest("Aluno não encontrado.");

            _mapper.Map(model, alu);

            _repository.Update(model);
            
            if(_repository.SaveChanges())
                return Ok(model);

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