using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Civilization.Models
{
    public class Unidade
    {
        public int Ataque { get; set; }
        public int Defesa { get; set; }
        public int Movimento { get; set; }
        public int TipoTerreno { get; set; } // 1 para Terra, 2 para Mar, 3 para Ar
        public Button Botao { get; set; }

        public Unidade(int ataque, int defesa, int movimento, int tipoTerreno, Button botao)
        {
            Ataque = ataque;
            Defesa = defesa;
            Movimento = movimento;
            TipoTerreno = tipoTerreno;
            Botao = botao;
        }
    }
}
