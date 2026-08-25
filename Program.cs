using System;
using System.Globalization;

class Program
{
    static Vendedores vendedores = new Vendedores(10);

    static void Main()
    {
        int opcao;
        do
        {
            ExibirMenu();
            opcao = LerInt("Escolha uma opção: ");
            Console.WriteLine();

            switch (opcao)
            {
                case 0:
                    Console.WriteLine("Encerrando...");
                    break;
                case 1:
                    CadastrarVendedor();
                    break;
                case 2:
                    ConsultarVendedor();
                    break;
                case 3:
                    ExcluirVendedor();
                    break;
                case 4:
                    RegistrarVenda();
                    break;
                case 5:
                    ListarVendedores();
                    break;
                default:
                    Console.WriteLine("Opção inválida!");
                    break;
            }

            Console.WriteLine();
        } while (opcao != 0);
    }

    static void ExibirMenu()
    {
        Console.WriteLine("===== MENU VENDEDORES =====");
        Console.WriteLine("0. Sair");
        Console.WriteLine("1. Cadastrar vendedor");
        Console.WriteLine("2. Consultar vendedor");
        Console.WriteLine("3. Excluir vendedor");
        Console.WriteLine("4. Registrar venda");
        Console.WriteLine("5. Listar vendedores");
        Console.WriteLine("============================");
    }

    // (*) Cadastrar vendedor, limitado a 10
    static void CadastrarVendedor()
    {
        Console.WriteLine("--- Cadastro de Vendedor ---");

        if (vendedores.Qtde >= vendedores.Max)
        {
            Console.WriteLine("Limite máximo de 10 vendedores atingido.");
            return;
        }

        int id = LerInt("Id: ");
        if (vendedores.SearchVendedorPorId(id) != null)
        {
            Console.WriteLine("Já existe um vendedor com esse id.");
            return;
        }

        Console.Write("Nome: ");
        string nome = Console.ReadLine();

        double perc = LerDouble("Percentual de comissão (ex: 5 para 5%): ");

        Vendedor v = new Vendedor(id, nome, perc);
        bool ok = vendedores.AddVendedor(v);

        Console.WriteLine(ok
            ? "Vendedor cadastrado com sucesso!"
            : "Não foi possível cadastrar o vendedor.");
    }

    // (**) Consultar vendedor: id, nome, total de vendas, comissão e valor médio por dia
    static void ConsultarVendedor()
    {
        Console.WriteLine("--- Consulta de Vendedor ---");
        int id = LerInt("Id do vendedor: ");

        Vendedor v = vendedores.SearchVendedorPorId(id);
        if (v == null)
        {
            Console.WriteLine("Vendedor não encontrado.");
            return;
        }

        Console.WriteLine($"Id: {v.Id}");
        Console.WriteLine($"Nome: {v.Nome}");
        Console.WriteLine($"Valor total de vendas: {v.ValorVendas():C2}");
        Console.WriteLine($"Valor da comissão: {v.ValorComissao():C2}");

        Console.WriteLine("Valor médio das vendas diárias (dias com registro):");
        bool algumaVenda = false;
        for (int dia = 1; dia <= 31; dia++)
        {
            Venda venda = v.AsVendas[dia - 1];
            if (venda != null)
            {
                algumaVenda = true;
                Console.WriteLine($"  Dia {dia:00}: qtde={venda.Qtde}, valor total={venda.Valor:C2}, valor médio={venda.ValorMedio():C2}");
            }
        }

        if (!algumaVenda)
            Console.WriteLine("  Nenhuma venda registrada neste mês.");
    }

    // (***) Excluir vendedor, apenas se não houver vendas associadas
    static void ExcluirVendedor()
    {
        Console.WriteLine("--- Exclusão de Vendedor ---");
        int id = LerInt("Id do vendedor: ");

        Vendedor v = vendedores.SearchVendedorPorId(id);
        if (v == null)
        {
            Console.WriteLine("Vendedor não encontrado.");
            return;
        }

        bool ok = vendedores.DelVendedor(v);
        Console.WriteLine(ok
            ? "Vendedor excluído com sucesso!"
            : "Não é possível excluir: vendedor possui vendas registradas.");
    }

    static void RegistrarVenda()
    {
        Console.WriteLine("--- Registro de Venda ---");
        int id = LerInt("Id do vendedor: ");

        Vendedor v = vendedores.SearchVendedorPorId(id);
        if (v == null)
        {
            Console.WriteLine("Vendedor não encontrado.");
            return;
        }

        int dia = LerInt("Dia (1-31): ");
        if (dia < 1 || dia > 31)
        {
            Console.WriteLine("Dia inválido.");
            return;
        }

        int qtde = LerInt("Quantidade de vendas no dia: ");
        double valor = LerDouble("Valor total das vendas no dia: ");

        Venda venda = new Venda(qtde, valor);
        v.RegistrarVenda(dia, venda);

        Console.WriteLine("Venda registrada com sucesso!");
    }

    // (****) Listar vendedores: id, nome, total de vendas e comissão, com totalização final
    static void ListarVendedores()
    {
        Console.WriteLine("--- Listagem de Vendedores ---");
        Vendedor[] lista = vendedores.Listar();

        if (lista.Length == 0)
        {
            Console.WriteLine("Nenhum vendedor cadastrado.");
            return;
        }

        Console.WriteLine($"{"Id",-5}{"Nome",-20}{"Vendas",15}{"Comissão",15}");

        double totalVendas = 0;
        double totalComissao = 0;

        foreach (var v in lista)
        {
            Console.WriteLine($"{v.Id,-5}{v.Nome,-20}{v.ValorVendas(),15:C2}{v.ValorComissao(),15:C2}");
            totalVendas += v.ValorVendas();
            totalComissao += v.ValorComissao();
        }

        Console.WriteLine(new string('-', 55));
        Console.WriteLine($"{"TOTAL",-25}{totalVendas,15:C2}{totalComissao,15:C2}");
    }

    static int LerInt(string mensagem)
    {
        int valor;
        Console.Write(mensagem);
        while (!int.TryParse(Console.ReadLine(), out valor))
        {
            Console.Write("Valor inválido. Digite um número inteiro: ");
        }
        return valor;
    }

    static double LerDouble(string mensagem)
    {
        double valor;
        Console.Write(mensagem);
        while (!double.TryParse(Console.ReadLine(), NumberStyles.Any, CultureInfo.InvariantCulture, out valor))
        {
            Console.Write("Valor inválido. Use ponto para decimais (ex: 1500.50): ");
        }
        return valor;
    }
}
