using System;
using System.Collections.Generic;
using PropostaFinanciamento.Enums;

namespace PropostaFinanciamento.Entidades
{
    public class Documento
    {
        public string Id { get; private set; }
        public string CaminhoImagem { get; private set; }
        public StatusDocumento Status { get; private set; }
        public TipoDocumento Tipo { get; private set; }

        public Documento(string caminhoImagem, TipoDocumento tipo)
        {
            Id = Guid.NewGuid().ToString();
            CaminhoImagem = caminhoImagem;
            Tipo = tipo;
            Status = StatusDocumento.Enviado;
        }
    }
}