using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dima.Core.Requests;
using System.ComponentModel.DataAnnotations;
using Dimadev.Core.Requests;

//creation of the category request
namespace Dima.Core.Requests.Categories
{
    public class CreateCategoryRequest : Request
    {
        [Required(ErrorMessage ="Título inválido.")]
        [MaxLength(100, ErrorMessage = "O título deve conter no maximo 100 caracteres.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Descricao Inválida.")]
        public string Description { get; set; } = string.Empty;

    }
}
