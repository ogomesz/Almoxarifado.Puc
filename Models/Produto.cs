using System;

internal class Produto
{
    public int CodigoID { get; private set; }
    public string NomeProduto { get; private set; }
    public string Categoria { get; private set; }
    public string Fornecedor { get; private set; } 
    public int QuantidadeEstoque { get; private set; }

    public Produto(int CodigoID, string NomeProduto, string Categoria, string Fornecedor)
    {
        this.CodigoID = CodigoID;
        this.NomeProduto = NomeProduto;
        this.Categoria = Categoria;
        this.Fornecedor = Fornecedor; 
        this.QuantidadeEstoque = 0; 
    }

    public void Estoque()
    {
        if (QuantidadeEstoque < 0)
        {
            Console.WriteLine("Precisa de Reposição!");
            QuantidadeEstoque = 0;
        }
    }

    public void RemoverEstoque(int valor)
    {
        QuantidadeEstoque -= valor;
    }

    public void AdicionarEstoque(int valor)
    {
        QuantidadeEstoque += valor;
    }

    public void Exibir()
    {
        Console.WriteLine($"Cod: {CodigoID} | {NomeProduto} | Categoria: {Categoria} | Fornecedor: {Fornecedor} | Quantidade: {QuantidadeEstoque}");
    }
}