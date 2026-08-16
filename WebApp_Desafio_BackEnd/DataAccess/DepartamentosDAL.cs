using System.Collections.Generic;
using System.Data.SQLite;
using WebApp_Desafio_BackEnd.Models;

namespace WebApp_Desafio_BackEnd.DataAccess
{
    public class DepartamentosDAL : BaseDAL
    {
        public IEnumerable<Departamento> Listar()
        {
            IList<Departamento> lstDepartamentos = new List<Departamento>();

            using (SQLiteConnection dbConnection = new SQLiteConnection(CONNECTION_STRING))
            {
                using (SQLiteCommand dbCommand = dbConnection.CreateCommand())
                {
                    dbCommand.CommandText = "SELECT ID, Descricao FROM departamentos";
                    dbConnection.Open();

                    using (SQLiteDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            var departamento = new Departamento();

                            if (!dataReader.IsDBNull(0))
                                departamento.ID = dataReader.GetInt32(0);

                            if (!dataReader.IsDBNull(1))
                                departamento.Descricao = dataReader.GetString(1);

                            lstDepartamentos.Add(departamento);
                        }
                        dataReader.Close();
                    }
                    dbConnection.Close();
                }

            }

            return lstDepartamentos;
        }

        public Departamento Obter(int idDepartamento)
        {
            var departamento = Departamento.Empty;

            using (SQLiteConnection dbConnection = new SQLiteConnection(CONNECTION_STRING))
            {
                using (SQLiteCommand dbCommand = dbConnection.CreateCommand())
                {
                    dbCommand.CommandText = "SELECT ID, Descricao FROM departamentos WHERE ID = @ID";
                    dbCommand.Parameters.AddWithValue("@ID", idDepartamento);
                    dbConnection.Open();

                    using (SQLiteDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        if (dataReader.Read())
                        {
                            departamento = new Departamento();

                            if (!dataReader.IsDBNull(0))
                                departamento.ID = dataReader.GetInt32(0);

                            if (!dataReader.IsDBNull(1))
                                departamento.Descricao = dataReader.GetString(1);
                        }
                        dataReader.Close();
                    }
                    dbConnection.Close();
                }
            }

            return departamento;
        }

        public bool Gravar(int idDepartamento, string descricao)
        {
            int registrosAfetados = -1;

            using (SQLiteConnection dbConnection = new SQLiteConnection(CONNECTION_STRING))
            {
                using (SQLiteCommand dbCommand = dbConnection.CreateCommand())
                {
                    dbCommand.CommandText = idDepartamento == 0 ? "INSERT INTO departamentos (Descricao) VALUES (@Descricao)" : "UPDATE departamentos SET Descricao = @Descricao WHERE ID = @ID";
                    dbCommand.Parameters.AddWithValue("@Descricao", descricao);
                    dbCommand.Parameters.AddWithValue("@ID", idDepartamento);

                    dbConnection.Open();
                    registrosAfetados = dbCommand.ExecuteNonQuery();
                    dbConnection.Close();
                }
            }

            return (registrosAfetados > 0);
        }

        public bool Excluir(int idDepartamento)
        {
            int registrosAfetados = -1;

            using (SQLiteConnection dbConnection = new SQLiteConnection(CONNECTION_STRING))
            {
                using (SQLiteCommand dbCommand = dbConnection.CreateCommand())
                {
                    dbCommand.CommandText = "DELETE FROM departamentos WHERE ID = @ID";
                    dbCommand.Parameters.AddWithValue("@ID", idDepartamento);

                    dbConnection.Open();
                    registrosAfetados = dbCommand.ExecuteNonQuery();
                    dbConnection.Close();
                }
            }

            return (registrosAfetados > 0);
        }
    }
}