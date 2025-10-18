using CorretoraImovel.Models;

public class Program
{
    private static Corretora corretora = new Corretora();

    public static void Main(string[] args)
    {
        Proprietario prop1 = new Proprietario("João Silva", "9999-1111", "111.111.111-11");
        Proprietario prop2 = new Proprietario("Maria Souza", "8888-2222", "222.222.222-22");
        Proprietario prop3 = new Proprietario("José Teste", "7777-3333", "000.000.000-00"); 

        corretora.CadastrarImovel(new Casa(corretora.ObterProximoId(), "Rua dos Campos", 100, prop1, 3, 2, true, 250.50));
        corretora.CadastrarImovel(new Apartamento(corretora.ObterProximoId(), "Av. Central", 50, prop1, 5, false, 1, 85.70, 350.00m));

        Casa casaAlugada = new Casa(corretora.ObterProximoId(), "Rua do Sol", 25, prop2, 4, 3, true, 400.00);
        casaAlugada.Alugar(); 
        corretora.CadastrarImovel(casaAlugada);

        corretora.CadastrarImovel(new Apartamento(corretora.ObterProximoId(), "Rua do Erro", 99, prop3, 1, true, 0, 50.00, 100.00m));


        bool sair = false;
        while (!sair)
        {
            Console.Clear();
            ExibirMenu();
            string opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    CadastrarImovelUI();
                    break;
                case "2":
                    ExcluirImovelUI();
                    break;
                case "3":
                    AlugarImovelUI();
                    break;
                case "4":
                    DisponibilizarImovelUI();
                    break;
                case "5":
                    CalcularAluguelTotalUI();
                    break;
                case "6":
                    ListarImoveisUI();
                    break;
                case "7":
                    sair = true;
                    break;
                default:
                    Console.WriteLine("\nOpção inválida. Pressione qualquer tecla para continuar...");
                    Console.ReadKey();
                    break;
            }
        }
        Console.WriteLine("\nObrigado por usar a Corretora!");
    }

    private static void ExibirMenu()
    {
        Console.WriteLine("====================================");
        Console.WriteLine("  SISTEMA DE CORRETORA DE IMÓVEIS  ");
        Console.WriteLine("1. Cadastrar Imóvel (Casa/Apartamento)");
        Console.WriteLine("2. Excluir Imóvel");
        Console.WriteLine("3. Alugar Imóvel");
        Console.WriteLine("4. Disponibilizar Imóvel");
        Console.WriteLine("5. Calcular Valor do Aluguel por Período");
        Console.WriteLine("6. Listar Todos os Imóveis");
        Console.WriteLine("7. Sair");
        Console.Write("Escolha uma opção: \n");
        Console.WriteLine("====================================");
    }

    private static void PausarEContinuar()
    {
        Console.WriteLine("\nPressione ENTER para voltar ao Menu...");
        Console.ReadLine();
    }

    private static int ExibirListaEObterId(List<Imovel> lista, string titulo)
    {
        if (!lista.Any())
        {
            Console.WriteLine($"\n{titulo}: NENHUM IMÓVEL ENCONTRADO.");
            return -1;
        }

        Console.WriteLine($"\n=== {titulo} ===");
        foreach (var imovel in lista)
        {
            // Polimorfismo na exibição do status
            Console.WriteLine($"[ID: {imovel.GetId()}] - {imovel.GetEndereco()}, {imovel.GetNumero()} - Status: {imovel.ObterStatusAluguel()}");
        }

        Console.Write("Digite o ID do imóvel ou 0 para cancelar: ");
        if (int.TryParse(Console.ReadLine(), out int idSelecionado) && idSelecionado > 0)
        {
            if (lista.Any(i => i.GetId() == idSelecionado))
            {
                return idSelecionado;
            }
            Console.WriteLine("\nERRO: ID não corresponde a nenhum imóvel nesta lista.");
        }
        return -1;
    }

    private static Proprietario CadastrarProprietarioUI()
    {
        Console.WriteLine("\n--- DADOS DO PROPRIETÁRIO ---");
        Console.Write("Nome do Proprietário: ");
        string nomeProp = Console.ReadLine();

        Console.Write("Telefone do Proprietário: ");
        string telefoneProp = Console.ReadLine();

        Console.Write("CPF do Proprietário: ");
        string cpfProp = Console.ReadLine();

        Proprietario proprietario = new Proprietario(nomeProp, telefoneProp, cpfProp);

        if (!proprietario.CpfValido)
        {
            Console.WriteLine($"\nAVISO: O CPF digitado ({cpfProp}) é inválido.");
        }

        return proprietario;
    }

    private static void CadastrarImovelUI()
    {
        Console.Clear();
        Console.WriteLine("==========================");
        Console.WriteLine("    CADASTRO DE IMÓVEL    ");
        Console.WriteLine("==========================");

        Proprietario proprietario = CadastrarProprietarioUI();

        if (string.IsNullOrWhiteSpace(proprietario.GetNome()))
        {
            Console.WriteLine("\nERRO: Cadastro cancelado devido a falta de nome do proprietário.");
            PausarEContinuar();
            return;
        }

        Console.Write("\nEndereço: ");
        string endereco = Console.ReadLine();

        int numero = 0;
        while (numero <= 0)
        {
            Console.Write("Número: ");
            if (!int.TryParse(Console.ReadLine(), out numero) || numero <= 0)
            {
                Console.WriteLine("Entrada inválida. Digite um número positivo.");
            }
        }

        Console.Write("Tipo de Imóvel (C para Casa, A para Apartamento): ");
        string tipo = Console.ReadLine().ToUpper();

        int novoId = corretora.ObterProximoId();

        if (tipo == "C")
        {
            int quartos = 0, banheiros = 0;
            double areaTerreno = 0.0;

            Console.Write("Número de Quartos: "); int.TryParse(Console.ReadLine(), out quartos);
            Console.Write("Número de Banheiros: "); int.TryParse(Console.ReadLine(), out banheiros);
            Console.Write("Tem Garagem (S/N)? "); bool garagem = Console.ReadLine().ToUpper() == "S";
            Console.Write("Área do Terreno (m²): "); double.TryParse(Console.ReadLine(), out areaTerreno);

            Casa novaCasa = new Casa(novoId, endereco, numero, proprietario, quartos, banheiros, garagem, areaTerreno);
            corretora.CadastrarImovel(novaCasa);
        }
        else if (tipo == "A")
        {
            int andar = 0, vagas = 0;
            double areaUtil = 0.0;
            decimal taxaCondominio = 0.0m;

            Console.Write("Andar: "); int.TryParse(Console.ReadLine(), out andar);
            Console.Write("Tem Elevador (S/N)? "); bool elevador = Console.ReadLine().ToUpper() == "S";
            Console.Write("Número de Vagas de Garagem: "); int.TryParse(Console.ReadLine(), out vagas);
            Console.Write("Área Útil (m²): "); double.TryParse(Console.ReadLine(), out areaUtil);
            Console.Write("Taxa de Condomínio: R$ "); decimal.TryParse(Console.ReadLine(), out taxaCondominio);

            Apartamento novoApartamento = new Apartamento(novoId, endereco, numero, proprietario, andar, elevador, vagas, areaUtil, taxaCondominio);
            corretora.CadastrarImovel(novoApartamento);
        }
        else
        {
            Console.WriteLine("\nTipo de imóvel inválido. Cadastro cancelado.");
        }

        PausarEContinuar();
    }

    private static void ExcluirImovelUI()
    {
        Console.Clear();
        List<Imovel> lista = corretora.ListarImoveis();

        int id = ExibirListaEObterId(lista, "EXCLUIR IMÓVEL - SELECIONE O ID");
        if (id != -1)
        {
            corretora.DeletarImovel(id);
        }

        PausarEContinuar();
    }

    private static void ListarImoveisUI()
    {
        Console.Clear();
        List<Imovel> lista = corretora.ListarImoveis();

        if (lista.Any())
        {
            Console.WriteLine("=================================");
            Console.WriteLine("    LISTA COMPLETA DE IMÓVEIS    ");
            Console.WriteLine("=================================");
            foreach (var imovel in lista)
            {
                Console.WriteLine("========================");
                Console.WriteLine(imovel.ExibirInformacoes()); // Polimorfismo
            }
            Console.WriteLine("============================");
        }
        else
        {
            Console.WriteLine("Nenhum imóvel cadastrado.");
        }

        PausarEContinuar();
    }

    private static void AlugarImovelUI()
    {
        Console.Clear();
        List<Imovel> disponiveis = corretora.ListarImoveis(apenasDisponiveis: true);
        int id = ExibirListaEObterId(disponiveis, "ALUGAR IMÓVEL - DISPONÍVEIS");

        if (id != -1)
        {
            corretora.AlugarImovel(id);
        }
        PausarEContinuar();
    }

    private static void DisponibilizarImovelUI()
    {
        Console.Clear();
        List<Imovel> alugados = corretora.ListarImoveis(apenasAlugados: true);
        int id = ExibirListaEObterId(alugados, "DISPONIBILIZAR IMÓVEL - ALUGADOS");

        if (id != -1)
        {
            corretora.DisponibilizarImovel(id);
        }
        PausarEContinuar();
    }

    private static void CalcularAluguelTotalUI()
    {
        Console.Clear();

        List<Imovel> todosImoveis = corretora.ListarImoveis();

        int id = ExibirListaEObterId(todosImoveis, "CÁLCULO DE ALUGUEL - SELECIONE O IMÓVEL");

        if (id != -1)
        {
            Console.Write("Digite o número de dias para o cálculo: ");
            if (int.TryParse(Console.ReadLine(), out int dias) && dias > 0)
            {
                corretora.CalcularAluguelTotal(id, dias);
            }
            else
            {
                Console.WriteLine("\nERRO: Número de dias inválido. Deve ser maior que zero.");
            }
        }

        PausarEContinuar();
    }
}


