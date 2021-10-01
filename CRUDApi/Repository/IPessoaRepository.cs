using System.Collections.Generic;
using CRUDApi.Models;
using System.Threading.Tasks;


namespace CRUDApi.Repository
{
    public interface IPessoaRepository 
    {
        Task<IEnumerable<Pessoa>> ReadPessoas();
        Task<Retorno> InsertPessoa(Pessoa pessoa);
        Task<Retorno> DeletePessoa(int idPessoa);
        Task<Retorno> UpdatePessoa(int idPessoa, Pessoa pessoa);
    }
}