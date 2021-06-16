using ClubeLeitura.ConsoleApp.Dominio;
using System.Collections.Generic;

namespace ClubeLeitura.ConsoleApp.Controladores
{
    public class ControladorBase<T> where T : EntidadeBase
    {
        private int ultimoId;
        public List<T> registros { get; private set; }

        public ControladorBase()
        {
            registros = new List<T>();
        }

        public string InserirNovoRegistro(T registro)
        {
            string resultadoValidacao = registro.Validar();

            if (resultadoValidacao == "REGISTRO_VALIDO")
            {
                registro.id = ++ultimoId;
                registros.Add(registro);
            }
            return resultadoValidacao;
        }
        public string EditarRegistro(int id, T registro)
        {
            string resultado = registro.Validar();
            if (resultado == "REGISTRO_VALIDO")
            {
                int indice = registros.IndexOf(SelecionarRegistroPorId(id));
                registro.id = id;
                registros[indice] = registro;
            }

            return resultado;
        }
        public void ExcluirRegistro(int id)
        {
            registros.Remove(SelecionarRegistroPorId(id));
        }
        public bool ExisteRegistroComEsteId(int id)
        {
            return SelecionarRegistroPorId(id) != null;
        }
        public T SelecionarRegistroPorId(int id)
        {
            return registros.Find(x => x.id == id);
        }
    }
}