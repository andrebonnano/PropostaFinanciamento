﻿using System;
using System.Collections.Generic;
using PropostaFinanciamento.Enums;

namespace PropostaFinanciamento.Entidades
{
    public class Imovel
    {
        public string Id { get; private set; }
        public double Valor { get; private set; }
        public string InscrMunicipal { get; private set; }
        public Endereco Endereco { get; private set; }
        public TipoImovel TipoImovel { get; private set; }

        public Imovel()
        {
        }

        public Imovel(double valor, string inscrMunicipal, Endereco endereco, TipoImovel tipoImovel)
        {
            Id = Guid.NewGuid().ToString();
            Valor = valor;
            InscrMunicipal = inscrMunicipal;
            Endereco = endereco;
            TipoImovel = tipoImovel;
        }
    }
}