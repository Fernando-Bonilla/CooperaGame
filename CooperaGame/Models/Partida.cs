namespace CooperaGame.Models
{
    public class Partida
    {
        #region Propiedades
        public int Id { get; set; }
        public string Estado { get; set; } = string.Empty;
        public int CantMadera { get; set; } = 0;
        public int CantPiedra { get; set; } = 0;
        public int CantComida { get; set; } = 0;

        public List<Recoleccion> Recoleccion {  get; set; } = new List<Recoleccion>();
        #endregion Propiedades


        #region Metodos
        public string PruebaParaTest()
        {
            return "Holitas";
        }

        public int GenerarNumAleatorio()
        {
            return 1;
        }
        #endregion Metodos

    }
}
