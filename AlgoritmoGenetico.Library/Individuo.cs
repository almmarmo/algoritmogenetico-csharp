using System;
using System.Collections.Generic;

namespace AlgoritmoGenetico.Library
{
    public class Individuo
    {
        private Random random = new Random();

        public Individuo(List<double> espacos, List<double> valores, double limiteEspacos, int geracao = 0)
        {
            Espacos = espacos;
            Valores = valores;
            LimiteEspacos = limiteEspacos;
            Cromossomo = new List<string>();

            InicializarCromossomo();
        }

        public List<double> Espacos { get; set; }
        public List<double> Valores { get; set; }
        public double LimiteEspacos { get; set; }
        public int Geracao { get; set; }
        public double NotaAvaliacao { get; set; }
        public double EspacoUsado { get; set; }

        public List<string> Cromossomo { get; private set; }

        public void Avaliacao()
        {
            double nota = 0;
            double somaEspacos = 0;

            for(int i = 0; i < Cromossomo.Count; i++)
            {
                if (Cromossomo[i] == "1")
                {
                    nota += Valores[i];
                    somaEspacos += Espacos[i];
                }
            }

            if (somaEspacos > LimiteEspacos)
                nota = 1;

            NotaAvaliacao = nota;
            EspacoUsado = somaEspacos;
        }

        public List<Individuo> Crossover(Individuo outro)
        {
            var corte = Convert.ToInt32(Math.Round(random.NextDouble() * Cromossomo.Count));

            var filho1 = new List<string>();
            filho1.AddRange(outro.Cromossomo.GetRange(0, corte));
            filho1.AddRange(Cromossomo.GetRange(corte, Cromossomo.Count-corte));

            var filho2 = new List<string>();
            filho2.AddRange(Cromossomo.GetRange(0, corte));
            filho2.AddRange(outro.Cromossomo.GetRange(corte, outro.Cromossomo.Count - corte));

            var filhos = new List<Individuo>();
            filhos.Add(new Individuo(this.Espacos, this.Valores, this.LimiteEspacos, this.Geracao + 1));
            filhos.Add(new Individuo(this.Espacos, this.Valores, this.LimiteEspacos, this.Geracao + 1));
            filhos[0].Cromossomo = filho1;
            filhos[1].Cromossomo = filho2;

            return filhos;
        }

        public Individuo Mutacao(double taxaMutacao)
        {
            for(int i=0; i < Cromossomo.Count; i++)
            {
                if(random.NextDouble() < taxaMutacao)
                {
                    if (Cromossomo[i] == "1")
                        Cromossomo[i] = "0";
                    else
                        Cromossomo[i] = "1";
                }
            }

            return this;
        }

        private void InicializarCromossomo()
        {
            for (int i = 0; i < Espacos.Count; i++)
            {
                if (random.NextDouble() < 0.5)
                    Cromossomo.Add("0");
                else
                    Cromossomo.Add("1");
            }
        }
    }
}
