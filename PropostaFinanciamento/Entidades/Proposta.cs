using System;
using System.IO;
using System.Text;
using System.Globalization;
using System.Collections.Generic;
using PropostaFinanciamento.Enums;
using PropostaFinanciamento.Calculos;

namespace PropostaFinanciamento.Entidades
{
    public class Proposta
    {
        public string Id { get; private set; }
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
            Id = Guid.NewGuid().ToString();
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
            Calculo calc = new Calculo(Imovel.Valor - Entrada, QtdPrestacoes, Juros);

            //Soma a renda dos proponentes
            double somaRenda = 0;
            double _primeiraParcela = calc.PrimeiraParcela();
            foreach (var proponente in proponentes)
            {
                somaRenda += proponente.RendaMensal;
            }

            //Verifica se a Soma da renda viabiliza o financiamento
            if (_primeiraParcela > (somaRenda * 0.3))
            {
                //Mensagem de negação do financiamento
                Console.Clear();
                Console.WriteLine("--------------------------------------------------------");
                Console.WriteLine("Para que sua proposta sejá enviada, a soma da ");
                Console.WriteLine("renda dos proponentes precisa ser maior que 30% ");
                Console.WriteLine("do valor da primaira prestação!");
                Console.WriteLine();
                Console.WriteLine("Soma da renda informada: R$ " + somaRenda.ToString("F2", CultureInfo.InvariantCulture));
                Console.WriteLine("Valor da primeira prestação: R$ " + _primeiraParcela.ToString("F2", CultureInfo.InvariantCulture));
                Console.WriteLine("Valor de renda mínima necessária: R$ " +  (_primeiraParcela / 0.3).ToString("F2", CultureInfo.InvariantCulture));
                Console.WriteLine();
                Console.WriteLine("Infelizmente seu financiamento não pode ser aprovado!");
                Console.WriteLine("--------------------------------------------------------");

                //Status Proposta Recusada
                StatusProposta = StatusProposta.Recusada;
            }
            else
            {   
                //Exibe a proposta montada
                Console.Clear();
                Console.WriteLine("----------------------------------------------");
                Console.Write("Olá ");
                foreach (var proponente in Proponentes)
                {
                    Console.Write(proponente.Nome + ", ");
                }
                Console.WriteLine("Segue abaixo, proposta para ser analisada:");
                Console.WriteLine();
                Console.WriteLine("Valor total do imóvel - R$" + Imovel.Valor.ToString("F2", CultureInfo.InvariantCulture));
                Console.WriteLine("Valor da Entrada - R$" + Entrada.ToString("F2", CultureInfo.InvariantCulture));
                Console.WriteLine("Valor a ser financiado -  R$" + (Imovel.Valor - Entrada).ToString("F2", CultureInfo.InvariantCulture));
                Console.WriteLine("Quantidade de prestações - " + QtdPrestacoes);
                Console.WriteLine("Juros anuais - " + Juros + "% ao ano.");
                Console.WriteLine("----------------------------------------------");
                Console.WriteLine("Segue abaixo a simulação do parcelamento:");
                
                //Exibe as parcelas utilizando tabela SAC
                Console.WriteLine(calc.SimulaFinanciamento());
            }

            //Cria Arquivo JSON da proposta
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