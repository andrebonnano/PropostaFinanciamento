using System;
using System.Collections.Generic;
using PropostaFinanciamento.Enums;

namespace PropostaFinanciamento.Entidades
{
    public class Proponente
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public IReadOnlyCollection<Documento> Documentos { get { return documentos; }}
        private List<Documento> documentos;
        public double RendaMensal { get; private set; }
        public bool Negativado { get; private set; }
        public string NegativadoMotivo { get; private set; }
        public Endereco Endereco { get; private set; }

        public Proponente() { }

        public Proponente(int id, string nome, DateTime dataNascimento, double rendaMensal, bool negativado, string negativadoMotivo, Endereco endereco)
        {
            Id = id;
            Nome = nome;
            DataNascimento = dataNascimento;
            RendaMensal = rendaMensal;
            Negativado = negativado;
            NegativadoMotivo = negativadoMotivo;
            Endereco = endereco;
        }

        public void AddDocumento()
        {

        }

        public string GetCpf()
        {
            string cpf = "465.789.321-45";
            return cpf;
        }
    }
}