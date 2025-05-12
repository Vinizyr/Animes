using AnimeCatalogo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeCatalogo.Domain.Tests
{
    public class AnimeTests
    {
        [Theory]
        [InlineData("Naruto", "Kishimoto", "Um ninja maluco!")]
        [InlineData("One Piece", "Oda", "Piratas em busca do tesouro.")]
        [InlineData("Bleach", "Kubo", "Shinigami e batalhas.")]
        public void CriarAnime(string nome, string diretor, string resumo)
        {
            var anime = new Anime(nome, diretor, resumo);

            Assert.NotEqual(Guid.Empty, anime.Id); 
            Assert.Equal(nome, anime.Nome);
            Assert.Equal(diretor, anime.Diretor);
            Assert.Equal(resumo, anime.Resumo);
            Assert.True(anime.EstaAtivo());
        }

        [Fact]
        public void AtualizarAnime()
        {
            var anime = new Anime("Naruto", "Masashi Kishimoto", "Um ninja em busca de se tornar Hokage.");
            var novoNome = "Naruto Shippuden";
            var novoDiretor = "Outro Diretor";
            var novoResumo = "Continuação de Naruto com mais aventuras.";

            anime.Atualizar(novoNome, novoDiretor, novoResumo);

            Assert.Equal(novoNome, anime.Nome);
            Assert.Equal(novoDiretor, anime.Diretor);
            Assert.Equal(novoResumo, anime.Resumo);

        }

        [Fact]
        public void ExcluirAnime()
        {
            var anime = new Anime("Naruto", "Masashi Kishimoto", "Um ninja em busca de se tornar Hokage.");
            anime.Excluir();
            Assert.False(anime.EstaAtivo());
        }

        [Fact]
        public void ExcluirAnimeJaExcluido()
        {
            var anime = new Anime("Naruto", "Masashi Kishimoto", "Um ninja em busca de se tornar Hokage.");
            anime.Excluir();
            Assert.Throws<InvalidOperationException>(anime.Excluir);
        }

    }
}
