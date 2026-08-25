using System;

/// <summary>
/// Coleção de vendedores da equipe comercial (máximo definido no construtor).
/// </summary>
public class Vendedores
{
    private Vendedor[] osVendedores;
    private int max;
    private int qtde;

    public Vendedores(int max)
    {
        this.max = max;
        this.qtde = 0;
        osVendedores = new Vendedor[max];
    }

    public int Qtde => qtde;
    public int Max => max;

    /// <summary>
    /// Adiciona um vendedor, respeitando o limite máximo e ids duplicados.
    /// </summary>
    public bool AddVendedor(Vendedor v)
    {
        if (qtde >= max) return false;
        if (SearchVendedor(v) != null) return false;

        osVendedores[qtde] = v;
        qtde++;
        return true;
    }

    /// <summary>
    /// Remove um vendedor, somente se ele não possuir vendas registradas.
    /// </summary>
    public bool DelVendedor(Vendedor v)
    {
        Vendedor encontrado = SearchVendedor(v);
        if (encontrado == null) return false;
        if (encontrado.TemVendas()) return false;

        int idx = Array.IndexOf(osVendedores, encontrado);
        for (int i = idx; i < qtde - 1; i++)
            osVendedores[i] = osVendedores[i + 1];

        osVendedores[qtde - 1] = null;
        qtde--;
        return true;
    }

    /// <summary>
    /// Busca um vendedor pelo id do objeto informado.
    /// </summary>
    public Vendedor SearchVendedor(Vendedor v)
    {
        if (v == null) return null;
        return SearchVendedorPorId(v.Id);
    }

    /// <summary>
    /// Busca auxiliar por id (facilita o uso a partir do menu, sem precisar
    /// montar um objeto Vendedor só para pesquisar).
    /// </summary>
    public Vendedor SearchVendedorPorId(int id)
    {
        for (int i = 0; i < qtde; i++)
            if (osVendedores[i].Id == id) return osVendedores[i];
        return null;
    }

    /// <summary>
    /// Soma o valor total de vendas de todos os vendedores.
    /// </summary>
    public double ValorVendas()
    {
        double total = 0;
        for (int i = 0; i < qtde; i++) total += osVendedores[i].ValorVendas();
        return total;
    }

    /// <summary>
    /// Soma o valor total de comissão de todos os vendedores.
    /// </summary>
    public double ValorComissao()
    {
        double total = 0;
        for (int i = 0; i < qtde; i++) total += osVendedores[i].ValorComissao();
        return total;
    }

    /// <summary>
    /// Retorna uma cópia da lista de vendedores atualmente cadastrados.
    /// </summary>
    public Vendedor[] Listar()
    {
        Vendedor[] lista = new Vendedor[qtde];
        Array.Copy(osVendedores, lista, qtde);
        return lista;
    }
}
