using JornadaMilhas.Constants;
using JornadaMilhasV1.Modelos;
using System;
using System.Linq;
using Xunit;

namespace JornadaMilhasTest.Modelos
{
    public class OfertaViagemConstrutor
    {
        [Theory]
        [InlineData("", null, "2024-01-01", "2024-01-02", 0, false)]
        [InlineData("São Paulo", "Salvador", "2024-02-01", "2024-03-02", 100, true)]
        [InlineData("", "alagoinhas", "2024-02-01", "2024-03-02", 100, false)]
        [InlineData("São Paulo", "Salvador", "2024-02-01", "2024-02-01", 0, false)]
        [InlineData("São Paulo", "Salvador", "2024-02-01", "2024-03-02", -100, false)]
        public void RetornaEhValidoDeAcordoComDadosDeEntrada(string origem, string destino, string dataIda, string dataVolta,
                                                            double preco, bool validacao)
        {
            //arrange
            Rota rota = new (origem, destino);
            Periodo periodo = new(DateTime.Parse(dataIda),DateTime.Parse(dataVolta));

            //acao - act
            OfertaViagem ofertaViagem = new(rota, periodo, preco);

            //validação  - assert
            Assert.Equal(validacao, ofertaViagem.EhValido);

        }

        [Fact]
        public void RetornaMensagemErroQuandoRotaNula()
        {
            double preco = 200;
            DateTime periodoInicial = new(2024, 2, 1);
            DateTime periodoFinal = new DateTime(2024, 3, 10);

            Rota rota = null;
            Periodo periodo = new(periodoInicial, periodoFinal);
            OfertaViagem ofertaViagem = new(rota, periodo, preco);

            Assert.Contains("A oferta de viagem não possui rota ou período válidos.", ofertaViagem.Erros.Sumario);
            Assert.False(ofertaViagem.EhValido);

        }

        [Fact]
        public void RetornaMensagemErroQuandoDatafinalForMaiorQueDataInicial()
        {
            // arrange - cenário
            string origem = "SSA";
            string destino = "VIX";
            double preco = 650;
            DateTime periodoInicial = new(2024, 5, 15);
            DateTime periodoFinal = new(2024, 4, 10);

            Rota rota = new(origem, destino);
            Periodo periodo = new(periodoInicial, periodoFinal);

            //act - ação
            OfertaViagem ofertaViagem = new(rota, periodo, preco);

            //assert - validação
            Assert.Contains(Constants.MensagemErroDataFinalMaiorDataInicial, ofertaViagem.Erros.Sumario);
            Assert.False(ofertaViagem.EhValido);
        }

        [Theory]
        [InlineData("Alagoinhas", "Salvador", "2024-03-22", "2024-03-25", 0)]
        [InlineData("Alagoinhas", "Salvador", "2024-03-22", "2024-03-25", -50)]
        public void RetornaMensagemErroQuandoPrecoIgualOuMenorQueZero(string origem, string destino, string dataIda, string dataVolta,
                                                            double preco)
        {
            //arrange
            Rota rota = new(origem, destino);
            Periodo periodo = new(DateTime.Parse(dataIda), DateTime.Parse(dataVolta));

            //act
            OfertaViagem ofertaViagem = new(rota, periodo, preco);

            //assert
            Assert.Contains(Constants.MensagemErroValorNegativo, ofertaViagem.Erros.Sumario);
        }

        [Theory]
        [InlineData("Alagoinhas", "Salvador", "2024-03-22", "2024-03-25", 0)]
        [InlineData("Alagoinhas", "Salvador", "2024-03-22", "2024-03-25", -50)]
        public void RetornaMensagemErroQuandoPrecoIgualOuMenorQueZero1(string origem, string destino, string dataIda, string dataVolta,
                                                            double preco)
        {
            //arrange
            Rota rota = new(origem, destino);
            Periodo periodo = new(DateTime.Parse(dataIda), DateTime.Parse(dataVolta));

            //act
            OfertaViagem ofertaViagem = new(rota, periodo, preco);

            //assert
            Assert.Contains(Constants.MensagemErroValorNegativo, ofertaViagem.Erros.Sumario);
        }

        [Theory]
        [InlineData("Alagoinhas", "Salvador", "2024-03-22", "2024-03-25", -10)]
        [InlineData("Alagoinhas", "Salvador", "2024-03-22", "2024-03-25", -50)]
        public void RetornaTresErrosDeValidacaoQuandoRotaPeriodoEPrecoInvalidos(string origem, string destino, string dataIda, string dataVolta,
                                                            double preco)
        {
            //arrange
            int quantidadeEsperada = 3;
            Rota rota = null;
            Periodo periodo = new(DateTime.Parse(dataIda), DateTime.Parse(dataVolta));

            //act
            OfertaViagem ofertaViagem = new(rota, periodo, preco);

            //assert
            Assert.Equal(quantidadeEsperada, ofertaViagem.Erros.Count());
        }
    }
}
