using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeCatalogo.Application.DTOs.Anime
{
    public class ObterTodosDto
    {
        public int Pagina { get; set; }
        public int ItensPorPagina { get; set; } = 10;
    }
}
