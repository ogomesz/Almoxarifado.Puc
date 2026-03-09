internal class Estoque
{
    //List criada para armazenar os produtos no obejto criado "produtos"
    private List<Produto> produtos = new List<Produto>();

    //Nova list de movimentação (ira exibir o historico de retirada)
    private List<Movimentacao> historico = new List<Movimentacao>();


    //metodo sem retorno que tem como parametro o tipo Produto com o objeto p1
    public void Cadastrar(Produto p1)
    {
        //o obejeto criado na list<> adiciona o valor de p1 a List<>
        produtos.Add(p1);
    }

    //criado o metodo de registrar a entrada do produto com parametro de id e quantidade
    public void RegistrarEntrada(int id, int qtd)
    {
        //usa o Tipo da list<> e cria uma variavel para armazenar o id que foi encontrado  para verificar se o id fornecido foi encontrado 
        Produto pEncontrado = BuscarPorCodigo(id);

        //se o valor mensionado for "null" ele exibe que o produto nao foi encontrado 
        if (pEncontrado == null)
        {
            System.Console.WriteLine("Produto não encontrado! ");

        }
        else
        {
            //se não o objeto ira chamar o metodo de AdicionarEstoque e armazenar a quantidade 
            pEncontrado.AdicionarEstoque(qtd);

            //se o produto for encontrado ira fazer a movimentação de ENTRADA de produtos(ira adicionar), o nome a quantidade e Tipo (entrada ou saida) e a data da entrada
            Movimentacao h1 = new Movimentacao(pEncontrado.NomeProduto, qtd, "ENTRADA", DateTime.Now);

            //ira adicionar a list de movimentação essa entrada 
            historico.Add(h1);
        }

    }

    //metodo sem retorno que Lista os produtos
    public void LisarTudo()
    {

        //Ira percorrer item por item e exibir o Nome, Id e Estoque de cada produto. 
        foreach (Produto item in produtos)
        {
            //apos percorrer os produtos da lista chama o metodo exibir e mostar na tela o Id nome e quantidade em estoque
            item.Exibir();
        }
    }

    //cria o metodo de retorno Produto (obejto da List<>) e passa por parametro o id para buscar pelo id na lista
    public Produto BuscarPorCodigo(int id)
    {
        //percorre a List<> atras do id 
        foreach (Produto item in produtos)
        {
            //se o codigoID (padrao) for igual ao codigo inserido retorna o item (id) encontrado 
            if (item.CodigoID == id)
            {
                return item;
            }

        }
        //se não retorna valor null (ausencia de objeto \ id (item))
        return null;
    }

    //metodo para registrar a saida do item buscando pelo id
    public void RegistrarSaida(int id, int qtd)
    {
        //variavel do tipo Produto 
        Produto encontrado;

        //usa o metodo de buscar o produto ja criada
        //armazena o Produto em uma variavel que foi criada a partir do tipo (onde esta armazenado os produtos )
        BuscarPorCodigo(id);
        encontrado = BuscarPorCodigo(id);

        //verifica se o produto existe
        if (encontrado == null)
        {
            //se nao existir exibe essa mensagem -
            System.Console.WriteLine("Produto inexistente! ");

        }
        else
        {
            //se nao verifica se a quantidade de produto solicitada é maior que o estoque 
            if (encontrado.QuantidadeEstoque >= qtd)
            {
                // se suim usa o produto encontrado com a variavel "encontrado" e chama o metodo com a quantidade solicitada como parametro
                encontrado.RemoverEstoque(qtd);

                //aqui eu passo por parametro o nome do produto encontrado, a quantidade que foi passada por parametro no metodo de Saida, o nome de SAIDA que ira indicar a saida e o horario 
                Movimentacao h1 = new Movimentacao(encontrado.NomeProduto, qtd, "SAIDA", DateTime.Now);

                //e aqui eu adiciono essa movimentação na lista de historico 
                historico.Add(h1);
            }
            // se nao ira exibir a mensagem de estoque insuficiente 
            else
            {
                System.Console.WriteLine("Saldo Insuficiente! ");


            }
        }


    }

    //esta dentro do escopo onde pode ser acessado a List de tipo Movimentação
    public void ExibirHistotico()
    {
        foreach (Movimentacao item in historico)
        {
            //uso a variavel que percorre e armazena o produto e exibo 
            item.Exibir();
        }
    }
}