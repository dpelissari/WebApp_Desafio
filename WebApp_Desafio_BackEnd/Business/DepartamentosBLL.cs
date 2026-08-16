using System.Collections.Generic;
using WebApp_Desafio_BackEnd.DataAccess;
using WebApp_Desafio_BackEnd.Models;

namespace WebApp_Desafio_BackEnd.Business
{
    public class DepartamentosBLL
    {
        private readonly DepartamentosDAL _dal = new DepartamentosDAL();

        public IEnumerable<Departamento> Listar()
        {
            return _dal.Listar();
        }

        public Departamento Obter(int idDepartamento)
        {
            return _dal.Obter(idDepartamento);
        }

        public bool Gravar(int idDepartamento, string descricao)
        {
            return _dal.Gravar(idDepartamento, descricao);
        }

        public bool Excluir(int idDepartamento)
        {
            return _dal.Excluir(idDepartamento);
        }
    }
}
