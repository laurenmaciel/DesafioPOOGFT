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

        // Propriedade pública para expor o resultado da validação
        public bool CpfValido { get; private set; }

        public Proprietario(string nome, string telefone, string cpf)
        {
            _nome = nome;
            _telefone = telefone;
            _cpf = cpf;
            CpfValido = ValidarCpfSimples(cpf); // Chama a validação no construtor
        }

        /// <summary>
        /// Realiza uma validação simples (formato, tamanho e dígitos repetidos) do CPF.
        /// NÃO IMPLEMENTA O CÁLCULO COMPLETO DOS DÍGITOS VERIFICADORES.
        /// </summary>
        private bool ValidarCpfSimples(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf)) return false;

            // 1. Limpa o CPF (mantém apenas dígitos)
            string cpfNumeros = new string(cpf.Where(char.IsDigit).ToArray());

            // 2. Verifica se possui 11 dígitos
            if (cpfNumeros.Length != 11) return false;

            // 3. Verifica se todos os dígitos são repetidos (ex: 11111111111, 22222222222)
            if (cpfNumeros.Distinct().Count() == 1) return false;

            // Se passamos pelos testes básicos (e simulação de validação), consideramos 'válido'
            return true;
        }

        // Getters públicos (Encapsulamento)
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
