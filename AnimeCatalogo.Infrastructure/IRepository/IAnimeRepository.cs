using AnimeCatalogo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeCatalogo.Infrastructure.IRepository
{
    public interface IAnimeRepository
    {
        Task<Anime> ObterPorId(Guid id);
        Task<IEnumerable<Anime>> BuscarPorFiltros(string? nome, string? diretor, string? resumo, int pageNumber, int pageSize);
        Task Criar(Anime anime);
        Task Atualizar(Anime anime);
        Task<bool> Excluir(Guid id);
    }
}
