using System;
using System.Globalization;
using System.Text;



namespace PropostaFinanciamento.Calculos
{
    internal class CalculoJuros
    {
        public double ValorFinanciado { get; private set; }
        public int Prestacoes { get; private set; }
        public double JurosAnuais { get; private set; }

        public CalculoJuros(double valorFinanciado, int prestacoes, double jurosAnuais)
        {
            ValorFinanciado = valorFinanciado;
            Prestacoes = prestacoes;
            JurosAnuais = jurosAnuais;
        }

        public double AnualParaMensal()
        {
            double juros = JurosAnuais / 100;
            double jurosMensal = (Math.Pow(1.00 + juros, 1.00 / 12) - 1)*100;
            return jurosMensal;
        }

        public string SimulaFinanciamento()
        {

            //Calcuros
            double BalanceDue = ValorFinanciado;
            double Amortization = ValorFinanciado / (double)Prestacoes;
            double Installment = 0;
            double _Fees = 0;
            double TotalPaid = 0;
            double TotalFees = 0;
            string specifier = "N";
            CultureInfo culture = CultureInfo.CreateSpecificCulture("pt-BR");
            StringBuilder sb = new StringBuilder();

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

    }
}
