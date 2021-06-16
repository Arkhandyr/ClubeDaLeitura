using System.Collections.Generic;

namespace ClubeLeitura.ConsoleApp.Dominio
{
    public class Revista : EntidadeBase
    {
        public Caixa caixa;
        public string colecao;
        public int numeroEdicao;
        private List<Emprestimo> historicoEmprestimos = new List<Emprestimo>();

        public Revista(string colecao, int numeroEdicao, Caixa caixa)
        {
            this.colecao = colecao;
            this.numeroEdicao = numeroEdicao;
            this.caixa = caixa;
        }
        public override string Validar()
        {
            return "REGISTRO_VALIDO";
        }
        public void RegistrarEmprestimo(Emprestimo emprestimo)
        {
            historicoEmprestimos.Add(emprestimo);
        }
        public bool TemEmprestimoEmAberto()
        {
            return historicoEmprestimos.Exists(x => x.estaAberto);
        }
    }
}
