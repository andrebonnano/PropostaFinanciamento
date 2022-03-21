using System;
using System.Globalization;
using System.Collections.Generic;
using PropostaFinanciamento.Enums;

namespace PropostaFinanciamento.Entidades
{
    public class Proposta
    {
        public int Id { get; private set; }
        public Imovel Imovel { get; private set; }
        public DateTime PropostaData { get; private set; }
        public int QtdPrestacoes { get; private set; }
        public double Entrada { get; private set; }
        public string PropostaDescr { get; private set; }
        public double Juros { get; private set; }
        public StatusProposta StatusProposta { get; private set; }
        public Enums.StatusDocumento StatusDocumento { get; private set; }
        public IReadOnlyCollection<Proponente> Proponentes { get { return proponentes; } }
        private List<Proponente> proponentes;

        public Proposta()
        {
        }

        public Proposta(List<Proponente> proponentes, Imovel imovel, int qtdPrestacoes, double entrada, double juros)
        {
            
            this.proponentes = proponentes;
            Imovel = imovel;
            QtdPrestacoes = qtdPrestacoes;
            Entrada = entrada;
            Juros = juros;
            Id = 24632;
            PropostaData = DateTime.Now;
            StatusProposta = StatusProposta.Criada;
            StatusDocumento = StatusDocumento.Enviado;
        }

        public void MontaProposta()
        {
            Console.Clear();
            Console.WriteLine("----------------------------------------------");
            Console.Write("Olá ");
            foreach (var proponente in Proponentes)
            {
                Console.Write(proponente.Nome);
                int i = Proponentes.Count;
                if (i > 1)
                {
                    Console.Write(" e ");
                    i--;
                }
            }
            Console.WriteLine(", segue a proposta para ser analisada:");
            Console.WriteLine();
            Console.WriteLine("O Imóvel a ser financiado tem o valor total de R$" + Imovel.Valor.ToString("F2", CultureInfo.InvariantCulture));
            Console.WriteLine("O valor de entrada é de R$" + Entrada.ToString("F2", CultureInfo.InvariantCulture));
            Console.WriteLine();
            Console.WriteLine("O valor financiado de R$" + (Imovel.Valor - Entrada).ToString("F2", CultureInfo.InvariantCulture) + " parcelado em " + QtdPrestacoes + " prestações");
            Console.WriteLine("O Juros aplicado será de " + Juros + "% ao ano.");
        }
        
        public void EnviarDadosProposta()
        {
            
        }
    }
}