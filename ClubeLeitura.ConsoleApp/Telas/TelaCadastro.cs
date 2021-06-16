using ClubeLeitura.ConsoleApp.Controladores;
using ClubeLeitura.ConsoleApp.Dominio;
using System;
using System.Collections.Generic;

namespace ClubeLeitura.ConsoleApp.Telas
{
    public abstract class TelaCadastro<T> : TelaBase, ICadastravel where T : EntidadeBase
    {
        private readonly ControladorBase<T> controlador;
        public TelaCadastro(ControladorBase<T> controlador, string tit) : base(tit)
        {
            this.controlador = controlador;
        }

        public override void Menu()
        {
            Console.Clear();
            ConfigurarTela("Escolha uma opção: ");
            Console.WriteLine("1. INSERIR um registro");
            Console.WriteLine("2. VISUALIZAR registros");
            Console.WriteLine("3. EDITAR um registro");
            Console.WriteLine("4. EXCLUIR um registro");

            Console.WriteLine("Digite S para Voltar\n");
            Console.Write("Opção: ");

            switch (Console.ReadLine())
            {
                case "1": InserirNovoRegistro(); break;
                case "2": { if (VisualizarRegistros()) Console.ReadKey(); break; }
                case "3": EditarRegistro(); break;
                case "4": ExcluirRegistro(); break;
            }
        }
        public abstract T ObterRegistro(TipoAcao inserindo);
        public abstract void ConfigurarTabela(List<T> registros);
        public bool VisualizarRegistros()
        {
            ConfigurarTela("Visualizando registros...");
            List<T> registros = controlador.registros;
            if (registros.Count == 0)
            {
                ApresentarMensagem("Nenhum registro cadastrado!", TipoMensagem.Atencao);
                return false;
            }
            ConfigurarTabela(registros);
            return true;
        }
        public void InserirNovoRegistro()
        {
            ConfigurarTela("Inserindo um novo registro...");

            T registro = ObterRegistro(TipoAcao.Inserindo);

            string resultadoValidacao = controlador.InserirNovoRegistro(registro);

            if (resultadoValidacao == "REGISTRO_VALIDO")
                ApresentarMensagem("Registro inserido com sucesso!", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem(resultadoValidacao, TipoMensagem.Erro);
                InserirNovoRegistro();
            }
        }
        public void EditarRegistro()
        {
            ConfigurarTela("Editando um registro...");

            if (!VisualizarRegistros())
                return;

            Console.Write("\nDigite o ID do registro que deseja editar: ");
            int id = Convert.ToInt32(Console.ReadLine());

            bool numeroEncontrado = controlador.ExisteRegistroComEsteId(id);
            if (!numeroEncontrado)
            {
                ApresentarMensagem("Nenhum registro foi encontrado com este ID: " + id, TipoMensagem.Erro);
                EditarRegistro();
                return;
            }
            controlador.ExcluirRegistro(id);
            InserirNovoRegistro();
        }
        public void ExcluirRegistro()
        {
            ConfigurarTela("Excluindo um registro...");

            if (!VisualizarRegistros())
                return;

            Console.Write("\nDigite o ID do registro que deseja excluir: ");
            int id = Convert.ToInt32(Console.ReadLine());

            bool numeroEncontrado = controlador.ExisteRegistroComEsteId(id);
            if (!numeroEncontrado)
            {
                ApresentarMensagem("Nenhum registro foi encontrado com este ID: " + id, TipoMensagem.Erro);
                ExcluirRegistro();
            }
            controlador.ExcluirRegistro(id);
            ApresentarMensagem("Registro excluído com sucesso!", TipoMensagem.Sucesso);
        }
    }
}