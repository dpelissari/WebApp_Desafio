using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Runtime.Serialization;

namespace WebApp_Desafio_FrontEnd.ViewModels
{
    [DataContract]
    public class ChamadoViewModel
    {
        private readonly CultureInfo _ptBR = new CultureInfo("pt-BR");

        [Display(Name = "ID")]
        [DataMember(Name = "ID")]
        public int ID { get; set; }

        [Display(Name = "Assunto")]
        [DataMember(Name = "Assunto")]
        [Required(ErrorMessage = "O Assunto é obrigatório.")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "O Assunto deve ter entre 3 e 200 caracteres.")]
        public string Assunto { get; set; }

        [Display(Name = "Solicitante")]
        [DataMember(Name = "Solicitante")]
        [Required(ErrorMessage = "O Solicitante é obrigatório.")]
        [StringLength(100, ErrorMessage = "O Solicitante deve ter no máximo 100 caracteres.")]
        public string Solicitante { get; set; }

        [Display(Name = "Departamento")]
        [DataMember(Name = "IdDepartamento")]
        [Required(ErrorMessage = "O Departamento é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "O Departamento é obrigatório.")]
        public int IdDepartamento { get; set; }

        [Display(Name = "Departamento")]
        [DataMember(Name = "Departamento")]
        public string Departamento { get; set; }

        [Display(Name = "Data de Abertura")]
        [DataMember(Name = "DataAbertura")]
        [Required(ErrorMessage = "A Data de Abertura é obrigatória.")]
        public DateTime DataAbertura { get; set; }

        [DataMember(Name = "DataAberturaWrapper")]
        public string DataAberturaWrapper
        {
            get
            {
                return DataAbertura.ToString("d", _ptBR);
            }
            set
            {
                DataAbertura = DateTime.Parse(value, _ptBR);
            }
        }
    }
}
