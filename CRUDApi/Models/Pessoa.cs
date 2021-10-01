// Modelo baseado nos dados que serao salvos no banco

namespace CRUDApi.Models
{
    public class Pessoa
    {
        public int IdPessoa {get; set;}
        public string Nome {get; set;}
        public string Email {get; set;}
        public string Telefone {get; set;}
    }
}