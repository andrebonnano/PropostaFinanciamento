using System;


namespace PropostaFinanciamento.Excessoes
{
    internal class DomainException : ApplicationException
    {
        public DomainException(string message) : base(message)
        {

        }
    }
}
