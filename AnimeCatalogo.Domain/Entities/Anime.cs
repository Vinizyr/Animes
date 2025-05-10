using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeCatalogo.Domain.Entities
{
    public class Anime
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Diretor { get; private set; }
        public string Resumo { get; private set; }
        public bool Ativo { get; private set; }

        protected Anime() { }

        public Anime(string nome, string diretor, string resumo)
        {
            Validar(nome, diretor, resumo);
            Id = Guid.NewGuid();
            Nome = nome;
            Diretor = diretor;
            Resumo = resumo;
            Ativo = true;
        }

        public void Atualizar(string nome, string diretor, string resumo)
        {
            Validar(nome, diretor, resumo);
            Nome = nome;
            Diretor = diretor;
            Resumo = resumo;
        }

        public void Excluir()
        {
            if (!Ativo) throw new InvalidOperationException("Anime já está excluído.");
            Ativo = false;
        }

        public bool EstaAtivo()
        {
            return Ativo;
        }

        private void Validar(string nome, string diretor, string resumo)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Nome não pode ser vazio.");
            if (nome.Length < 3 || nome.Length > 100)
                throw new ArgumentException("O nome do anime deve ter entre 3 e 100 caracteres.");

            if (string.IsNullOrWhiteSpace(diretor))
                throw new ArgumentException("Diretor não pode ser vazio.");
            if (diretor.Length < 3 || diretor.Length > 50)
                throw new ArgumentException("O diretor do anime deve ter entre 3 e 50 caracteres.");

            if (string.IsNullOrWhiteSpace(resumo))
                throw new ArgumentException("Resumo não pode ser vazio.");
            if (resumo.Length < 10 || resumo.Length > 500)
                throw new ArgumentException("O resumo do anime deve ter entre 10 e 500 caracteres.");
        }
    }
}
