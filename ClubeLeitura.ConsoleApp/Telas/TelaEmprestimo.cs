using ClubeLeitura.ConsoleApp.Controladores;
using ClubeLeitura.ConsoleApp.Dominio;
using System;
using System.Collections.Generic;

namespace ClubeLeitura.ConsoleApp.Telas
{
    public class TelaEmprestimo : TelaBase
    {
        private readonly TelaRevista telaRevista;
        private readonly TelaAmigo telaAmigo;
        private readonly ControladorBase<Revista> controladorRevista;
        private readonly ControladorBase<Amigo> controladorAmigo;
        private readonly ControladorBase<Caixa> controladorCaixa;
        private readonly ControladorEmprestimo controladorEmprestimo;

        public TelaEmprestimo(ControladorEmprestimo ctrlEmprestimo, ControladorBase<Amigo> ctrlAmigo, ControladorBase<Revista> ctrlRevista, ControladorBase<Caixa> ctrlCaixa) : base("Controle de Empréstimos")
        {
            controladorAmigo = ctrlAmigo;
            controladorRevista = ctrlRevista;
            controladorEmprestimo = ctrlEmprestimo;
            controladorCaixa = ctrlCaixa;

            telaRevista = new TelaRevista(controladorRevista, controladorCaixa);
            telaAmigo = new TelaAmigo(controladorAmigo);
        }

        public override void Menu()
        {
            Console.Clear();
            ConfigurarTela("Escolha uma opção: ");
            Console.WriteLine("1. Registrar empréstimos");
            Console.WriteLine("2. Registrar devoluções");
            Console.WriteLine("3. Visualizar empréstimos em aberto");
            Console.WriteLine("4. Visualizar empréstimos fechados do mês");

            Console.WriteLine("Digite S para Voltar\n");
            Console.Write("Opção:");
            switch (Console.ReadLine())
            {
                case "1": RegistrarEmprestimo(); break;
                case "2": RegistrarDevolucao(); break;
                case "3": { if (VisualizarEmprestimosAbertos()) Console.ReadKey(); break; }
                case "4": { if (VisualizarEmprestimosFechados()) Console.ReadKey(); break; }
                default: break;
            }
        }
        public void RegistrarEmprestimo()
        {
            ConfigurarTela("Registro de Empréstimos...");

            if (!telaRevista.VisualizarRegistros())
                return;

            Console.Write("\nDigite o id da revista: ");
            int idRevista = Convert.ToInt32(Console.ReadLine());

            bool revistaEncontrada = controladorRevista.ExisteRegistroComEsteId(idRevista);
            if (!revistaEncontrada)
            {
                ApresentarMensagem("Nenhuma revista foi encontrado com este id: " + idRevista, TipoMensagem.Erro);
                RegistrarEmprestimo();
                return;
            }

            Console.WriteLine();

            if (!telaAmigo.VisualizarRegistros())
                return;

            Console.Write("\nDigite o id do amiguinho: ");
            int idAmigo = Convert.ToInt32(Console.ReadLine());

            bool amigoEncontrado = controladorAmigo.ExisteRegistroComEsteId(idAmigo);
            if (!amigoEncontrado)
            {
                ApresentarMensagem("Nenhum amiguinho foi encontrado com este id: " + idAmigo, TipoMensagem.Erro);
                RegistrarEmprestimo();
                return;
            }

            Amigo amigo = controladorAmigo.SelecionarRegistroPorId(idAmigo);
            Revista revista = controladorRevista.SelecionarRegistroPorId(idRevista);

            Console.Write("Digite a data do empréstimo: ");
            DateTime dataEmprestimo = Convert.ToDateTime(Console.ReadLine());
            Emprestimo emprestimo = new Emprestimo(amigo, revista, dataEmprestimo);
            string resultadoValidacao = controladorEmprestimo.InserirNovoRegistro(emprestimo);

            if (resultadoValidacao == "REGISTRO_VALIDO")
            {
                ApresentarMensagem("Empréstimo realizado com sucesso", TipoMensagem.Sucesso);
                revista.RegistrarEmprestimo(emprestimo);
                amigo.RegistrarEmprestimo(emprestimo);
            }
            else
                ApresentarMensagem(resultadoValidacao, TipoMensagem.Erro);
        }
        public void RegistrarDevolucao()
        {
            ConfigurarTela("Registro de Devoluções...");

            if (!VisualizarEmprestimosAbertos())
                return;

            Console.WriteLine();

            Console.Write("Digite o id do empréstimo: ");
            int idEmprestimo = Convert.ToInt32(Console.ReadLine());

            bool emprestimoEncontrado = controladorEmprestimo.ExisteRegistroComEsteId(idEmprestimo);
            if (!emprestimoEncontrado)
            {
                ApresentarMensagem("Nenhum empréstimo foi encontrado com este id: " + idEmprestimo, TipoMensagem.Erro);
                RegistrarDevolucao();
                return;
            }

            Console.Write("Digite a data da devolução: ");
            DateTime dataDevolucao = Convert.ToDateTime(Console.ReadLine());

            controladorEmprestimo.RegistrarDevolucao(idEmprestimo, dataDevolucao);
            ApresentarMensagem("Devolução realizada com sucesso", TipoMensagem.Sucesso);

        }
        public bool VisualizarEmprestimosAbertos()
        {
            ConfigurarTela("Visualizando empréstimos em aberto...");

            List<Emprestimo> emprestimos = controladorEmprestimo.SelecionarEmprestimosEmAberto();

            if (emprestimos.Count == 0)
            {
                ApresentarMensagem("Nenhum empréstimo em aberto", TipoMensagem.Atencao);
                return false;
            }

            string configuracaColunasTabela = "{0,-10} | {1,-25} | {2,-25} | {3,-25}";

            Console.WriteLine();

            MontarCabecalhoTabela(configuracaColunasTabela, "Id", "Data de Empréstimo", "Amiguinho", "Revistinha");

            foreach (Emprestimo emprestimo in emprestimos)
                Console.WriteLine(configuracaColunasTabela, emprestimo.id, emprestimo.dataEmprestimo.ToString("dd/MM/yyyy"), emprestimo.amiguinho.nome, emprestimo.revistinha.colecao);

            return true;
        }
        public bool VisualizarEmprestimosFechados()
        {
            ConfigurarTela("Visualizando empréstimos fechados");

            Console.Write("Digite o número do mês: ");
            int numeroMes = Convert.ToInt32(Console.ReadLine());

            List<Emprestimo> emprestimos = controladorEmprestimo.SelecionarEmprestimosFechados(numeroMes);

            if (emprestimos.Count == 0)
            {
                ApresentarMensagem("Nenhum empréstimo fechado neste mês", TipoMensagem.Atencao);
                return false;
            }

            string configuracaColunasTabela = "{0,-10} | {1,-25} | {2,-25} | {3,-25}";

            Console.WriteLine();

            MontarCabecalhoTabela(configuracaColunasTabela, "Id", "Data de Devolução", "Amiguinho", "Revistinha");

            foreach (Emprestimo emprestimo in emprestimos)
                Console.WriteLine(configuracaColunasTabela, emprestimo.id, emprestimo.dataDevolucao.ToString("dd/MM/yyyy"), emprestimo.amiguinho.nome, emprestimo.revistinha.colecao);

            return true;
        }
    }
}
