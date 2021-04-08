using System.ComponentModel.DataAnnotations;

namespace Pasquinelli.Martina._5H.SecondaWeb.DTO
{
  public class SigInDTO
  {
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Confirm Password")]
    [Compare("Password", ErrorMessage = "Password and confirmation password not match.")]
    public string ConfirmPassword { get; set; }
  }
}