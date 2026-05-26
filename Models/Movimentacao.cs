using System;

internal class Movimentacao
{
    public string NomeProduto { get; private set; }
    public int Quantidade { get; private set; }
    public string Tipo { get; private set; }
    public DateTime DataHora { get; private set; }

    public Movimentacao(string NomeProduto, int Quantidade, string Tipo, DateTime DataHora)
    {
        this.NomeProduto = NomeProduto;
        this.Quantidade = Quantidade;
        this.Tipo = Tipo;
        this.DataHora = DataHora;
    }

    public void Exibir()
    {
        Console.WriteLine($"{NomeProduto} | QTD: {Quantidade} - {Tipo} - {DataHora}");
    }
}