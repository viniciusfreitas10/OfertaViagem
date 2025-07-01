using JornadaMilhasV1.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace JornadaMilhasTest.Modelos
{
    public class OfertaViagemDesconto
    {

        [Theory]
        [InlineData("", null, "2024-01-01", "2024-01-02", 0, false)]
        [InlineData("São Paulo", "Salvador", "2024-02-01", "2024-03-02", 100, true)]
        [InlineData("", "alagoinhas", "2024-02-01", "2024-03-02", 100, false)]
        [InlineData("São Paulo", "Salvador", "2024-02-01", "2024-02-01", 0, false)]
        [InlineData("São Paulo", "Salvador", "2024-02-01", "2024-03-02", -100, false)]
        public void RetornaPrecoAtualizadoQuandoAplicadoDesconto(string origem, string destino, string dataIda, string dataVolta,
                                                            double preco, bool validacao)
        {
            //arrange
            Rota rota = new(origem, destino);
            Periodo periodo = new(DateTime.Parse(dataIda), DateTime.Parse(dataVolta));
            double precoOriginal = 100.00;
            double desconto = 20.00;
            double precoComDesconto = precoOriginal - desconto;
            OfertaViagem oferta = new OfertaViagem(rota,periodo, precoOriginal);

            //act
            oferta.Desconto = desconto;
            //assert
            Assert.Equal(precoComDesconto, oferta.Preco);
        }

        [Theory]
        [InlineData("São Paulo", "Salvador", "2024-02-01", "2024-03-02", 120, 30)]
        [InlineData("São Paulo", "Salvador", "2024-02-01", "2024-02-01", 100, 30)]
        public void RetornaDescontoMaximoQuandoValorDescontoMaiorOuIgualAoPreco(string origem, string destino, string dataIda, string dataVolta,
                                                            double desconto, double precoComDesconto)
        {
            //arrange
            Rota rota = new(origem, destino);
            Periodo periodo = new(DateTime.Parse(dataIda), DateTime.Parse(dataVolta));
            double precoOriginal = 100.00;
            OfertaViagem oferta = new OfertaViagem(rota, periodo, precoOriginal);

            //act
            oferta.Desconto = desconto;
            //assert
            Assert.Equal(precoComDesconto, oferta.Preco, 2);
        }

        [Theory]
        [InlineData("São Paulo", "Salvador", "2024-02-01", "2024-03-02", 100, true)]
        [InlineData("São Paulo", "Salvador", "2024-02-01", "2024-02-01", 0, false)]
        public void RetornaPrecoQuandoDescontoNegativo(string origem, string destino, string dataIda, string dataVolta,
                                                            double preco, bool validacao)
        {
            //arrange
            Rota rota = new(origem, destino);
            Periodo periodo = new(DateTime.Parse(dataIda), DateTime.Parse(dataVolta));
            double precoOriginal = 100.00;
            double desconto = -10.00;
            double precoComDesconto = precoOriginal;
            OfertaViagem oferta = new OfertaViagem(rota, periodo, precoOriginal);

            //act
            oferta.Desconto = desconto;
            //assert
            Assert.Equal(precoComDesconto, oferta.Preco, 2);
        }
    }
}
