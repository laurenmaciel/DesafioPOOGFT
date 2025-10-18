using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorretoraImovel.Models
{
    public class Casa : Imovel
    {
        protected int NumeroQuartos;
        protected int NumeroBanheiros;
        protected bool TemGaragem;
        protected double AreaTerreno;

        public Casa(int id, string endereco, int numero, Proprietario proprietario,
                    int numeroQuartos, int numeroBanheiros, bool temGaragem, double areaTerreno)
            : base(id, endereco, numero, proprietario)
        {
            NumeroQuartos = numeroQuartos;
            NumeroBanheiros = numeroBanheiros;
            TemGaragem = temGaragem;
            AreaTerreno = areaTerreno;
        }

        public override decimal CalcularAluguel(int dias)
        {
            decimal valorBaseMensal = 1000m;
            decimal valorAreaMensal = (decimal)(AreaTerreno * 3.0);
            decimal valorAluguelMensal = valorBaseMensal + valorAreaMensal;
            decimal valorDiario = valorAluguelMensal / 30m;

            return valorDiario * dias;
        }

        public override string ObterStatusAluguel()
        {
            return Alugado ? "A casa está alugada" : "A casa está disponível";
        }

        public override string ExibirInformacoes()
        {
            return base.ExibirInformacoes() +
                       $"\nTipo: Casa\n" +
                       $"Quartos: {NumeroQuartos}\n" +
                       $"Banheiros: {NumeroBanheiros}\n" +
                       $"Garagem: {(TemGaragem ? "Sim" : "Não")}\n" +
                       $"Área do terreno: {AreaTerreno:N2}m²";
        }
    }
}


