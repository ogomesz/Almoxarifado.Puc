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

<<<<<<< HEAD
            string insertSql = "INSERT INTO Produto (codigo_id, nome_produto, id_categoria, id_fornecedor, quantidade_estoque) " +
                               "VALUES (@CodigoID, @Nome, @Categoria, @Fornecedor, @Quantidade)";
=======
            string insertSql = "INSERT INTO Produto (codigo_id, nome_produto, id_categoria, quantidade_estoque) " +
                               "VALUES (@CodigoID, @Nome, @Categoria, @Quantidade)";
>>>>>>> cd80aa7e159c5e19cc0c9af4c968f0d0b6b6e6dc
            
            using (MySqlCommand insertCmd = new MySqlCommand(insertSql, con))
            {
                insertCmd.Parameters.AddWithValue("@CodigoID", p1.CodigoID);
                insertCmd.Parameters.AddWithValue("@Nome", p1.NomeProduto);
                insertCmd.Parameters.AddWithValue("@Categoria", int.Parse(p1.Categoria));
<<<<<<< HEAD
                insertCmd.Parameters.AddWithValue("@Fornecedor", int.Parse(p1.Fornecedor)); 
=======
>>>>>>> cd80aa7e159c5e19cc0c9af4c968f0d0b6b6e6dc
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

<<<<<<< HEAD
            string sql = @"SELECT p.codigo_id, p.nome_produto, c.nome_categoria, f.nome_fantasia, p.quantidade_estoque 
                           FROM Produto p 
                           JOIN Categoria c ON p.id_categoria = c.id_categoria 
                           JOIN Fornecedor f ON p.id_fornecedor = f.id_fornecedor
=======
            string sql = @"SELECT p.codigo_id, p.nome_produto, c.nome_categoria, p.quantidade_estoque 
                           FROM Produto p 
                           JOIN Categoria c ON p.id_categoria = c.id_categoria 
>>>>>>> cd80aa7e159c5e19cc0c9af4c968f0d0b6b6e6dc
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
<<<<<<< HEAD
                            reader.GetString("nome_categoria"),
                            reader.GetString("nome_fantasia") 
=======
                            reader.GetString("nome_categoria")
>>>>>>> cd80aa7e159c5e19cc0c9af4c968f0d0b6b6e6dc
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

<<<<<<< HEAD
            string sql = @"SELECT p.codigo_id, p.nome_produto, c.nome_categoria, f.nome_fantasia, p.quantidade_estoque 
                           FROM Produto p 
                           JOIN Categoria c ON p.id_categoria = c.id_categoria
                           JOIN Fornecedor f ON p.id_fornecedor = f.id_fornecedor";
=======
            string sql = @"SELECT p.codigo_id, p.nome_produto, c.nome_categoria, p.quantidade_estoque 
                           FROM Produto p 
                           JOIN Categoria c ON p.id_categoria = c.id_categoria";
>>>>>>> cd80aa7e159c5e19cc0c9af4c968f0d0b6b6e6dc

            using (MySqlCommand cmd = new MySqlCommand(sql, con))
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                bool temRegistros = false;
                while (reader.Read())
                {
                    temRegistros = true;
                    Console.WriteLine($"Cod: {reader["codigo_id"]} | {reader["nome_produto"]} | " +
<<<<<<< HEAD
                                      $"Categoria: {reader["nome_categoria"]} | Fornecedor: {reader["nome_fantasia"]} | Quantidade: {reader["quantidade_estoque"]}");
=======
                                      $"Categoria: {reader["nome_categoria"]} | Quantidade: {reader["quantidade_estoque"]}");
>>>>>>> cd80aa7e159c5e19cc0c9af4c968f0d0b6b6e6dc
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
}