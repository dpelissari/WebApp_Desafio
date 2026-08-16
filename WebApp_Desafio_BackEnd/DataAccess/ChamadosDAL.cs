using System;
using System.Collections.Generic;
using System.Data.SQLite;
using WebApp_Desafio_BackEnd.Models;

namespace WebApp_Desafio_BackEnd.DataAccess
{
    public class ChamadosDAL : BaseDAL
    {
        public IEnumerable<Chamado> Listar()
        {
            IList<Chamado> lstChamados = new List<Chamado>();

            using (SQLiteConnection dbConnection = new SQLiteConnection(CONNECTION_STRING))
            {
                using (SQLiteCommand dbCommand = dbConnection.CreateCommand())
                {

                    dbCommand.CommandText = 
                        "SELECT chamados.ID, " + 
                        "       Assunto, " +
                        "       Solicitante, " +
                        "       IdDepartamento, " +
                        "       departamentos.Descricao AS Departamento, " + 
                        "       DataAbertura " + 
                        "FROM chamados " + 
                        "INNER JOIN departamentos " +
                        "   ON chamados.IdDepartamento = departamentos.ID ";

                    dbConnection.Open();

                    using (SQLiteDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            var chamado = new Chamado();

                            if (!dataReader.IsDBNull(0))
                                chamado.ID = dataReader.GetInt32(0);

                            if (!dataReader.IsDBNull(1))
                                chamado.Assunto = dataReader.GetString(1);

                            if (!dataReader.IsDBNull(2))
                                chamado.Solicitante = dataReader.GetString(2);

                            if (!dataReader.IsDBNull(3))
                                chamado.IdDepartamento = dataReader.GetInt32(3);

                            if (!dataReader.IsDBNull(4))
                                chamado.Departamento = dataReader.GetString(4);

                            if (!dataReader.IsDBNull(5))
                                chamado.DataAbertura = DateTime.Parse(dataReader.GetString(5));

                            lstChamados.Add(chamado);
                        }
                        dataReader.Close();
                    }
                    dbConnection.Close();
                }
            }
            return lstChamados;
        }

        public Chamado Obter(int idChamado)
        {
            var chamado = Chamado.Empty;

            using (SQLiteConnection dbConnection = new SQLiteConnection(CONNECTION_STRING))
            {
                using (SQLiteCommand dbCommand = dbConnection.CreateCommand())
                {
                    dbCommand.CommandText =
                        "SELECT chamados.ID, " +
                        "       Assunto, " +
                        "       Solicitante, " +
                        "       IdDepartamento, " +
                        "       departamentos.Descricao AS Departamento, " +
                        "       DataAbertura " +
                        "FROM chamados " +
                        "INNER JOIN departamentos " +
                        "   ON chamados.IdDepartamento = departamentos.ID " +
                        "WHERE chamados.ID = @ID";
                    dbCommand.Parameters.AddWithValue("@ID", idChamado);

                    dbConnection.Open();

                    using (SQLiteDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        if (dataReader.Read())
                        {
                            chamado = new Chamado();

                            if (!dataReader.IsDBNull(0))
                                chamado.ID = dataReader.GetInt32(0);

                            if (!dataReader.IsDBNull(1))
                                chamado.Assunto = dataReader.GetString(1);

                            if (!dataReader.IsDBNull(2))
                                chamado.Solicitante = dataReader.GetString(2);

                            if (!dataReader.IsDBNull(3))
                                chamado.IdDepartamento = dataReader.GetInt32(3);

                            if (!dataReader.IsDBNull(4))
                                chamado.Departamento = dataReader.GetString(4);

                            if (!dataReader.IsDBNull(5))
                                chamado.DataAbertura = DateTime.Parse(dataReader.GetString(5));
                        }
                        dataReader.Close();
                    }
                    dbConnection.Close();
                }
            }
            return chamado;
        }

        public bool Gravar(int idChamado, string assunto, string solicitante, int idDepartamento, DateTime dataAbertura)
        {
            int registrosAfetados = -1;

            using (SQLiteConnection dbConnection = new SQLiteConnection(CONNECTION_STRING))
            {
                using (SQLiteCommand dbCommand = dbConnection.CreateCommand())
                {
                    if (idChamado == 0)
                    {
                        dbCommand.CommandText = 
                            "INSERT INTO chamados (Assunto,Solicitante,IdDepartamento,DataAbertura)" +
                            "VALUES (@Assunto,@Solicitante,@IdDepartamento,@DataAbertura)";
                    }
                    else
                    {
                        dbCommand.CommandText = 
                            "UPDATE chamados " + 
                            "SET Assunto=@Assunto, " + 
                            "    Solicitante=@Solicitante, " +
                            "    IdDepartamento=@IdDepartamento, " + 
                            "    DataAbertura=@DataAbertura " + 
                            "WHERE ID=@ID ";
                    }

                    dbCommand.Parameters.AddWithValue("@Assunto", assunto);
                    dbCommand.Parameters.AddWithValue("@Solicitante", solicitante);
                    dbCommand.Parameters.AddWithValue("@IdDepartamento", idDepartamento);
                    dbCommand.Parameters.AddWithValue("@DataAbertura", dataAbertura.ToString("yyyy-MM-dd"));
                    dbCommand.Parameters.AddWithValue("@ID", idChamado);

                    dbConnection.Open();
                    registrosAfetados = dbCommand.ExecuteNonQuery();
                    dbConnection.Close();
                }
            }

            return (registrosAfetados > 0);

        }

        public bool Excluir(int idChamado)
        {
            int registrosAfetados = -1;

            using (SQLiteConnection dbConnection = new SQLiteConnection(CONNECTION_STRING))
            {
                using (SQLiteCommand dbCommand = dbConnection.CreateCommand())
                {
                    dbCommand.CommandText = "DELETE FROM chamados WHERE ID = @ID";
                    dbCommand.Parameters.AddWithValue("@ID", idChamado);

                    dbConnection.Open();
                    registrosAfetados = dbCommand.ExecuteNonQuery();
                    dbConnection.Close();
                }
            }
            return (registrosAfetados > 0);
        }

        public IEnumerable<string> Solicitantes()
        {
            IList<string> lstSolicitantes = new List<string>();

            using (SQLiteConnection dbConnection = new SQLiteConnection(CONNECTION_STRING))
            {
                using (SQLiteCommand dbCommand = dbConnection.CreateCommand())
                {
                    dbCommand.CommandText =
                        "SELECT DISTINCT Solicitante " +
                        "FROM chamados " +
                        "WHERE Solicitante IS NOT NULL " +
                        "  AND Solicitante <> '' " +
                        "ORDER BY Solicitante";

                    dbConnection.Open();

                    using (SQLiteDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            if (!dataReader.IsDBNull(0))
                                lstSolicitantes.Add(dataReader.GetString(0));
                        }
                        dataReader.Close();
                    }
                    dbConnection.Close();
                }
            }

            return lstSolicitantes;
        }
    }
}