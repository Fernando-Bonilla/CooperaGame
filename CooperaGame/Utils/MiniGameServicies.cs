namespace CooperaGame.Utils
{
    public sealed  class MiniGameServicies //usamos patron singleton para asegurarnos que solo exista una instancia del servicio
    {
        private static MiniGameServicies? instance = null;

        private MiniGameServicies()
        {

        }

        public static MiniGameServicies GetServiceInstance()
        {
            if (instance == null)
            {
                instance = new MiniGameServicies();
            }
            return instance;
        }
    }
}
