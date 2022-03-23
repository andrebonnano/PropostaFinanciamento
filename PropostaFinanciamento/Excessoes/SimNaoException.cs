using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropostaFinanciamento.Excessoes
{
    internal class SimNaoException : Exception
    {
        public SimNaoException() : base(String.Format("O valor precisa ser 'S' pasa sim ou 'N' para não!"))
        {
        }
    }
}
