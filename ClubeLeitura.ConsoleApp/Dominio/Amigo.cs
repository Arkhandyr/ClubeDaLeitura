using System.Collections.Generic;

namespace ClubeLeitura.ConsoleApp.Dominio
{
    public class Amigo : EntidadeBase
    {
        public string nome;
        public string nomeResponsavel;
        public string telefone;
        public string localidade;
        private List<Emprestimo> historicoEmprestimos = new List<Emprestimo>();
        public Amigo()
        {
        }
        public Amigo(string nome, string nomeResponsavel, string telefone, string localidade)
        {
            this.nome = nome;
            this.nomeResponsavel = nomeResponsavel;
            this.telefone = telefone;
            this.localidade = localidade;
        }

        public override string Validar()
        {
            return "REGISTRO_VALIDO";
        }
        public override bool Equals(object obj)
        {
            return ((Amigo)obj).id == id;
        }
        public void RegistrarEmprestimo(Emprestimo emprestimo)
        {
            historicoEmprestimos.Add(emprestimo);
        }
        public bool TemEmprestimoEmAberto()
        {
            return historicoEmprestimos.Exists(x => x.estaAberto); ;
        }
    }
}