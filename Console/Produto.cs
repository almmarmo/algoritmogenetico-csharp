using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp
{
    public class Produto
    {
        public Produto(string nome, double espaco, double valor)
        {
            Nome = nome;
            Espaco = espaco;
            Valor = valor;
        }
        public string Nome { get; set; }
        public double Espaco { get; set; }
        public double Valor { get; set; }

    }
}
