using AnimeCatalogo.Application.DTOs;
using AnimeCatalogo.Application.DTOs.Anime;
using AnimeCatalogo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeCatalogo.Application.IService
{
    public interface IAnimeService
    {
        Task<GenericResponse> Criar(CreateAnimeDto dto);
        Task<GenericResponse> Atualizar(Guid id, CreateAnimeDto dto);
        Task<bool> Excluir(Guid id);
    }
}
