using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorretoraImovel.Models
{
    public class Corretora
    {
        private List<Imovel> _imoveis;
        private int _proximoId = 1;

        public Corretora()
        {
            _imoveis = new List<Imovel>();
        }

        public int ObterProximoId()
        {
            return _proximoId;
        }
        private void IncrementarId()
        {
            _proximoId++;
        }

        public Imovel BuscarImovelPorId(int id)
        {
            return _imoveis.FirstOrDefault(i => i.GetId() == id);
        }

        // Lógica: Cadastro
        public void CadastrarImovel(Imovel imovel)
        {
            if (_imoveis.Any(i => i.GetId() == imovel.GetId()))
            {
                System.Console.WriteLine("\nERRO: Já existe um imóvel com este ID. Cadastro cancelado.");
                return;
            }

            if (!imovel.GetProprietario().CpfValido)
            {
                System.Console.WriteLine($"\nAVISO: CPF do proprietário ({imovel.GetProprietario().GetCpf()}) é inválido. Imóvel cadastrado com ressalva.");
            }

            _imoveis.Add(imovel);
            IncrementarId();
            System.Console.WriteLine($"\nIMÓVEL CADASTRADO COM SUCESSO! (ID: {imovel.GetId()})");
            System.Console.WriteLine(imovel.ExibirInformacoes());
        }

        // Lógica: Exclusão
        public void DeletarImovel(int id)
        {
            Imovel imovelDeletado = BuscarImovelPorId(id);

            if (imovelDeletado != null)
            {
                _imoveis.Remove(imovelDeletado);
                System.Console.WriteLine($"\nIMÓVEL DELETADO COM SUCESSO!");
            }
            else
            {
                System.Console.WriteLine("\nERRO: ID de imóvel inválido.");
            }
        }

        // Lógica: Alugar (Inclui verificação de aluguel duplo)
        public bool AlugarImovel(int id)
        {
            Imovel imovel = BuscarImovelPorId(id);
            if (imovel != null)
            {
                if (!imovel.GetAlugado()) // Impedir aluguel duplo
                {
                    imovel.Alugar();
                    System.Console.WriteLine($"\nIMÓVEL ALUGADO COM SUCESSO! ID: {id}");
                    return true;
                }
                else
                {
                    System.Console.WriteLine("\nERRO: Este imóvel já está alugado. Aluguel duplo impedido.");
                    return false;
                }
            }
            else
            {
                System.Console.WriteLine("\nERRO: ID de imóvel inválido.");
                return false;
            }
        }

        // Lógica: Disponibilizar
        public bool DisponibilizarImovel(int id)
        {
            Imovel imovel = BuscarImovelPorId(id);
            if (imovel != null)
            {
                if (imovel.GetAlugado())
                {
                    imovel.Disponibilizar();
                    System.Console.WriteLine($"\nIMÓVEL DISPONIBILIZADO COM SUCESSO! ID: {id}");
                    return true;
                }
                else
                {
                    System.Console.WriteLine("\nERRO: Este imóvel já está disponível.");
                    return false;
                }
            }
            else
            {
                System.Console.WriteLine("\nERRO: ID de imóvel inválido.");
                return false;
            }
        }

        // Lógica: Cálculo
        public decimal CalcularAluguelTotal(int id, int dias)
        {
            Imovel imovel = BuscarImovelPorId(id);

            if (imovel != null)
            {
                if (dias <= 0)
                {
                    System.Console.WriteLine("\nERRO: O período (dias) deve ser maior que zero.");
                    return 0;
                }

                // Polimorfismo em ação
                decimal valorTotal = imovel.CalcularAluguel(dias);

                System.Console.WriteLine($"\nCÁLCULO DE ALUGUEL POR PERÍODO:");
                System.Console.WriteLine($"Imóvel: {imovel.GetEndereco()}, {imovel.GetNumero()} (ID: {id})");
                System.Console.WriteLine($"Valor Diário (Estimativa): R$ {(valorTotal / dias):N2}");
                System.Console.WriteLine($"Período: {dias} dias");
                System.Console.WriteLine($"Valor Total para {dias} dias: R$ {valorTotal:N2}");
                return valorTotal;
            }
            else
            {
                System.Console.WriteLine("\nERRO: ID de imóvel inválido.");
                return 0;
            }
        }

        // Lógica: Listagem
        public List<Imovel> ListarImoveis(bool apenasAlugados = false, bool apenasDisponiveis = false)
        {
            IEnumerable<Imovel> lista = _imoveis;

            if (apenasAlugados)
            {
                lista = lista.Where(i => i.GetAlugado());
            }
            else if (apenasDisponiveis)
            {
                lista = lista.Where(i => !i.GetAlugado());
            }

            return lista.ToList();
        }
    }

}
