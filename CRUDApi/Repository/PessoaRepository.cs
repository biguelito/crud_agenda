using System.Collections.Generic;
using CRUDApi.Models;
using MySqlConnector;
using Dapper;
using System;
using System.Threading.Tasks;


namespace CRUDApi.Repository
{
    // Classe responsavel por enviar comandos ao banco mysql [crud]
    public class PessoaRepository : IPessoaRepository
    {
        public readonly string _connectionString;

        public PessoaRepository(string connectionString)
        {
            this._connectionString = connectionString; 
        }

        // Metodo que realiza um select para todos os dados do banco, ordenando pelo nome
        // Retorna todas as pessoas salvas no banco
        public async Task<IEnumerable<Pessoa>> ReadPessoas()
        {
            List<Pessoa> pessoas = new List<Pessoa>();
            
            using(MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT * FROM pessoa ORDER BY nome ASC;", connection);
                MySqlDataReader reader = await Task.Run(() =>cmd.ExecuteReader());

                while (reader.Read())
                {
                    Pessoa pessoa = new Pessoa();
                    pessoa.Nome = reader["nome"].ToString();
                    pessoa.Telefone = reader["telefone"].ToString();
                    pessoa.Email = reader["email"].ToString();
                    pessoa.IdPessoa = Convert.ToInt32(reader["idPessoa"].ToString());

                    pessoas.Add(pessoa);
                }

                connection.Close();
            }

            return pessoas;
        }

        // Metodo que insere uma, somente uma por vez, pessoa no banco, nas colunas nome, email, telefone
        // Retorna um objeto da classe [Retorno] 
        public async Task<Retorno> InsertPessoa(Pessoa pessoa)
        {   
            Retorno retorno = new Retorno();

            using(MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                
                string query = "insert ignore into pessoa (nome, email, telefone) VALUE (@nomeValor, @emailValor, @telefoneValor);";
                using (MySqlCommand cmd = new MySqlCommand(query))
                {
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@nomeValor", pessoa.Nome);
                    cmd.Parameters.AddWithValue("@emailValor", pessoa.Email);
                    cmd.Parameters.AddWithValue("@telefoneValor", pessoa.Telefone);

                    try
                    {
                         int r = await Task.Run(() => cmd.ExecuteNonQuery());
                        if (r == 1) 
                        {
                            retorno.Resultado = "Pessoa criada";
                            retorno.Sucesso = true;
                        }
                        else
                        {
                            retorno.Resultado = "Pessoa ja existente";
                            retorno.Sucesso = false;
                        }
                        connection.Close();
                    }
                    catch (MySqlException error)
                    {
                        Console.WriteLine(error.ErrorCode);
                    }

                    
                }
            }

            return retorno;
        }

        // Metodo que deleta uma pessoa do banco, baseada no id
        // Retorna um objeto da classe [Retorno]
        public async Task<Retorno> DeletePessoa(int idPessoa)
        {
            Retorno retorno = new Retorno();

            using(MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "DELETE FROM pessoa where idPessoa = @idPessoaValor;";
                
                using (MySqlCommand cmd = new MySqlCommand(query))
                {
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@idPessoaValor", idPessoa);

                    int r = await Task.Run(() => cmd.ExecuteNonQuery());
                    if (r == 1) 
                    {
                        retorno.Resultado = "Pessoa removida";
                        retorno.Sucesso = true;
                    }
                    else
                    {
                        retorno.Resultado = "Pessoa nao removida";
                        retorno.Sucesso = false;
                    }
                
                connection.Close();
                }
            }

            return retorno;
        }

        // Metodo que atualiza uma pessoa do banco, baseada no id
        // e verificando quais colunas serao atualizadas
        // Retorna um objeto da classe [Retorno]
        public async Task<Retorno> UpdatePessoa(int idPessoa, Pessoa pessoa)
        {
            Retorno retorno = new Retorno();

            using(MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "UPDATE pessoa set ";
                if (pessoa.Nome != null)
                {
                    query += "nome = '" + pessoa.Nome + "',";
                }
                if (pessoa.Email != null)
                {
                    query += "email = '" + pessoa.Email + "',";
                }
                if (pessoa.Telefone != null)
                {
                    query += "telefone = '" + pessoa.Telefone + "',";
                }
                query = query.Remove(query.Length - 1);
                query += " where idPessoa = @idPessoaValor;";
                
                using (MySqlCommand cmd = new MySqlCommand(query))
                {
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@idPessoaValor", idPessoa);

                        int r = await Task.Run(() => cmd.ExecuteNonQuery());
                    if (r == 1) 
                    {
                        retorno.Resultado = "Pessoa atualizada";
                        retorno.Sucesso = true;
                    }
                    else
                    {
                        retorno.Resultado = "Pessoa não atualizada";
                        retorno.Sucesso = false;
                    }
                    
                }
                connection.Close();
            }

            return retorno; 
        }
    }
}