using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeCatalogo.Application.DTOs.Anime
{
    public class CreateAnimeDto
    {
        public string Nome { get; set; } = null!;
        public string Diretor { get; set; } = null!;
        public string Resumo { get; set; } = null!;
    }
}
