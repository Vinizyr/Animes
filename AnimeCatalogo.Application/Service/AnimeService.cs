using AnimeCatalogo.Application.DTOs;
using AnimeCatalogo.Application.DTOs.Anime;
using AnimeCatalogo.Application.IService;
using AnimeCatalogo.Domain.Entities;
using AnimeCatalogo.Infrastructure.IRepository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AnimeCatalogo.Application.Service
{
    public class AnimeService : IAnimeService
    {
        private readonly IAnimeRepository _animeRepository;
        

        public AnimeService(IAnimeRepository animeRepository)
        {
            _animeRepository = animeRepository;
        }
        public async Task<GenericResponse> Atualizar(Guid id, CreateAnimeDto dto)
        {
            var anime = await _animeRepository.ObterPorId(id);

            if (anime == null)
            {
                return new GenericResponse
                {
                    Sucesso = false,
                    Mensagem = "Anime não encontrado."
                };
            }

            anime.Atualizar(dto.Nome, dto.Diretor, dto.Resumo);
            await _animeRepository.Atualizar(anime);

            return new GenericResponse
            {
                Sucesso = true,
                Mensagem = "Anime atualizado com sucesso."
            };
        }

        public async Task<GenericResponse> Criar(CreateAnimeDto dto)
        {
            var anime = new Anime(dto.Nome, dto.Diretor, dto.Resumo);
            await _animeRepository.Criar(anime);

            return new GenericResponse
            {
                Sucesso = true,
                Mensagem = "Anime criado com sucesso."
            };
        }

        public async Task<bool> Excluir(Guid id)
        {
            var excluido = await _animeRepository.Excluir(id);
            if (excluido)
            {
                return true;
            }
            return false;
        }
    }
}
