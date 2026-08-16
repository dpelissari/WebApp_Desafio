using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace WebApp_Desafio_FrontEnd.ViewModels
{
    [DataContract]
    public class DepartamentoViewModel
    {
        [Display(Name = "ID")]
        [DataMember(Name = "ID")]
        public int ID { get; set; }

        [Display(Name = "Descrição")]
        [DataMember(Name = "Descricao")]
        [Required(ErrorMessage = "A Descrição é obrigatória.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "A Descrição deve ter entre 3 e 100 caracteres.")]
        public string Descricao { get; set; }
    }
}
