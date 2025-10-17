using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorretoraImovel.Models
{
    public abstract class Imovel
    {
        // Atributos protected conforme Requisito 4.1
        protected int Id;
        protected string Endereco;
        protected int Numero;
        protected bool Alugado; // Controlado por métodos próprios (Encapsulamento)
        protected Proprietario Proprietario; // Composição

        public Imovel(int id, string endereco, int numero, Proprietario proprietario)
        {
            Id = id;
            Endereco = endereco;
            Numero = numero;
            Proprietario = proprietario;
            Alugado = false;
        }

        // Getters públicos (Encapsulamento)
        public int GetId() { return Id; }
        public string GetEndereco() { return Endereco; }
        public int GetNumero() { return Numero; }
        public bool GetAlugado() { return Alugado; }
        public Proprietario GetProprietario() { return Proprietario; }

        // Setters controlados (Encapsulamento)
        public void SetEndereco(string novoEndereco)
        {
            if (!string.IsNullOrWhiteSpace(novoEndereco))
            {
                Endereco = novoEndereco;
            }
        }

        // Métodos que alteram o status (Encapsulamento - Requisito 5.2)
        public void Alugar()
        {
            if (!Alugado)
            {
                Alugado = true;
            }
        }

        public void Disponibilizar()
        {
            if (Alugado)
            {
                Alugado = false;
            }
        }

        // Método Abstrato (Abstração)
        public abstract decimal CalcularAluguel(int dias);

        // Método Virtual (Polimorfismo)
        public virtual string ObterStatusAluguel()
        {
            return Alugado ? "Alugado" : "Disponível";
        }

        public virtual string ExibirInformacoes()
        {
            return $"ID: {Id}\n" +
                   $"Endereço: {Endereco}, {Numero}\n" +
                   $"{Proprietario.ExibirDetalhes()}\n" +
                   $"Status: {ObterStatusAluguel()}\n" +
                   $"Estimativa Aluguel Mensal (30 dias): R$ {CalcularAluguel(30):N2}";
        }
    }
}

