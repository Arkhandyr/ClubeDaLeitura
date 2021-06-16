using ClubeLeitura.ConsoleApp.Controladores;
using ClubeLeitura.ConsoleApp.Dominio;
using System;
using System.Collections.Generic;

namespace ClubeLeitura.ConsoleApp.Telas
{
    public class TelaRevista : TelaCadastro<Revista>
    {
        private readonly ControladorBase<Caixa> controladorCaixa;
        private readonly TelaCaixa telaCaixa;

        public TelaRevista(ControladorBase<Revista> controlador, ControladorBase<Caixa> ctrlCaixa) : base(controlador, "Cadastro de Revistas")
        {
            controladorCaixa = ctrlCaixa;
            telaCaixa = new TelaCaixa(controladorCaixa);
        }
        public override void ConfigurarTabela(List<Revista> registros)
        {
            string configuracaColunasTabela = "{0,-10} | {1,-25} | {2,-25}";
            MontarCabecalhoTabela(configuracaColunasTabela, "ID", "Coleção", "Caixa");
            foreach (Revista revista in registros)
                Console.WriteLine(configuracaColunasTabela, revista.id, revista.colecao, revista.caixa.cor);
        }
        public override Revista ObterRegistro(TipoAcao tipo)
        {
            telaCaixa.VisualizarRegistros();

            Console.Write("\nDigite o ID da caixa: ");
            int idCaixa = Convert.ToInt32(Console.ReadLine());

            bool numeroEncontrado = controladorCaixa.ExisteRegistroComEsteId(idCaixa);
            if (!numeroEncontrado)
            {
                ApresentarMensagem("Nenhuma revista foi encontrada com este ID: " + idCaixa, TipoMensagem.Erro);

                ConfigurarTela($"{tipo} uma revista...");

                return ObterRegistro(tipo);
            }

            Caixa caixa = controladorCaixa.SelecionarRegistroPorId(idCaixa);

            Console.Write("Digite a coleção da revista: ");
            string colecao = Console.ReadLine();

            Console.Write("Digite o número de edição da revista: ");
            int numeroEdicao = Convert.ToInt32(Console.ReadLine());

            return new Revista(colecao, numeroEdicao, caixa);
        }
    }
}
