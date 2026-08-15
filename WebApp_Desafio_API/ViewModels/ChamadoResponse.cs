using System;

namespace WebApp_Desafio_API.ViewModels
{
    public class ChamadoResponse
    {
        public int Id { get; set; }
        public string Assunto { get; set; }
        public string Solicitante { get; set; }
        public int IdDepartamento { get; set; }
        public string Departamento { get; set; }
        public DateTime DataAbertura { get; set; }
    }
}
