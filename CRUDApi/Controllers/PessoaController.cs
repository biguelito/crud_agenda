using CRUDApi.Models;
using CRUDApi.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDApi.Controllers
{   
    // Classe responsavel pelo contato do back com o front, onde é criado os endpoints
    [ApiController]
    [Route("/api/[controller]")]
    public class PessoaController : ControllerBase
    {
        private readonly IPessoaRepository _pessoaRepository;

        public PessoaController(IPessoaRepository pessoaRepository)
        {
            _pessoaRepository = pessoaRepository;
        }

        // Metodo que solicita os dados do banco a classe responsavel [PessoaRepository]
        // Retorna uma lista com os dados do banco, e 204 caso nao exista dados
        [HttpGet]
        public async Task<IActionResult> Read()
        {
            var pessoas = await _pessoaRepository.ReadPessoas();

            if (pessoas.Count() == 0)
                return NoContent();

            return Ok(pessoas);
        }
        
        // Metodo que recebe os dados do front end e repassa para a classe [PessoaRepository] inseri-la no banco
        // Retorna a descricao do objeto [retorno] caso a pessoa seja inserida, e 400 caso nao seja inserida
        [HttpPost]
        public async Task<IActionResult> Insert(Pessoa pessoa)
        {
            if (pessoa.Email == null)
            {
                pessoa.Email = "@";
            }
            if (pessoa.Telefone == null)
            {
                pessoa.Telefone = "-1";
            }

            Retorno retorno = await _pessoaRepository.InsertPessoa(pessoa);

            if (retorno.Sucesso)
            {   
                return Ok(retorno.Resultado);

            }
             
            return BadRequest(retorno.Resultado);
        }
        
        // Metodo que recebe o id da pessoa pelo front end e repassa para a classe [PessoaRepository] remove-la do banco
        // Retorna a descricao do objeto [retorno] caso a pessoa seja deletada, e 400 caso nao seja possivel
        [HttpDelete("{idPessoa}")]
        public async Task<IActionResult> Delete(int idPessoa)
        {
            Retorno retorno = await _pessoaRepository.DeletePessoa(idPessoa);

            if (retorno.Sucesso)
            {   
                return Ok(retorno.Resultado);
            }
             
            return BadRequest(retorno.Resultado);
        } 

        // Metodo que recebe os dados da pessoa pelo front end, incluindo id, para atualizar uma pessoa existente no banco 
        // Retorna a descricao do objeto [retorno] caso a pessoa seja atualizada, e 400 caso nao seja possivel
        [HttpPut("{idPessoa}")]
        public async Task<IActionResult> Update(int idPessoa, Pessoa pessoa)
        {
            Retorno retorno = await _pessoaRepository.UpdatePessoa(idPessoa, pessoa);

            if (retorno.Sucesso)
            {   
                return Ok(retorno.Resultado);
            }
             
            return BadRequest(retorno.Resultado);
        }
    }

}