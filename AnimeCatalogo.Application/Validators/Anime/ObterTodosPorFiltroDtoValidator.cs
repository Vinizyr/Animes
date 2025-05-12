using AnimeCatalogo.Application.DTOs.Anime;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeCatalogo.Application.Validators.Anime
{
    public class ObterTodosPorFiltroDtoValidator : AbstractValidator<ObterTodosPorFiltroDto>
    {
        public ObterTodosPorFiltroDtoValidator()
        {
            RuleFor(x => x.Nome)
                .MaximumLength(100)
                .WithMessage("O nome do anime deve ter no máximo 100 caracteres.");
            RuleFor(x => x.Diretor)
                .MaximumLength(50)
                .WithMessage("O diretor do anime deve ter no máximo 50 caracteres.");
            RuleFor(x => x.Resumo)
                .MaximumLength(500)
                .WithMessage("O resumo do anime deve ter no máximo 500 caracteres.");
            RuleFor(x => x.Pagina)
                .GreaterThan(0)
                .WithMessage("A página deve ser maior que zero.");
            RuleFor(x => x.ItensPorPagina)
                .GreaterThan(0)
                .WithMessage("Os itens por página devem ser maior que zero.");
        }
    }
}
