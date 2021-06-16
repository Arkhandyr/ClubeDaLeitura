using System;

namespace ClubeLeitura.ConsoleApp.Dominio
{
    public class Emprestimo : EntidadeBase
    {
        public Amigo amiguinho;
        public Revista revistinha;
        public DateTime dataEmprestimo;
        public DateTime dataDevolucao;
        public bool estaAberto;

        public Emprestimo(Amigo amigo, Revista revista, DateTime dtEmprestimo)
        {
            amiguinho = amigo;
            revistinha = revista;
            dataEmprestimo = dtEmprestimo;
            estaAberto = true;
        }
        public override string Validar()
        {
            string resultadoValidacao = "";

            if (amiguinho.TemEmprestimoEmAberto())
                resultadoValidacao += "O amiguinho está com empréstimo em aberto \n";

            if (revistinha.TemEmprestimoEmAberto())
                resultadoValidacao += "A Revistinha está emprestada \n";

            if (string.IsNullOrEmpty(resultadoValidacao))
                resultadoValidacao = "REGISTRO_VALIDO";

            return resultadoValidacao;
        }
        public void Fechar(DateTime dataDevolucao)
        {
            estaAberto = false;
            this.dataDevolucao = dataDevolucao;
        }
    }
}
