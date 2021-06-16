using ClubeLeitura.ConsoleApp.Controladores;
using ClubeLeitura.ConsoleApp.Dominio;
using System;

namespace ClubeLeitura.ConsoleApp.Telas
{
    public class TelaPrincipal : TelaBase
    {
        private readonly ControladorBase<Amigo> controladorAmigo = new ControladorBase<Amigo>();
        private readonly ControladorBase<Caixa> controladorCaixa = new ControladorBase<Caixa>();
        private readonly ControladorBase<Revista> controladorRevista = new ControladorBase<Revista>();
        private readonly ControladorEmprestimo controladorEmprestimo = new ControladorEmprestimo();
        public TelaPrincipal() : base("Tela Principal")
        {
            PopularAplicacao();
            while (true)
            {
                TelaBase tb = ObterTela();
                if (tb == null)
                {
                    ApresentarMensagem("Opção inválida", TipoMensagem.Erro);
                    continue;
                }
                tb.Menu();
            }
        }
        private void PopularAplicacao()
        {
            Caixa c = new Caixa("Azul", "xua-654");
            controladorCaixa.InserirNovoRegistro(c);

            Amigo a = new Amigo("Helena", "Alexandre", "321", "Colégio");
            controladorAmigo.InserirNovoRegistro(a);

            Revista r = new Revista("Superman", 10, c);
            controladorRevista.InserirNovoRegistro(r);

            Emprestimo e = new Emprestimo(a, r, DateTime.Today);
            controladorEmprestimo.InserirNovoRegistro(e);
            r.RegistrarEmprestimo(e);
            a.RegistrarEmprestimo(e);
        }
        public TelaBase ObterTela()
        {
            Menu();
            switch (Console.ReadLine())
            {
                case "1": return new TelaAmigo(controladorAmigo);
                case "2": return new TelaCaixa(controladorCaixa);
                case "3": return new TelaRevista(controladorRevista, controladorCaixa);
                case "4": return new TelaEmprestimo(controladorEmprestimo, controladorAmigo, controladorRevista, controladorCaixa);
                default: return null;
            }
        }
        public override void Menu()
        {
            ConfigurarTela("Escolha uma opção: ");
            Console.WriteLine("1. Cadastro de Amiguinhos");
            Console.WriteLine("2. Cadastro de Caixas");
            Console.WriteLine("3. Cadastro de Revistas");
            Console.WriteLine("4. Controle de Empréstimos\n");
            Console.Write("Opção: ");
        }
    }
}
