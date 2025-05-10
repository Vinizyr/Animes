using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeCatalogo.Application.DTOs
{
    public class GenericResponse
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; } = string.Empty;
    }
}
