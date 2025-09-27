using System.ComponentModel.DataAnnotations;

namespace CooperaGame.Models
{
    public class Jugador
    {
        #region Propiedades
        public int Id {  get; set; }

        [Required]
        [MaxLength(20,ErrorMessage = "El Nombre no puede contener mas de 20 caracteres")]
        [MinLength(1,ErrorMessage = "El nombre debe contener al menos un caracter")]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "El Nombre solo puede contener caracteres alfanumericos")]
        public string Nombre { get; set; } = string.Empty;

        public List<Recoleccion> Recoleccion {  get; set; } = new List<Recoleccion>();
        #endregion Propiedades

    }
}
