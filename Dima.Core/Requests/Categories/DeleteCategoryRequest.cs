using System.ComponentModel.DataAnnotations;

namespace Dima.Core.Requests.Categories;

public class DeleteCategoryRequest : Request
{
    [Required(ErrorMessage = "Id da categoria inválido")]
    public long Id { get; set; }
}
