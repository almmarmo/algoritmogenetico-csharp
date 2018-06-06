﻿using System;
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
        }

        public int TamanhoPopulacao { get; set; }
        public List<Individuo> Populacao { get; set; }
        public int Geracao { get; set; }
        public Individuo MelhorSolucao { get; set; }

        public void InicializarPopulacao(List<double> espacos, List<double> valores, double limiteEspacos)
        {
            for(int i = 0; i < TamanhoPopulacao; i++)
                Populacao.Add(new Individuo(espacos, valores, limiteEspacos));

            MelhorSolucao = Populacao[0];
        }

        public void OrdenarPopulacao()
        {
            Populacao = Populacao.OrderByDescending(x => x.NotaAvaliacao).ToList();
            MelhorSolucao = Populacao[0];
        }
    }
}
