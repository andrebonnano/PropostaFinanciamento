using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropostaFinanciamento.Excessoes
{
    internal class MoedaException : Exception
    {
        public MoedaException() : base(String.Format("O Valor deve conter apenas numeros e '.' como separador decimal!"))
        {
        }
    }
}
