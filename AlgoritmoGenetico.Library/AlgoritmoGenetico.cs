using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlgoritmoGenetico.Library
{
    public class AlgoritmoGenetico
    {
        public AlgoritmoGenetico(int tamanhoPopulacao)
        {
            TamanhoPopulacao = tamanhoPopulacao;
            Populacao = new List<Individuo>();
            ListaSolucoes = new List<double>();
        }

        public int TamanhoPopulacao { get; set; }
        public List<Individuo> Populacao { get; set; }
        public int Geracao { get; set; }
        public Individuo MelhorSolucao { get; set; }
        public List<double> ListaSolucoes { get; set; }

        public void InicializarPopulacao(List<double> espacos, List<double> valores, double limiteEspacos)
        {
            for(int i = 0; i < TamanhoPopulacao; i++)
                Populacao.Add(new Individuo(espacos, valores, limiteEspacos));

            Populacao.ForEach(x => x.Avaliacao());
            this.OrdenarPopulacao();
            MelhorSolucao = Populacao.First();
            ListaSolucoes.Add(MelhorSolucao.NotaAvaliacao);
        }

        public void OrdenarPopulacao()
        {
            Populacao = Populacao.OrderByDescending(x => x.NotaAvaliacao).ToList();
            MelhorSolucao = Populacao[0];
        }

        public void MelhorIndividuo(Individuo individuo)
        {
            if (individuo.NotaAvaliacao > this.MelhorSolucao.NotaAvaliacao)
                this.MelhorSolucao = individuo;
        }

        public double SomaAvaliacoes()
        {
            return this.Populacao.Sum(x => x.NotaAvaliacao);
        }

        public int SelecionaIndicePai(double somaAvaliacao)
        {
            Random random = new Random();
            var pai = -1;
            var valorSorteado = random.NextDouble() * somaAvaliacao;
            double soma = 0;
            var i = 0;

            while(i < this.Populacao.Count && soma < valorSorteado)
            {
                soma += this.Populacao[i].NotaAvaliacao;
                pai += 1;
                i += 1;
            }

            return pai;
        }

        public string VisualizaGeracao()
        {
            var melhor = this.MelhorSolucao;

            return $"G: {melhor.Geracao} -> Valor: {melhor.NotaAvaliacao} Espaço: {melhor.EspacoUsado} Cromossomo: [{String.Join(",", melhor.Cromossomo)}]";
        }

        public List<string> Resolver(double taxaMutacao, int numeroGeracoes, List<double> espacos, List<double> valores, double limiteEspacos)
        {
            InicializarPopulacao(espacos, valores, limiteEspacos);

            for(int geracao = 0; geracao < numeroGeracoes; geracao++)
            {
                var somaAvaliacao = SomaAvaliacoes();
                List<Individuo> novaPopulacao = new List<Individuo>();

                for(int i = 0; i < TamanhoPopulacao; i+=2)
                {
                    var pai1 = SelecionaIndicePai(somaAvaliacao);
                    var pai2 = SelecionaIndicePai(somaAvaliacao);

                    var filhos = Populacao[pai1].Crossover(Populacao[pai2]);

                    novaPopulacao.Add(filhos[0].Mutacao(taxaMutacao));
                    novaPopulacao.Add(filhos[1].Mutacao(taxaMutacao));
                }

                Populacao = novaPopulacao;

                Populacao.ForEach(x => x.Avaliacao());
                OrdenarPopulacao();
                MelhorSolucao = Populacao.First();
                ListaSolucoes.Add(MelhorSolucao.NotaAvaliacao);
                MelhorIndividuo(MelhorSolucao);
            }

            return MelhorSolucao.Cromossomo;
        }
    }
}
