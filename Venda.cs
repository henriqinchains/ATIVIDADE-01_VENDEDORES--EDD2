using System;

/// <summary>
/// Representa o registro de vendas de um vendedor em um único dia.
/// </summary>
public class Venda
{
    public int Qtde { get; set; }
    public double Valor { get; set; }

    public Venda(int qtde, double valor)
    {
        Qtde = qtde;
        Valor = valor;
    }

    /// <summary>
    /// Valor médio de cada venda no dia (valor total / quantidade de vendas).
    /// </summary>
    public double ValorMedio()
    {
        if (Qtde == 0) return 0;
        return Valor / Qtde;
    }
}
