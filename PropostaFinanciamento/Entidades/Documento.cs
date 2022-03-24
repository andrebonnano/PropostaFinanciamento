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

        public Documento(TipoDocumento tipo)
        {
            Id = Guid.NewGuid().ToString();
            Tipo = tipo;
            Status = StatusDocumento.Enviado;
        }

        public void Upload(string caminho)
        {
            CaminhoImagem = caminho;
            Status = StatusDocumento.Enviado;
        }

        public void AprovarDocumento()
        {
            Status = StatusDocumento.Aprovado;
        }

        public void RecusarDocumento()
        {
            Status = StatusDocumento.Recusado;
        }
    }
}