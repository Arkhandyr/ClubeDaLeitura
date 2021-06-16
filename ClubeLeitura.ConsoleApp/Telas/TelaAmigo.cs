using ClubeLeitura.ConsoleApp.Controladores;
using ClubeLeitura.ConsoleApp.Dominio;
using System;
using System.Collections.Generic;

namespace ClubeLeitura.ConsoleApp.Telas
{
    public class TelaAmigo : TelaCadastro<Amigo>
    {
        public TelaAmigo(ControladorBase<Amigo> controlador) : base(controlador, "Cadastro de Amigos")
        {
        }

        public override void ConfigurarTabela(List<Amigo> registros)
        {
            string configuracaoColunasTabela = "{0,-10} | {1,-55} | {2,-35}";

            MontarCabecalhoTabela(configuracaoColunasTabela, "ID", "Nome", "Local");

            foreach (Amigo amigo in registros)
                Console.WriteLine(configuracaoColunasTabela, amigo.id, amigo.nome, amigo.localidade);
        }

        public override Amigo ObterRegistro(TipoAcao tipo)
        {
            Console.Write("Digite o nome do amiguinho: ");
            string nome = Console.ReadLine();

            Console.Write("Digite o nome do responsável: ");
            string nomeResponsavel = Console.ReadLine();

            Console.Write("Digite o telefone do amiguinho: ");
            string telefone = Console.ReadLine();

            Console.Write("Digite da onde é o amiguinho: ");
            string localidade = Console.ReadLine();

            return new Amigo(nome, nomeResponsavel, telefone, localidade);
        }
    }
}
