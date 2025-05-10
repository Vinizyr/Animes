using AnimeCatalogo.Domain.Entities;
using AnimeCatalogo.Infrastructure.IRepository;
using AnimeCatalogo.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeCatalogo.Infrastructure.Repository
{
    public class AnimeRepository : IAnimeRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<AnimeRepository> _logger;
        public AnimeRepository(AppDbContext context, ILogger<AnimeRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<Anime> ObterPorId(Guid id)
        {
            var anime = await _context.Animes
                .Where(a => a.Id == id && a.Ativo)
                .FirstOrDefaultAsync();

            _logger.LogInformation("Busca por anime com o Id {id}", id);

            if (anime == null)
            {
                return null!;
            }
            return anime;
        }
        public async Task<IEnumerable<Anime>> BuscarPorFiltros(string? nome, string? diretor, string? resumo, int pageNumber, int pageSize)
        {
            var query = _context.Animes.AsQueryable();

            if (!string.IsNullOrWhiteSpace(nome))
                query = query.Where(a => a.Nome.Contains(nome));

            if (!string.IsNullOrWhiteSpace(diretor))
                query = query.Where(a => a.Diretor.Contains(diretor));

            if (!string.IsNullOrWhiteSpace(resumo))
                query = query.Where(a => a.Resumo.Contains(resumo));

            query = query.Where(a => a.Ativo);
            query = query.OrderBy(a => a.Nome);

            _logger.LogInformation("Busca por animes com filtros: Nome={Nome}, Diretor={Diretor}, Resumo={Resumo}", nome, diretor, resumo);

            return await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task Criar(Anime anime)
        {
            await _context.Animes.AddAsync(anime);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Anime criado com Id: {id}", anime.Id);
        }
        public async Task Atualizar(Anime anime)
        {
            _context.Animes.Update(anime);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Anime com id {id} foi atualizado.", anime.Id);
        }
        public async Task<bool> Excluir(Guid id)
        {
            var anime = await ObterPorId(id);
            if (anime != null)
            {
                anime.Excluir(); //Exclusão lógica
                await Atualizar(anime);
                _logger.LogInformation("Anime excluído logicamente com o Id {id}", id);
                return true;
            }
            else
            {
                _logger.LogWarning("Anime não encontrado para exclusão com o Id {id}", id);
                return false;
            }
        }
    }
    
}
