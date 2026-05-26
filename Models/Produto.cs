using System;

internal class Produto
{
    public int CodigoID { get; private set; }
    public string NomeProduto { get; private set; }
    public string Categoria { get; private set; }
<<<<<<< HEAD
    public string Fornecedor { get; private set; } 
    public int QuantidadeEstoque { get; private set; }

    public Produto(int CodigoID, string NomeProduto, string Categoria, string Fornecedor)
=======
    public int QuantidadeEstoque { get; private set; }

    public Produto(int CodigoID, string NomeProduto, string Categoria)
>>>>>>> cd80aa7e159c5e19cc0c9af4c968f0d0b6b6e6dc
    {
        this.CodigoID = CodigoID;
        this.NomeProduto = NomeProduto;
        this.Categoria = Categoria;
<<<<<<< HEAD
        this.Fornecedor = Fornecedor; 
=======
>>>>>>> cd80aa7e159c5e19cc0c9af4c968f0d0b6b6e6dc
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
<<<<<<< HEAD
        Console.WriteLine($"Cod: {CodigoID} | {NomeProduto} | Categoria: {Categoria} | Fornecedor: {Fornecedor} | Quantidade: {QuantidadeEstoque}");
=======
        Console.WriteLine($"Cod: {CodigoID} | {NomeProduto} | Categoria: {Categoria} | Quantidade: {QuantidadeEstoque}");
>>>>>>> cd80aa7e159c5e19cc0c9af4c968f0d0b6b6e6dc
    }
}