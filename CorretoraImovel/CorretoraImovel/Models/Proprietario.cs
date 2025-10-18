using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorretoraImovel.Models
{
    public class Proprietario
    {
        private string _nome;
        private string _telefone;
        private string _cpf;
        
        public bool CpfValido { get; private set; }

        public Proprietario(string nome, string telefone, string cpf)
        {
            _nome = nome;
            _telefone = telefone;
            _cpf = cpf;
            CpfValido = ValidarCpfSimples(cpf); // Chama a validação no construtor
        }
        
        private bool ValidarCpfSimples(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf)) return false;
            string cpfNumeros = new string(cpf.Where(char.IsDigit).ToArray());
            if (cpfNumeros.Length != 11) return false;
            if (cpfNumeros.Distinct().Count() == 1) return false;
            return true;
        }
        
        public string GetNome() { return _nome; }
        public string GetTelefone() { return _telefone; }
        public string GetCpf() { return _cpf; }

        public string ContatoProprietario()
        {
            return $"Proprietário: {_nome} | Tel: {_telefone} | CPF: {_cpf} ({(CpfValido ? "Válido" : "Inválido")})";
        }

        public string ExibirDetalhes()
        {
            return $"Nome: {_nome}\nTelefone: {_telefone}\nCPF: {_cpf} ({(CpfValido ? "Válido" : "Inválido")})";
        }
    }

}
