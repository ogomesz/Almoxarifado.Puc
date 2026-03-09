//Criado a classe base para definir o controle de estoque 
internal class Produto
{
    //Codigo pode ser visto mas nao alterado, por que o codigo é padrão por produto
    public int CodigoID { get; private set; }
    //Nome do produto privado para que ninguem "sacaneie" o nome do produto mas é visivel
    public string NomeProduto { get; private set; }
    //quantidade visivel mas nao pode ser alterada por terceiros, para nao ser manipulado de modo errado
    public int QuantidadeEstoque { get; private set; }

    //contrutor criado para passar por parametro o codigo e o nome na hora da criação do objeto 
    public Produto(int CodigoID, string NomeProduto)
    {
        this.NomeProduto = NomeProduto;
        this.CodigoID = CodigoID;
    }
    //Metodo de estoque criado para verificar se o estoque precisa de reposição e não deicar ele ficar negativo 
    public void Estoque()
    {
        if (QuantidadeEstoque < 0)
        {
            System.Console.WriteLine("Precisa de Reposição!");
            QuantidadeEstoque = 0;
        }
    }
    //metodo para remover (quantidade) item do estoque 
    public void RemoverEstoque(int valor)
    {
        QuantidadeEstoque = QuantidadeEstoque - valor;
    }
    //metodo para adicionar item (quantidade )ao estoque 
    public void AdicionarEstoque(int valor)
    {
        QuantidadeEstoque = QuantidadeEstoque + valor;
    }

    //metodo para exibir codigo, nome, e quantidade 
    public void Exibir()
    {
        System.Console.WriteLine($"Cod: {CodigoID} | {NomeProduto} | Qunatidade: {QuantidadeEstoque}");
    }
}


