using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JornadaMilhas.Modelos
{
    public class Musica
    {
        public const string ARTISTA_DESCONHECIDO = "Desconhecido";
        private int? anoLancamento;
        private string artista;

        public Musica(string nome, int anoLancamento, string artista)
        {
            Nome = nome;
            AnoLancamento = anoLancamento;
            Artista = artista;
        }

        public string Nome { get; set; }
        public int Id { get; set; }
        public string Artista 
        { 
            get => artista;
            set 
            { 
                artista = value;
                if (string.IsNullOrWhiteSpace(Artista))
                    Artista = ARTISTA_DESCONHECIDO;
            } 
        }
        public int? AnoLancamento 
        { 
            get => anoLancamento;
            set
            {
                anoLancamento = value;
                if(anoLancamento == 0)
                    AnoLancamento = null;
            }
        }

        public void ExibirFichaTecnica()
        {
            Console.WriteLine($"Nome: {Nome}");

        }

        public override string ToString()
        {
            return @$"Id: {Id} Nome: {Nome}";
        }
    }
}
