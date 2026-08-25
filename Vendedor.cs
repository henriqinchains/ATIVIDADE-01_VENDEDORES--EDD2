using System;

/// <summary>
/// Representa um vendedor, com suas vendas registradas dia a dia (1 a 31).
/// </summary>
public class Vendedor
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public double PercComissao { get; set; }

    // Índice 0 = dia 1 ... índice 30 = dia 31
    private Venda[] asVendas = new Venda[31];

    public Vendedor(int id, string nome, double percComissao)
    {
        Id = id;
        Nome = nome;
        PercComissao = percComissao;
    }

    /// <summary>
    /// Expõe o array de vendas apenas para leitura (usado na consulta detalhada).
    /// </summary>
    public Venda[] AsVendas => asVendas;

    public bool RegistrarVenda(int dia, Venda venda)
    {
        if (dia < 1 || dia > 31) return false;
        asVendas[dia - 1] = venda;
        return true;
    }

    /// <summary>
    /// Soma o valor de todas as vendas registradas no mês.
    /// </summary>
    public double ValorVendas()
    {
        double total = 0;
        foreach (var v in asVendas)
            if (v != null) total += v.Valor;
        return total;
    }

    /// <summary>
    /// Calcula a comissão devida com base no valor total de vendas.
    /// </summary>
    public double ValorComissao()
    {
        return ValorVendas() * (PercComissao / 100.0);
    }

    /// <summary>
    /// Indica se o vendedor possui alguma venda registrada (usado na exclusão).
    /// </summary>
    public bool TemVendas()
    {
        foreach (var v in asVendas)
            if (v != null) return true;
        return false;
    }
}
