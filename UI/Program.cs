internal sealed class Almoxarifado
{
    public static void Main()
    {
        //criacao do objeto estoquePuc que ira servir para chamar os metodos do Produto 
        Estoque estoquePuc = new Estoque();
        //variavel usada para usar no switch
        int opcao;

        do
        {
            System.Console.WriteLine("");
            System.Console.WriteLine("========== MENU ==========");
            System.Console.WriteLine("");
            System.Console.WriteLine("1 - CADASTRAR PRODUTO. ");
            System.Console.WriteLine("2 - REGISTRAR ENTRADA. ");
            System.Console.WriteLine("3 - REGISTRAR SAIDA. ");
            System.Console.WriteLine("4 - LISTAR TUDO. ");
            System.Console.WriteLine("5 - VER HISTORICO. ");
            System.Console.WriteLine("6 - BUSCAR PRODUTO. ");
            System.Console.WriteLine("7 - SAIDA (APERTE 0). ");
            System.Console.WriteLine("");
            System.Console.WriteLine("==========================");

            opcao = int.Parse(Console.ReadLine());
            switch (opcao)
            {
                case 1:

                    System.Console.WriteLine("");
                    //ira pedir ao usuario para digitar o id do produto 
                    Console.Write("Digite o ID do produto: ");
                    int id = int.Parse(Console.ReadLine());

                    //ira pedir para o usuario digitar o nome do produto 
                    Console.Write("Digite o nome do produto: ");
                    string nome = Console.ReadLine();

                    // Aqui você cria o objeto com os dados que o usuário digitou
                    Produto novoProduto = new Produto(id, nome);

                    System.Console.WriteLine("");
                    // Agora você passa o objeto para o estoque que ira cadastralo pelo metodo criado no Estoque
                    estoquePuc.Cadastrar(novoProduto);

                    Console.WriteLine("Produto cadastrado com sucesso!");
                    //encerra o programa e volta para o MENU
                    break;


                case 2:
                    System.Console.WriteLine("");
                    System.Console.Write("Digite o ID do produto. ");
                    int IdEntrada = int.Parse(Console.ReadLine());

                    System.Console.Write("Digite a quantidade do produto. ");
                    int qtdEntrada = int.Parse(Console.ReadLine());

                    estoquePuc.RegistrarEntrada(IdEntrada, qtdEntrada);
                    System.Console.Write("Lançamento efetuado! ");
                    break;

                case 3:
                    System.Console.WriteLine("");
                    System.Console.Write("Digite o ID do produto. ");
                    int IdSaida = int.Parse(Console.ReadLine());

                    System.Console.Write("Digite a quantidade do produto. ");
                    int qtdSaida = int.Parse(Console.ReadLine());

                    estoquePuc.RegistrarSaida(IdSaida, qtdSaida);
                    System.Console.Write("Saida realizada! ");
                    break;

                case 4:
                    System.Console.WriteLine("");
                    estoquePuc.LisarTudo();
                    break;

                case 5:
                    System.Console.WriteLine("");
                    estoquePuc.ExibirHistotico();
                    break;

                case 6:
                    System.Console.WriteLine("");
                    Console.Write("Digite o ID que deseja buscar: ");
                    // Captura o ID que foi inserido 
                    int idBusca = int.Parse(Console.ReadLine());

                    //pega o tipo da List + objeto = objeto2.Metodo(variavel que armazenou o valor digitado )
                    Produto encontrado = estoquePuc.BuscarPorCodigo(idBusca);

                    //Se encontrado diferente de inexistente 
                    if (encontrado != null)
                    {
                        //o id encontrado ira exibir 
                        Console.WriteLine("\n--- Produto Encontrado ---");
                        // Usa o método que você já criou!
                        encontrado.Exibir();
                    }
                    else
                    {
                        //se nao exibe essa msg
                        Console.WriteLine("Erro: Produto com este ID não foi localizado.");
                    }
                    break;


                case 0:
                    System.Console.WriteLine("");
                    System.Console.WriteLine("Sistema encerrado.....");
                    break;

                default:
                    System.Console.WriteLine("");
                    System.Console.WriteLine("Opção invalida! ");
                    break;


            }
        } while (opcao != 0);


    }
}



