using AnimeCatalogo.Application.DTOs.Anime;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeCatalogo.Application.Validators.Anime
{
    public class CreateAnimeDtoValidator : AbstractValidator<CreateAnimeDto>
    {
        public CreateAnimeDtoValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage("O nome do anime é obrigatório.")
                .Length(3, 100)
                .WithMessage("O nome do anime deve ter entre 3 e 100 caracteres.");
            RuleFor(x => x.Diretor)
                .NotEmpty()
                .WithMessage("O diretor do anime é obrigatório.")
                .Length(3, 100)
                .WithMessage("O diretor do anime deve ter entre 3 e 50 caracteres.");
            RuleFor(x => x.Resumo)
                .NotEmpty()
                .WithMessage("O resumo do anime é obrigatório.")
                .Length(10, 500)
                .WithMessage("O resumo do anime deve ter entre 10 e 500 caracteres.");
        }
    }
}
