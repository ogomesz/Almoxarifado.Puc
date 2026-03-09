internal class Movimentacao
{

    //foi criado as propriedades somente visivel e privadas somente os metodos presente nessa class possa alterar. 
    public string NomeProduto { get; private set; }
    public int Quantidade { get; private set; }
    public string Tipo { get; private set; }
    public DateTime DataHora { get; private set; }

    //criado o contrutor passando por parametro as propriedades
    public Movimentacao(string NomeProduto, int Quantidade, string Tipo, DateTime DataHora)
    {
        this.NomeProduto = NomeProduto;
        this.Quantidade = Quantidade;
        this.Tipo = Tipo;
        this.DataHora = DataHora;
    }

    //metoto de exibir que apos fazer todo o proceso ira exibir o nome quantidade tipo (se é entrada ou saida) e a data (da retirada ou entarada)
    public void Exibir()
    {
        System.Console.WriteLine($"{NomeProduto} | {Quantidade} - {Tipo} - {DataHora}");
    }


}