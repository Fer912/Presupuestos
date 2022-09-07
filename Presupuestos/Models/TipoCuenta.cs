using Microsoft.AspNetCore.Mvc;
using Presupuestos.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace Presupuestos.Models
{
    public class TipoCuenta 
    {
        public int Id { get; set; }
      
        [Required(ErrorMessage ="El campo {0} es REQUERIDO")]
        [Display(Name ="Nombre del Tipo Cuenta")]
        [PrimeraLetraMayuscula]
        [Remote(action: "VerificarSiExisteTipoCuenta",controller:"TiposCuentas")]
        public string Nombre{ get; set; }
        public int UsuarioId { get; set; }
        public int Orden { get; set; }

      


        /* public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
         {
             if(Nombre != null && Nombre.Length > 0)
             {
                 var primeraLetra = Nombre[0].ToString();
                 if(primeraLetra != primeraLetra.ToUpper())
                 {
                     yield return new ValidationResult("La primera letra debe ser mauyuscula", new[] { nameof(Nombre) });

                 }
             }
         }*/
    }
}
