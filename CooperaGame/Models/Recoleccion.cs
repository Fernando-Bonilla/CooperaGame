namespace CooperaGame.Models
{
    public class Recoleccion
    {
        #region Propiedades
        public int Id { get; set; }
        public string Recurso { get; set; } = string.Empty;
        public DateTime Fecha { get; set; } = DateTime.Now;

        public int JugadorId { get; set; }
        public Jugador? Jugador { get; set; }

        public int PartidaId { get; set; }
        public Partida? Partida { get; set; }
        #endregion Propiedades

    }
}
