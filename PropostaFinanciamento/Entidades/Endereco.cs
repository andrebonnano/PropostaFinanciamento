using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PropostaFinanciamento.Entidades
{
    public class Endereco
    {
        public string CEP { get; private set; }
        public string Rua { get;  private set; }
        public string Numero { get; private set; }
        public string Complemento { get; private set; }
        public string Bairro { get; private set; }
        public string Cidade { get; private set; }
        public string Estado { get; private set; }

        public Endereco()
        {
        }

        public Endereco(string cep, string rua, string numero, string complemento, string bairro, string cidade, string estado)
        {
            CEP = cep;
            Rua = rua;
            Numero = numero;
            Complemento = complemento;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;
        }
    }
}