using System;
using MySql.Data.MySqlClient;

internal class Estoque
{
    private ConexaoBD bd = new ConexaoBD();
    private int idUsuarioLogado;

    public void SetUsuarioLogado(int id)
    {
        idUsuarioLogado = id;
    }

    public bool Cadastrar(Produto p1)
    {
        using (MySqlConnection con = bd.Conectar())
        {
            if (con == null) return false;

            string checkSql = "SELECT COUNT(*) FROM Produto WHERE codigo_id = @CodigoID";
            using (MySqlCommand checkCmd = new MySqlCommand(checkSql, con))
            {
                checkCmd.Parameters.AddWithValue("@CodigoID", p1.CodigoID);
                int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                if (count > 0)
                {
                    Console.WriteLine("Erro: Já existe um produto cadastrado com este ID!");
                    return false;
                }
            }

            // CORREÇÃO 1: Adicionando 'descricao' na lista de colunas e '@Descricao' nos valores
            string insertSql = "INSERT INTO Produto (codigo_id, nome_produto, descricao, id_categoria, id_fornecedor, quantidade_estoque) " +
                               "VALUES (@CodigoID, @Nome, @Descricao, @Categoria, @Fornecedor, @Quantidade)";
            
            using (MySqlCommand insertCmd = new MySqlCommand(insertSql, con))
            {
                insertCmd.Parameters.AddWithValue("@CodigoID", p1.CodigoID);
                insertCmd.Parameters.AddWithValue("@Nome", p1.NomeProduto);
                
                // CORREÇÃO 2: Passando o valor da descrição do objeto para o banco de dados
                insertCmd.Parameters.AddWithValue("@Descricao", p1.Descricao); 
                
                insertCmd.Parameters.AddWithValue("@Categoria", int.Parse(p1.Categoria));
                insertCmd.Parameters.AddWithValue("@Fornecedor", int.Parse(p1.Fornecedor)); 
                insertCmd.Parameters.AddWithValue("@Quantidade", p1.QuantidadeEstoque);

                int linhasAfetadas = insertCmd.ExecuteNonQuery();
                return linhasAfetadas > 0;
            }
        }
    }

