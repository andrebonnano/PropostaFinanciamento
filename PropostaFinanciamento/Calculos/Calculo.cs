using System;
using System.Globalization;
using System.Text;



namespace PropostaFinanciamento.Calculos
{
    internal class Calculo
    {
        public double ValorFinanciado { get; private set; }
        public int Prestacoes { get; private set; }
        public double JurosAnuais { get; private set; }

        public Calculo(double valorFinanciado, int prestacoes, double jurosAnuais)
        {
            ValorFinanciado = valorFinanciado;
            Prestacoes = prestacoes;
            JurosAnuais = jurosAnuais;
        }

        //Converte juros anuais para juros mensais
        public double AnualParaMensal()
        {
            double juros = JurosAnuais / 100;
            double jurosMensal = (Math.Pow(1.00 + juros, 1.00 / 12) - 1)*100;
            return jurosMensal;
        }

        //Gera a simulação do financiamento utilizando tabela SAC
        public string SimulaFinanciamento()
        {
            double BalanceDue = ValorFinanciado;
            double Amortization = ValorFinanciado / (double)Prestacoes;
            double Installment = 0;
            double _Fees = 0;
            double TotalPaid = 0;
            double TotalFees = 0;
            string specifier = "N";
            CultureInfo culture = CultureInfo.CreateSpecificCulture("pt-BR");
            StringBuilder sb = new StringBuilder();

            //Cria string para exibir as prestações detalhadas
            sb.AppendLine("---------------------------------------------------------------");
            for (int i = 0; i <= Prestacoes; i++)
            {
                if (i > 0)
                {
                    sb.AppendLine(
                        "Mes: " + i +
                        " | Saldo Devedor: R$ " + BalanceDue.ToString(specifier, culture) +
                        " | Juros: R$ " + _Fees.ToString(specifier, culture) +
                        " | Prestação: R$ " + Installment.ToString(specifier, culture));
                }

                _Fees = BalanceDue * (AnualParaMensal() / 100);
                Installment = Amortization + _Fees;
                BalanceDue -= Amortization;
                TotalPaid += Installment;
                TotalFees += _Fees;
            }
            sb.AppendLine("---------------------------------------------------------------");

            sb.AppendLine("Valor total pago pelo Financiamento - R$ " + TotalPaid.ToString(specifier, culture));
            sb.AppendLine("Total de jutos Pagos - R$ " + TotalFees.ToString(specifier, culture));

            return sb.ToString();
        }

        //Calcula a primeira parcela do financiamento
        public double PrimeiraParcela()
        {
            double primeiraParcela = (ValorFinanciado / (double)Prestacoes) + (ValorFinanciado * (AnualParaMensal() / 100));
            return primeiraParcela;
        }

    }
}
