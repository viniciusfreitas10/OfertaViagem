﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using JornadaMilhas.Constants;
using JornadaMilhasV1.Validador;

namespace JornadaMilhasV1.Modelos
{
    public class OfertaViagem : Valida
    {
        private double desconto;
        public const double DESCONTO_MAXIMO = 0.7;
        public int Id { get; set; }
        public Rota Rota { get; set; }
        public bool Ativa { get; set; } = true;
        public Periodo Periodo { get; set; }
        public double Preco { get; set; }
        public double Desconto
        {
            get => desconto;
            set
            {
                desconto = value;
                ValidaDesconto();
            }
        }

        public OfertaViagem(Rota rota, Periodo periodo, double preco)
        {
            Rota = rota;
            Periodo = periodo;
            Preco = preco;
            Validar();
        }

        public override string ToString()
        {
            return $"Origem: {Rota.Origem}, Destino: {Rota.Destino}, Data de Ida: {Periodo.DataInicial.ToShortDateString()}, Data de Volta: {Periodo.DataFinal.ToShortDateString()}, Preço: {Preco:C}";
        }

        protected override void Validar()
        {
            if (!Periodo.EhValido)
            {
                Erros.RegistrarErro(Periodo.Erros.Sumario);
            }
            
            if (Rota == null || Periodo == null)
            {
                Erros.RegistrarErro("A oferta de viagem não possui rota ou período válidos.");
            }

            if (Preco <= 0)
            {
                Erros.RegistrarErro(Constants.MensagemErroValorNegativo);
            }

            if(string.IsNullOrEmpty(Rota?.Destino) || string.IsNullOrEmpty(Rota?.Origem))
            {
                Erros.RegistrarErro("O Destino ou Origem não podem ser vazios");
            }
        }

        private void ValidaDesconto()
        {
            if (desconto >= Preco)
                Preco *= (1 - DESCONTO_MAXIMO);
            else if (desconto > 0)
                Preco -= desconto;
        }
    }
}