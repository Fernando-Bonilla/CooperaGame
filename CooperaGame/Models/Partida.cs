namespace CooperaGame.Models
{
    public class Partida
    {
        #region Propiedades
        public int Id { get; set; }
        public string Estado { get; set; } = string.Empty;
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
