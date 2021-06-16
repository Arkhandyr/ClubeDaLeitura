using ClubeLeitura.ConsoleApp.Controladores;
using ClubeLeitura.ConsoleApp.Dominio;
using System;
using System.Collections.Generic;

namespace ClubeLeitura.ConsoleApp.Telas
{
    public class TelaCaixa : TelaCadastro<Caixa>
    {
        public TelaCaixa(ControladorBase<Caixa> controlador) : base(controlador, "Cadastro de Caixas")
        {
        }
        public override void ConfigurarTabela(List<Caixa> registros)
        {
            string configuracaColunasTabela = "{0,-10} | {1,-55} | {2,-35}";

            MontarCabecalhoTabela(configuracaColunasTabela, "ID", "Etiqueta", "Cor");

            foreach (Caixa caixa in registros)
                Console.WriteLine(configuracaColunasTabela, caixa.id, caixa.etiqueta, caixa.cor);
        }
        public override Caixa ObterRegistro(TipoAcao tipo)
        {
            Console.Write("Digite a etiqueta da caixa: ");
            string etiqueta = Console.ReadLine();

            Console.Write("Digite a cor da caixa: ");
            string cor = Console.ReadLine();

            return new Caixa(cor, etiqueta);
        }
    }
}