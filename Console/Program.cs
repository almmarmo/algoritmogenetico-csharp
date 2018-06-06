using System;
using System.Collections.Generic;
using System.Linq;
using AlgoritmoGenetico.Library;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var listaProdutos = CriarProdutos();

            var espacos = listaProdutos.Select(x => x.Espaco).ToList();
            var valores = listaProdutos.Select(x => x.Valor).ToList();
            double limiteEspacos = 3;
            int tamanhoPopulacao = 20;

            var ag = new AlgoritmoGenetico.Library.AlgoritmoGenetico(tamanhoPopulacao);
            ag.InicializarPopulacao(espacos, valores, limiteEspacos);
            ag.Populacao.ForEach(x => x.Avaliacao());

            for(int i = 0; i < ag.Populacao.Count; i++)
            {
                Console.WriteLine($"*** Indivíduo {i} ***");
                Console.WriteLine($"Espaços = {String.Join(',', ag.Populacao[i].EspacoUsado)}");
                Console.WriteLine($"Valores = {String.Join(',', ag.Populacao[i].NotaAvaliacao)}");
                Console.WriteLine($"Cromossomo = {String.Join(',', ag.Populacao[i].Cromossomo)}");
            }

            //var ind1 = new Individuo(espacos, valores, limiteEspacos);
            //var ind2 = new Individuo(espacos, valores, limiteEspacos);

            //Console.WriteLine($"Cromossomos do Individuo 1: {String.Join(',', ind1.Cromossomo)}");
            //Console.WriteLine($"Cromossomos do Individuo 2: {String.Join(',', ind2.Cromossomo)}");

            //Console.WriteLine("\nRealizando crossover...\n");

            //var crossovers = ind1.Crossover(ind2);

            //foreach (var item in crossovers)
            //    Console.WriteLine($"Novo Individuo: {String.Join(',', item.Cromossomo)}");

            Console.ReadKey();
        }

        static List<Produto> CriarProdutos()
        {
            var listProdutos = new List<Produto>();
            listProdutos.Add(new Produto("Geladeira Dako", 0.751, 999.90));
            listProdutos.Add(new Produto("Iphone 6", 0.0000899, 2911.12));
            listProdutos.Add(new Produto("TV 55' ", 0.400, 4346.99));
            listProdutos.Add(new Produto("TV 50' ", 0.290, 3999.90));
            listProdutos.Add(new Produto("TV 42' ", 0.200, 2999.00));
            listProdutos.Add(new Produto("Notebook Dell", 0.00350, 2499.90));
            listProdutos.Add(new Produto("Ventilador Panasonic", 0.496, 199.90));
            listProdutos.Add(new Produto("Microondas Electrolux", 0.0424, 308.66));
            listProdutos.Add(new Produto("Microondas LG", 0.0544, 429.90));
            listProdutos.Add(new Produto("Microondas Panasonic", 0.0319, 299.29));
            listProdutos.Add(new Produto("Geladeira Brastemp", 0.635, 849.00));
            listProdutos.Add(new Produto("Geladeira Consul", 0.870, 1199.89));
            listProdutos.Add(new Produto("Notebook Lenovo", 0.498, 1999.90));
            listProdutos.Add(new Produto("Notebook Asus", 0.527, 3999.00));

            return listProdutos;
        }
    }
}
