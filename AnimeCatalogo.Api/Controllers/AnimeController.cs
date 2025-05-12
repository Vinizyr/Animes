using AnimeCatalogo.Application.DTOs;
using AnimeCatalogo.Application.DTOs.Anime;
using AnimeCatalogo.Application.IService;
using AnimeCatalogo.Application.Validators.Anime;
using AnimeCatalogo.Domain.Entities;
using AnimeCatalogo.Infrastructure.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace AnimeCatalogo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class AnimeController : ControllerBase
    {
        private readonly IAnimeService _animeService;
        private readonly IAnimeRepository _animeRepository;

        public AnimeController(IAnimeService animeService, IAnimeRepository animeRepository)
        {
            _animeService = animeService;
            _animeRepository = animeRepository;
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(Anime), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> ObterPorId(Guid id)
        {
            var anime = await _animeRepository.ObterPorId(id);
            if (anime == null) return NotFound("Anime não encontrado.");
            return Ok(anime);
        }

        [HttpGet("")]
        [ProducesResponseType(typeof(GenericResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<object>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> ObterTodosFilrados([FromQuery] ObterTodosPorFiltroDto dto)
        {
            var ObterTodosDtoValidator = new ObterTodosPorFiltroDtoValidator();
            var validationResult = await ObterTodosDtoValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => new
                {
                    propertyName = e.PropertyName,
                    errorMessage = e.ErrorMessage
                });
                return BadRequest(errors);
            }

            var result = await _animeRepository.BuscarPorFiltros(dto.Nome, dto.Diretor, dto.Resumo,
                dto.Pagina, dto.ItensPorPagina);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(GenericResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<object>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Criar([FromBody] CreateAnimeDto dto)
        {
            CreateAnimeDtoValidator validator = new CreateAnimeDtoValidator();
            var validationResult = await validator.ValidateAsync(dto);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => new
                {
                    propertyName = e.PropertyName,
                    errorMessage = e.ErrorMessage
                });
                return BadRequest(errors);
            }

            var result = await _animeService.Criar(dto);
            return Ok(result);
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(GenericResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<object>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Atualizar(Guid id, [FromBody] CreateAnimeDto dto)
        {
            CreateAnimeDtoValidator validator = new CreateAnimeDtoValidator();
            var validationResult = await validator.ValidateAsync(dto);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => new
                {
                    propertyName = e.PropertyName,
                    errorMessage = e.ErrorMessage
                });
                return BadRequest(errors);
            }
            var result = await _animeService.Atualizar(id, dto);
            return Ok(result);
            
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Deletar(Guid id)
        {
            var result = await _animeRepository.Excluir(id);
            return result ? NoContent() : NotFound("Anime não encontrado.");
        }
    }
}
