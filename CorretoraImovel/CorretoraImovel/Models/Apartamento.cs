using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorretoraImovel.Models
{
    public class Apartamento : Imovel
    {
        protected int Andar;
        protected bool TemElevador;
        protected int NumeroVagasGaragem;
        protected double Areautil;
        protected decimal TaxaCondominio;

        public Apartamento(int id, string endereco, int numero, Proprietario proprietario,
                           int andar, bool temElevador, int numeroVagasGaragem,
                           double areautil, decimal taxaCondominio)
            : base(id, endereco, numero, proprietario)
        {
            Andar = andar;
            TemElevador = temElevador;
            NumeroVagasGaragem = numeroVagasGaragem;
            Areautil = areautil;
            TaxaCondominio = taxaCondominio;
        }

        public override decimal CalcularAluguel(int dias)
        {
            decimal valorBaseMensal = 800m;
            decimal valorAreaMensal = (decimal)(Areautil * 2.0);
            decimal valorAluguelMensal = valorBaseMensal + valorAreaMensal + TaxaCondominio;
            decimal valorDiario = valorAluguelMensal / 30m;

            return valorDiario * dias;
        }

        public override string ObterStatusAluguel()
        {
            return Alugado ? $"O apartamento nº {Numero} está alugado" : $"O apartamento nº {Numero} está disponível";
        }

        public override string ExibirInformacoes()
        {
            return base.ExibirInformacoes() +
                       $"\nTipo: Apartamento\n" +
                       $"Andar: {Andar}\n" +
                       $"Elevador: {(TemElevador ? "Sim" : "Não")}\n" +
                       $"Vagas Garagem: {NumeroVagasGaragem}\n" +
                       $"Área útil: {Areautil:N2}m²\n" +
                       $"Taxa de Condomínio: R$ {TaxaCondominio:N2}";
        }
    }
}

