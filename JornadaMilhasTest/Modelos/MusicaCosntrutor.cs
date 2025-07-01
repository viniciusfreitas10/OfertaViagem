using JornadaMilhas.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace JornadaMilhasTest.Modelos
{
    public class MusicaCosntrutor
    {
        [Fact]
        public void DeveRetornarOAnoDeLancamentoComoNuloSeMenorOuIgualAZero()
        {
            //arrange
            Musica musica = new Musica("Noite Feliz", 0, "Vinicius");
            //act
            musica.AnoLancamento = 0;
            //asert
            Assert.Null(musica.AnoLancamento);
        }

        [Fact]
        public void DeveRetornarOArtiscaComoDesconhecidoCasoNuloOuVazio()
        {
            //arrange
            Musica musica = new Musica("Noite Feliz", 0, "");
            string artistaEsperado =  "Desconhecido";
            //act
            musica.Artista = "";
            //asert
            Assert.Equal(artistaEsperado, musica.Artista);
        }
    }
}
