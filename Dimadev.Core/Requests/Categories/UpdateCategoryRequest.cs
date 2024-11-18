using Dimadev.Core.Requests;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dima.Core.Requests.Categories
{
    public class UpdateCategoryRequest : Request
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Título Inválido")]
        [MaxLength(80, ErrorMessage = "O titulo deve conter ate 80 caracteres")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Descricao inválida")]
        public string Description { get; set; } = string.Empty;
        
    } 
}
