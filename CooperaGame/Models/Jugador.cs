namespace CooperaGame.Models
{
    public class Jugador
    {
        #region Propiedades
        public int Id {  get; set; }
        public string Nombre { get; set; } = string.Empty;

        public List<Recoleccion> Recoleccion {  get; set; } = new List<Recoleccion>();
        #endregion Propiedades

    }
}
