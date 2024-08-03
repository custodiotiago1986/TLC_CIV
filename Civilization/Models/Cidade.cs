using System.Collections.Generic;
using System.Windows.Forms;

namespace Civilization.Models
{
    public class Cidade
    {
        public string Nome { get; set; }
        public int Producao { get; set; }
        public int Ouro { get; set; }
        public int Comida { get; set; }
        public int Lealdade { get; set; }
        public int Fe { get; set; }
        public List<Unidade> Unidades { get; set; }
        public List<string> Construcoes { get; set; }
        public List<ItemProducao> FilaProducao { get; set; }
        public Button Botao { get; set; }

        public Cidade(string nome)
        {
            Nome = nome;
            Producao = 10;
            Ouro = 10;
            Comida = 10;
            Lealdade = 10;
            Fe = 10;
            Unidades = new List<Unidade>();
            Construcoes = new List<string>();
            FilaProducao = new List<ItemProducao>();
        }
    }

    public class ItemProducao
    {
        public string Nome { get; set; }
        public int TurnosRestantes { get; set; }
        public int CustoProducao { get; set; }
        public ItemType Tipo { get; set; }

        public ItemProducao(string nome, int turnosRestantes, int custoProducao, ItemType tipo)
        {
            Nome = nome;
            TurnosRestantes = turnosRestantes;
            CustoProducao = custoProducao;
            Tipo = tipo;
        }
    }

    public enum ItemType
    {
        Unit,
        Building
    }
}
