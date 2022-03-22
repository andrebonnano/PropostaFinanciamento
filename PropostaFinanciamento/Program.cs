using System;
using System.IO;
using System.Globalization;
using System.Collections.Generic;
using PropostaFinanciamento.Entidades;
using PropostaFinanciamento.Enums;

namespace PropostaFinanciamento
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int qtdProponentes = 0;
            List<Proponente> proponentes = new List<Proponente>();

            Console.WriteLine("------------------------------------------");
            Console.WriteLine("Olá, Seja bem vindo!");
            Console.WriteLine("Preciso que você preencha os dados abaixo");
            Console.WriteLine("para darmos início à sua proposta!");
            Console.WriteLine();
            
            //////////////////// PROPONENTES /////////////////////
            //Proponentes
            Console.Write("Qual o numero de Proponentes? ");
            qtdProponentes = int.Parse(Console.ReadLine());
            for(int i = 0; i < qtdProponentes; i++)
            {
                //Inicia
                Console.WriteLine("------------------------------------------");
                Console.WriteLine("Dados do Proponente #" + (i + 1) + ":");

                //Nome
                Console.Write("Nome: ");
                string _nome = Console.ReadLine();

                //Data Nescimento
                Console.Write("Data de Nascimento (dd/MM/yyyy): ");
                DateTime _date = DateTime.Parse(Console.ReadLine());

                //Renda
                Console.Write("Renda Mensal: R$");
                double _renda = double.Parse(Console.ReadLine());

                //CPF Negativado?
                Console.Write("Está com CPF Negativado (s/n)? ");
                char _negChar = char.Parse(Console.ReadLine());
                bool _negativado;
                string _negativadoMotivo = "";
                if(_negChar == 's' || _negChar == 'S')
                {
                    _negativado = true;
                    Console.WriteLine("Qual o motivo da Negativação? ");
                    _negativadoMotivo = Console.ReadLine();
                }
                else
                {
                    _negativado = false;
                }

                //Endereço
                Console.WriteLine("Digite os dados do endereço do proponente:");
                Endereco enderecoProponente = InserirEndereco();
                Proponente _prop = new Proponente(i, _nome, _date, _renda, _negativado, _negativadoMotivo, enderecoProponente);
                proponentes.Add(_prop);

            }

            //////////////////// IMOVEL /////////////////////
            //Tipo de Imovel
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("Qual o tipo do imóvel que deseja Adquirir?");
            Console.WriteLine("Casa (C), Apartamento (A), Loja (L)");
            Console.WriteLine("Galpão (G) ou Terreno (T)");
            char _charImovel = char.Parse(Console.ReadLine());
            TipoImovel tipoImovel = TipoImovel.Casa;
            switch (_charImovel)
            {
                case 'C' :
                    tipoImovel = TipoImovel.Casa;
                    break;
                case 'A':
                    tipoImovel = TipoImovel.Apartamento;
                    break;
                case 'L':
                    tipoImovel = TipoImovel.Loja;
                    break;
                case 'G':
                    tipoImovel = TipoImovel.Galpao;
                    break;
                case 'T':
                    tipoImovel = TipoImovel.Terreno;
                    break;
            }

            //Valor do Imovel
            Console.Write("Qual o valor do total imóvel? R$");
            double valorTotal = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

            //Inscrição Municipal
            Console.Write("Qual o numero da Inscrição Estadual? ");
            string inscrMunic = Console.ReadLine();

            //Endereço do Imovel
            Console.WriteLine("Digite os dados do endereço do Imovel:");
            Endereco enderecoImovel = InserirEndereco();

            //Cria Objeto Imovel
            Imovel imovel = new Imovel(valorTotal, inscrMunic, enderecoImovel, tipoImovel, 1234);


            //////////////////// FINANCIAMENTO /////////////////////
            //Valor da entrada
            Console.Write("Qual será o valor de entrada? R$");
            double valorEntrada = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

            //Juros
            Console.Write("Qual será o juros anual? % ");
            double juros = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

            //Prestações
            Console.Write("Qual será a quantidade de prestações? ");
            int prestac = int.Parse(Console.ReadLine());


            //Cria objeto proposta
            Proposta proposta = new Proposta(proponentes, imovel, prestac, valorEntrada, juros );


            // Montando a proposta para exibir na tela
            proposta.MontaProposta();

            //Cria Arquivo JSON
            proposta.CriaArquivoJson();


            //////////////////// FUNÇÕES /////////////////////
            // Função para inserir o objeto de endereço
            Endereco InserirEndereco()
            {
                Console.Write("CEP: ");
                string _cep = Console.ReadLine();
                Console.Write("Nome da rua (sem numero): ");
                string _rua = Console.ReadLine();
                Console.Write("Numero da casa ou prédio: ");
                string _numero = Console.ReadLine();
                Console.Write("Complemento (caso não houver, digite enter): ");
                string _complemento = Console.ReadLine();
                Console.Write("Bairro: ");
                string _bairro = Console.ReadLine();
                Console.Write("Cidade: ");
                string _cidade = Console.ReadLine();
                Console.Write("Estado: ");
                string _estado = Console.ReadLine();
                Endereco objEndereco = new Endereco(_cep, _rua, _numero, _complemento, _bairro, _cidade, _estado);

                return objEndereco;
            }

        }
    }
}
