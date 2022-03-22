using System;
using System.IO;
using System.Text;
using System.Globalization;
using System.Collections.Generic;
using PropostaFinanciamento.Enums;

namespace PropostaFinanciamento.Entidades
{
    public class Proposta
    {
        public string Id = Guid.NewGuid().ToString();
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

            //Cria Arquivo JSON
            CriaArquivoJson();
        }

        public void CriaArquivoJson()
        {
            string path = @"..\..\Json\proposta.json";

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("{");
            sb.AppendLine($"    \"ID\": \"{Id}\",");
            sb.AppendLine($"    \"DataProposta\": \"{PropostaData}\",");
            sb.AppendLine($"    \"Status\": \"{StatusProposta}\",");
            sb.AppendLine($"    \"Entrada\": \"{Entrada}\",");
            sb.AppendLine($"    \"Prestacoes\": \"{QtdPrestacoes}\",");
            sb.AppendLine("    \"Proponentes\": [");
            int i = 0;
            foreach (var proponente in Proponentes)
            {
                sb.AppendLine("    {");
                sb.AppendLine($"        \"ID\": \"{proponentes[i].Id}\",");
                sb.AppendLine($"        \"Nome\": \"{proponentes[i].Nome}\",");
                sb.AppendLine($"        \"CPF\": \"{proponentes[i].CPF}\",");
                sb.AppendLine($"        \"DataNescimento\": \"{proponentes[i].DataNascimento}\",");
                sb.AppendLine($"        \"RendaMensal\": {proponentes[i].RendaMensal},");
                sb.AppendLine($"        \"Negativado\": {proponentes[i].Negativado},");
                sb.AppendLine($"        \"NegativadoMotivo\": \"{proponentes[i].NegativadoMotivo}\",");
                sb.AppendLine("        \"Endereço\": {");
                sb.AppendLine($"            \"CEP\": \"{proponentes[i].Endereco.CEP}\",");
                sb.AppendLine($"            \"Rua\": \"{proponentes[i].Endereco.Rua}\",");
                sb.AppendLine($"            \"Numero\": \"{proponentes[i].Endereco.Numero}\",");
                sb.AppendLine($"            \"Complemento\": \"{proponentes[i].Endereco.Complemento}\",");
                sb.AppendLine($"            \"Bairro\": \"{proponentes[i].Endereco.Bairro}\",");
                sb.AppendLine($"            \"Cidade\": \"{proponentes[i].Endereco.Cidade}\",");
                sb.AppendLine($"            \"Estado\": \"{proponentes[i].Endereco.Estado}\"");
                sb.AppendLine("        }");
                if (proponentes.Count == i + 1) { sb.AppendLine("    }],"); } else { sb.AppendLine("    },"); }
                i++;
            }
            sb.AppendLine("    \"Imovel\": {");
            sb.AppendLine($"        \"ID\": \"{Imovel.Id}\",");
            sb.AppendLine($"        \"Valor\": {Imovel.Valor},");
            sb.AppendLine($"        \"IscrMunicipal\": \"{Imovel.InscrMunicipal}\",");
            sb.AppendLine($"        \"Tipo\": \"{Imovel.TipoImovel}\",");
            sb.AppendLine("        \"Endereço\": {");
            sb.AppendLine($"            \"CEP\": \"{Imovel.Endereco.CEP}\",");
            sb.AppendLine($"            \"Rua\": \"{Imovel.Endereco.Rua}\",");
            sb.AppendLine($"            \"Numero\": \"{Imovel.Endereco.Numero}\",");
            sb.AppendLine($"            \"Complemento\": \"{Imovel.Endereco.Complemento}\",");
            sb.AppendLine($"            \"Bairro\": \"{Imovel.Endereco.Bairro}\",");
            sb.AppendLine($"            \"Cidade\": \"{Imovel.Endereco.Cidade}\",");
            sb.AppendLine($"            \"Estado\": \"{Imovel.Endereco.Estado}\"");
            sb.AppendLine("        }");
            sb.AppendLine("    }");
            sb.AppendLine("}");

            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(sb);
            }


        }
        
        public void EnviarDadosProposta()
        {
            
        }
    }
}