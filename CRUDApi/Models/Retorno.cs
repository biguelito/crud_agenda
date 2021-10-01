// Modelo que serve de retorno para as operacoes de inserir, deletar e atualizar
// realizadas no banco
// O campo boolean Sucesso indica se a operacao foi realizda com sucesso
// e a string Resultado possui uma pequena descricao do retorno

namespace CRUDApi.Models
{
    public class Retorno
    {
        public string Resultado {get; set;}
        public bool Sucesso {get; set;}
    }
}