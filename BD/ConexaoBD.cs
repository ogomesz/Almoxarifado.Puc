using MySql.Data.MySqlClient;
using System;

internal class ConexaoBD
{

    private string stringConexao = "Server=localhost;Port=3306;Database=almoxarifado_db;Uid=root;Pwd=Lilico8346#;"; private MySqlConnection conexao;

    public MySqlConnection Conectar()
    {
        try
        {
            conexao = new MySqlConnection(stringConexao);
            conexao.Open();
            return conexao;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao conectar com o banco de dados: {ex.Message}");
            return null;
        }
    }

    public void Desconectar()
    {
        if (conexao != null && conexao.State == System.Data.ConnectionState.Open)
        {
            conexao.Close();
        }
    }
}