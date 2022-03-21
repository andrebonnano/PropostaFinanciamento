using System;
using System.Collections.Generic;
using PropostaFinanciamento.Enums;

namespace PropostaFinanciamento.Entidades
{
    public class Documento
    {
        public int Id { get; private set; }
        public string Numero { get; private set; }
        public string Imagem { get; private set; }
        public StatusDocumento Status { get; private set; }
        public TipoDocumento Tipo { get; private set; }
    }
}