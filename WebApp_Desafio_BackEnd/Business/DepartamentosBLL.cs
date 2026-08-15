using System.Collections.Generic;
using WebApp_Desafio_BackEnd.DataAccess;
using WebApp_Desafio_BackEnd.Models;

namespace WebApp_Desafio_BackEnd.Business
{
    public class DepartamentosBLL
    {
        private DepartamentosDAL dal = new DepartamentosDAL();

        public IEnumerable<Departamento> ListarDepartamentos()
        {
            return dal.ListarDepartamentos();
        }

        public Departamento ObterDepartamento(int idDepartamento)
        {
            return dal.ObterDepartamento(idDepartamento);
        }

        public bool GravarDepartamento(int id, string descricao)
        {
            return dal.GravarDepartamento(id, descricao);
        }

        public bool ExcluirDepartamento(int idDepartamento)
        {
            return dal.ExcluirDepartamento(idDepartamento);
        }
    }
}
