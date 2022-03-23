using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropostaFinanciamento.Excessoes
{
    internal class CpfException : Exception
    {
        public CpfException() : base(String.Format("O CPF que você inseriu é inválido!"))
        {
        }
    }
}
