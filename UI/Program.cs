using System;
using MySql.Data.MySqlClient;

internal sealed class Almoxarifado
{
    public static int IdUsuarioLogado { get; private set; } = 0;

    public static void Main()
    {
        bool autenticado = false;

        while (!autenticado)
        {
            Console.WriteLine("\n=== SISTEMA DE ALMOXARIFADO ===");
            Console.WriteLine("1 - Fazer Login");
            Console.WriteLine("2 - Cadastrar Novo Usuário");
            Console.WriteLine("0 - Sair");
            Console.Write("Escolha uma opção: ");

            string op = Console.ReadLine();

            if (op == "1")
            {
                autenticado = FazerLogin();
            }
            else if (op == "2")
            {
                CadastrarUsuario();
            }
            else if (op == "0")
            {
                Console.WriteLine("Encerrando...");
                return;
            }
            else
            {
                Console.WriteLine("Opção inválida!");
            }
        }

        MenuEstoque();
    }

    private static bool FazerLogin()
    {
        Console.Write("\nLogin: ");
        string login = Console.ReadLine();

        Console.Write("Senha: ");
        string senha = Console.ReadLine();

        ConexaoBD bd = new ConexaoBD();
        using (MySqlConnection con = bd.Conectar())
        {
            if (con == null) return false;

            string sql = "SELECT id_usuario, nome FROM Usuario WHERE login = @login AND senha = @senha";
            using (MySqlCommand cmd = new MySqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@login", login);
                cmd.Parameters.AddWithValue("@senha", senha);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        IdUsuarioLogado = reader.GetInt32("id_usuario");
                        string nome = reader.GetString("nome");

                        Console.WriteLine($"\nAcesso Liberado! Bem-vindo(a), {nome}.");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("\nErro: Login ou senha incorretos.");
                        return false;
                    }
                }
            }
        }
    }

    private static void CadastrarUsuario()
    {
        Console.Write("\nDigite o seu Nome completo: ");
        string nome = Console.ReadLine();

        Console.Write("Crie um Login de acesso: ");
        string login = Console.ReadLine();

        Console.Write("Crie uma Senha: ");
        string senha = Console.ReadLine();

        ConexaoBD bd = new ConexaoBD();
        using (MySqlConnection con = bd.Conectar())
        {
            if (con == null) return;

            string checkSql = "SELECT COUNT(*) FROM Usuario WHERE login = @login";
            using (MySqlCommand checkCmd = new MySqlCommand(checkSql, con))
            {
                checkCmd.Parameters.AddWithValue("@login", login);
                if (Convert.ToInt32(checkCmd.ExecuteScalar()) > 0)
                {
                    Console.WriteLine("Erro: Este login já está em uso por outra pessoa.");
                    return;
                }
            }

            string insertSql = "INSERT INTO Usuario (nome, login, senha) VALUES (@nome, @login, @senha)";
            using (MySqlCommand cmd = new MySqlCommand(insertSql, con))
            {
                cmd.Parameters.AddWithValue("@nome", nome);
                cmd.Parameters.AddWithValue("@login", login);
                cmd.Parameters.AddWithValue("@senha", senha);

                if (cmd.ExecuteNonQuery() > 0)
                {
                    Console.WriteLine("Usuário cadastrado com sucesso! Use a opção 1 para logar.");
                }
            }
        }
    }

    private static void MenuEstoque()
    {
        Estoque estoquePuc = new Estoque();
        estoquePuc.SetUsuarioLogado(IdUsuarioLogado);
        int opcao;

        do
        {

            Console.WriteLine("\n========== MENU ==========");
            Console.WriteLine("1 - CADASTRAR PRODUTO.");
            Console.WriteLine("2 - REGISTRAR ENTRADA.");
            Console.WriteLine("3 - REGISTRAR SAIDA.");
            Console.WriteLine("4 - LISTAR TUDO.");
            Console.WriteLine("5 - VER HISTORICO.");
            Console.WriteLine("6 - BUSCAR PRODUTO.");
            Console.WriteLine("7 - EXCLUIR PRODUTO.");
            Console.WriteLine("8 - DETALHES DO PRODUTO");
            Console.WriteLine("0 - SAIR DO SISTEMA.");
            Console.WriteLine("==========================");

            if (!int.TryParse(Console.ReadLine(), out opcao))
            {
                Console.WriteLine("Entrada inválida. Digite um número.");
                continue;
            }

            switch (opcao)
            {
             case 1:
                    Console.WriteLine("");
                    Console.Write("Digite o ID do produto: ");
                    int id = int.Parse(Console.ReadLine());

                    Console.Write("Digite o nome do produto: ");
                    string nome = Console.ReadLine();

                    
                    Console.Write("Digite uma breve descrição (ex: marca, cor, tamanho): ");
                    string descricao = Console.ReadLine();

                    Console.WriteLine("Categorias: [1] Informática | [2] Papelaria | [3] Limpeza | [4] Operações | [5] Jardinagem");
                    Console.Write("Digite o ID da categoria: ");
                    string categoria = Console.ReadLine();

                    Console.WriteLine("Fornecedores: [1] Port | [2] Climpo | [3] Dell| [4] BrasPrint  | [5] Minas Ferramentas | [6] Mercado Livre");
                    Console.Write("Digite o ID do fornecedor: ");
                    string fornecedor = Console.ReadLine();

                    Produto novoProduto = new Produto(id, nome, descricao, categoria, fornecedor);

                    Console.WriteLine("");
                    if (estoquePuc.Cadastrar(novoProduto))
                    {
                        Console.WriteLine("Produto cadastrado com sucesso!");
                    }
                    break;
                case 2:
                    Console.WriteLine("");
                    Console.Write("Digite o ID do produto: ");
                    int IdEntrada = int.Parse(Console.ReadLine());

                    Console.Write("Digite a quantidade de entrada: ");
                    int qtdEntrada = int.Parse(Console.ReadLine());

                    estoquePuc.RegistrarEntrada(IdEntrada, qtdEntrada);
                    break;

                case 3:
                    Console.WriteLine("");
                    Console.Write("Digite o ID do produto: ");
                    int IdSaida = int.Parse(Console.ReadLine());

                    Console.Write("Digite a quantidade de saida: ");
                    int qtdSaida = int.Parse(Console.ReadLine());

                    estoquePuc.RegistrarSaida(IdSaida, qtdSaida);
                    break;

                case 4:
                    Console.WriteLine("");
                    estoquePuc.LisarTudo();
                    break;

                case 5:
                    Console.WriteLine("");
                    estoquePuc.ExibirHistotico();
                    break;

                case 6:
                    Console.WriteLine("");
                    Console.Write("Digite o ID que deseja buscar: ");
                    int idBusca = int.Parse(Console.ReadLine());

                    Produto encontrado = estoquePuc.BuscarPorCodigo(idBusca);

                    if (encontrado != null)
                    {
                        Console.WriteLine("\n--- Produto Encontrado ---");
                        encontrado.Exibir();
                    }
                    else
                    {
                        Console.WriteLine("Erro: Produto com este ID não foi localizado.");
                    }
                    break;

                case 7:
                    Console.WriteLine("");
                    Console.Write("Digite o ID do produto que deseja excluir: ");
                    int idExclusao = int.Parse(Console.ReadLine());

                    estoquePuc.ExcluirProduto(idExclusao);
                    break;

                    case 8:
                        Console.WriteLine("");
                        Console.Write("Digite o Código ID do Produto que deseja consultar os detalhes: ");
                        int idDescricao = int.Parse(Console.ReadLine());
                        
                        estoquePuc.BuscarProduto(idDescricao);
                        break;

                

                case 0:
                    Console.WriteLine("");
                    Console.WriteLine("Sistema encerrado.....");
                    break;

                default:
                    Console.WriteLine("");
                    Console.WriteLine("Opção invalida! ");
                    break;
            }
        } while (opcao != 0);
    }
}
