using System;
using System.Collections.Generic;
using WebApp_Desafio_BackEnd.DataAccess;
using WebApp_Desafio_BackEnd.Models;

namespace WebApp_Desafio_BackEnd.Business
{
    public class ChamadosBLL
    {
        private readonly ChamadosDAL _dal = new ChamadosDAL();

        public IEnumerable<Chamado> Listar()
        {
            return _dal.Listar();
        }

        public Chamado Obter(int idChamado)
        {
            return _dal.Obter(idChamado);
        }

        public bool Gravar(int idChamado, string assunto, string solicitante, int idDepartamento, DateTime dataAbertura)
        {
            if (dataAbertura.Date < DateTime.Today)
                throw new ApplicationException("Não é permitido gravar chamado com data retroativa.");

            return _dal.Gravar(idChamado, assunto, solicitante, idDepartamento, dataAbertura);
        }

        public bool Excluir(int idChamado)
        {
            return _dal.Excluir(idChamado);
        }
    }
}
