using ClubeLeitura.ConsoleApp.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClubeLeitura.ConsoleApp.Controladores
{
    public class ControladorEmprestimo : ControladorBase<Emprestimo>
    {
        public void RegistrarDevolucao(int idEmprestimo, DateTime dataDevolucao)
        {
            SelecionarRegistroPorId(idEmprestimo).Fechar(dataDevolucao);
        }
        public List<Emprestimo> SelecionarEmprestimosEmAberto()
        {
            return registros.FindAll(x => x.estaAberto);
        }
        public List<Emprestimo> SelecionarEmprestimosFechados(int mes)
        {
            var resultado = registros.Except(SelecionarEmprestimosEmAberto());
            return resultado.Where(x => x.dataDevolucao.Month == mes).ToList();
        }
    }
}