namespace CooperaGame.Models
{
    public class Partida
    {
        private Random _secuenciaRandomica;
        private const int _valorMinimo = 10;
        private const int _valorMaximo = 16; // esto despues pasarlo a 100, ahora lo dejo entre 10 y 15 para mas facil testear cuando se llega a la meta

        #region Propiedades
        public int Id { get; set; }
        public string Estado { get; set; } = string.Empty;
        public int CantMadera { get; set; } = 0;
        public int CantPiedra { get; set; } = 0;
        public int CantComida { get; set; } = 0;
        public int Semilla { get; set; } = 0; // Propiedad que me asegura que no se genere siempre el mismo numero PseudoRandomico, lo que me costó entender esto mamita posho

        public List<Recoleccion> Recoleccion {  get; set; } = new List<Recoleccion>();
        #endregion Propiedades

        public Partida()
        {
            Semilla = Environment.TickCount;
            _secuenciaRandomica = new Random(Semilla);
        }

        public Partida(int semilla)
        {
            Semilla = semilla;
            _secuenciaRandomica = new Random(Semilla);
        }

        #region Metodos      

        public void GenerarMetasRecursos()
        {
            GenerarMetaCantMadera(_secuenciaRandomica.Next(_valorMinimo, _valorMaximo));
            GenerarMetaCantPiedra(_secuenciaRandomica.Next(_valorMinimo, _valorMaximo));
            GenerarMetaCantComida(_secuenciaRandomica.Next(_valorMinimo, _valorMaximo));
        }

        private void GenerarMetaCantMadera(int totalRecurso)
        {
            CantMadera = totalRecurso;            
        }
        private void GenerarMetaCantPiedra(int totalRecurso)
        {
            CantPiedra = totalRecurso;
        }
        private void GenerarMetaCantComida(int totalRecurso)
        {
            CantComida = totalRecurso;
        }

        #endregion Metodos

    }
}