    public Produto BuscarPorCodigo(int id)
    {
        using (MySqlConnection con = bd.Conectar())
        {
            if (con == null) return null;

            string sql = @"SELECT p.codigo_id, p.nome_produto, c.nome_categoria, f.nome_fantasia, p.quantidade_estoque 
                           FROM Produto p 
                           JOIN Categoria c ON p.id_categoria = c.id_categoria 
                           JOIN Fornecedor f ON p.id_fornecedor = f.id_fornecedor
                           WHERE p.codigo_id = @id";

            using (MySqlCommand cmd = new MySqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@id", id);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Produto p = new Produto(
                            reader.GetInt32("codigo_id"),
                            reader.GetString("nome_produto"),
                            reader.GetString("descricao"),
                            reader.GetString("nome_categoria"),
                            reader.GetString("nome_fantasia") 
                        );
                        
                        p.AdicionarEstoque(reader.GetInt32("quantidade_estoque")); 
                        return p;
                    }
                }
            }
        }
        return null;
    }

    public void LisarTudo()
    {
        using (MySqlConnection con = bd.Conectar())
        {
            if (con == null) return;

            string sql = @"SELECT p.codigo_id, p.nome_produto, c.nome_categoria, f.nome_fantasia, p.quantidade_estoque 
                           FROM Produto p 
                           JOIN Categoria c ON p.id_categoria = c.id_categoria
                           JOIN Fornecedor f ON p.id_fornecedor = f.id_fornecedor";

            using (MySqlCommand cmd = new MySqlCommand(sql, con))
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                bool temRegistros = false;
                while (reader.Read())
                {
                    temRegistros = true;
                    Console.WriteLine($"Cod: {reader["codigo_id"]} | {reader["nome_produto"]} | " +
                                      $"Categoria: {reader["nome_categoria"]} | Fornecedor: {reader["nome_fantasia"]} | Quantidade: {reader["quantidade_estoque"]}");
                }

                if (!temRegistros)
                {
                    Console.WriteLine("Nenhum produto cadastrado no momento.");
                }
            }
        }
    }

    public void ExcluirProduto(int id)
    {
        using (MySqlConnection con = bd.Conectar())
        {
            if (con == null) return;

            string sql = "DELETE FROM Produto WHERE codigo_id = @id";
            using (MySqlCommand cmd = new MySqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@id", id);
                int linhasAfetadas = cmd.ExecuteNonQuery();

                if (linhasAfetadas > 0)
                {
                    Console.WriteLine("Produto excluído com sucesso!");
                }
                else
                {
                    Console.WriteLine("Erro: Produto não encontrado para exclusão!");
                }
            }
        }
    }

    public void RegistrarEntrada(int id, int qtd)
    {
        using (MySqlConnection con = bd.Conectar())
        {
            if (con == null) return;

            string updateSql = "UPDATE Produto SET quantidade_estoque = quantidade_estoque + @qtd WHERE codigo_id = @id";
            using (MySqlCommand updateCmd = new MySqlCommand(updateSql, con))
            {
                updateCmd.Parameters.AddWithValue("@qtd", qtd);
                updateCmd.Parameters.AddWithValue("@id", id);
                int afetadas = updateCmd.ExecuteNonQuery();

                if (afetadas == 0)
                {
                    Console.WriteLine("Produto inexistente!");
                    return;
                }
            }

            string insertMov = "INSERT INTO Movimentacao (codigo_id, id_usuario, quantidade, tipo) VALUES (@id, @id_user, @qtd, 'ENTRADA')";
            using (MySqlCommand movCmd = new MySqlCommand(insertMov, con))
            {
                movCmd.Parameters.AddWithValue("@id", id);
                movCmd.Parameters.AddWithValue("@id_user", idUsuarioLogado);
                movCmd.Parameters.AddWithValue("@qtd", qtd);
                movCmd.ExecuteNonQuery();
            }
            Console.WriteLine("Entrada efetuada com sucesso!");
        }
    }

    public void RegistrarSaida(int id, int qtd)
    {
        Produto encontrado = BuscarPorCodigo(id);

        if (encontrado == null)
        {
            Console.WriteLine("Produto inexistente!");
            return;
        }

        if (encontrado.QuantidadeEstoque < qtd)
        {
            Console.WriteLine("Saldo Insuficiente!");
            return;
        }

        using (MySqlConnection con = bd.Conectar())
        {
            if (con == null) return;

            string updateSql = "UPDATE Produto SET quantidade_estoque = quantidade_estoque - @qtd WHERE codigo_id = @id";
            using (MySqlCommand updateCmd = new MySqlCommand(updateSql, con))
            {
                updateCmd.Parameters.AddWithValue("@qtd", qtd);
                updateCmd.Parameters.AddWithValue("@id", id);
                updateCmd.ExecuteNonQuery();
            }

            string insertMov = "INSERT INTO Movimentacao (codigo_id, id_usuario, quantidade, tipo) VALUES (@id, @id_user, @qtd, 'SAIDA')";
            using (MySqlCommand movCmd = new MySqlCommand(insertMov, con))
            {
                movCmd.Parameters.AddWithValue("@id", id);
                movCmd.Parameters.AddWithValue("@id_user", idUsuarioLogado);
                movCmd.Parameters.AddWithValue("@qtd", qtd);
                movCmd.ExecuteNonQuery();
            }
            Console.WriteLine("Saída efetuada com sucesso!");
        }
    }

    public void ExibirHistotico()
    {
        using (MySqlConnection con = bd.Conectar())
        {
            if (con == null) return;

            string sql = @"SELECT m.quantidade, m.tipo, m.data_hora, p.nome_produto 
                           FROM Movimentacao m
                           JOIN Produto p ON m.codigo_id = p.codigo_id
                           ORDER BY m.data_hora DESC";

            using (MySqlCommand cmd = new MySqlCommand(sql, con))
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                bool temRegistros = false;
                while (reader.Read())
                {
                    temRegistros = true;
                    DateTime data = reader.GetDateTime("data_hora");
                    Console.WriteLine($"{reader["nome_produto"]} | QTD: {reader["quantidade"]} - {reader["tipo"]} - {data}");
                }

                if (!temRegistros)
                {
                    Console.WriteLine("Nenhuma movimentação registrada no histórico.");
                }
            }
        }
    }

    public void BuscarProduto(int idProduto)
    {
        using (MySqlConnection con = bd.Conectar())
        {
            if (con == null) return;
            
            // Fazemos um JOIN para trazer não só a descrição, mas os nomes da Categoria e Fornecedor
            string sql = @"
                SELECT p.codigo_id, p.nome_produto, p.descricao, p.quantidade_estoque, 
                       c.nome_categoria, f.nome_fantasia
                FROM Produto p
                JOIN Categoria c ON p.id_categoria = c.id_categoria
                JOIN Fornecedor f ON p.id_fornecedor = f.id_fornecedor
                WHERE p.codigo_id = @id";

            using (MySqlCommand cmd = new MySqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@id", idProduto);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Console.WriteLine("\n=== FICHA DO PRODUTO ===");
                        Console.WriteLine($"Código ID  : {reader["codigo_id"]}");
                        Console.WriteLine($"Nome       : {reader["nome_produto"]}");
                        Console.WriteLine($"Descrição  : {reader["descricao"]}");
                        Console.WriteLine($"Categoria  : {reader["nome_categoria"]}");
                        Console.WriteLine($"Fornecedor : {reader["nome_fantasia"]}");
                        Console.WriteLine($"Em Estoque : {reader["quantidade_estoque"]} unidades");
                        Console.WriteLine("========================");
                    }
                    else
                    {
                        Console.WriteLine("Erro: Produto não encontrado no banco de dados.");
                    }
                }
            }
        }
    }
}