using System;
using System.Text.RegularExpressions;

namespace PropostaFinanciamento.Excessoes
{
    internal class Validator
    {
        public string StringData { get; set; }


        // Validar o CPF
        public bool ValidaCPF(string cpf)
        {
            Regex regex = new Regex("^[0-9.-]+$");
            if (!regex.IsMatch(cpf))
            {
                throw new CpfException();
            }
            else if (IsCpf(cpf))
            {
				return true;
			}
			throw new CpfException();
		}
		public static bool IsCpf(string cpf)
		{
			int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
			int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
			string tempCpf;
			string digito;
			int soma;
			int resto;
			cpf = cpf.Trim();
			cpf = cpf.Replace(".", "").Replace("-", "");
			if (cpf.Length != 11)
				return false;
			tempCpf = cpf.Substring(0, 9);
			soma = 0;

			for (int i = 0; i < 9; i++)
				soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
			resto = soma % 11;
			if (resto < 2)
				resto = 0;
			else
				resto = 11 - resto;
			digito = resto.ToString();
			tempCpf = tempCpf + digito;
			soma = 0;
			for (int i = 0; i < 10; i++)
				soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
			resto = soma % 11;
			if (resto < 2)
				resto = 0;
			else
				resto = 11 - resto;
			digito = digito + resto.ToString();
			return cpf.EndsWith(digito);
		}

		// Valida valores monetários
		public bool ValidaMoeda(double dados)
        {
			Regex regex = new Regex("^[0-9.]+$");
			if (!regex.IsMatch(dados.ToString()))
			{
				throw new MoedaException();
			}
			return true;
		}

		public bool ValidaSimNao(char dados)
		{
			Regex regex = new Regex("^[sSnN]+$");
			if (!regex.IsMatch(dados.ToString()))
			{
				throw new MoedaException();
			}
			return true;
		}
	}
}
