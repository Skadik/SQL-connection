using MySql.Data.MySqlClient;

namespace SQL.Service.Data_Base
{
    public class Singleton
    {
        private MySqlConnection connection;
        private Singleton() { }

        private static Singleton instance;

        public static Singleton GetInstance()
        {
            if (instance == null)
            {
                instance = new Singleton();
            }
            return instance;
        }

        public void setConnection(MySqlConnection connection)
        {
            this.connection = connection;
        }

        public MySqlConnection getConnection(string dbName = "shop")
        {
            if (this.connection == null)
            {
                this.connection = DataBaseConnector.getConnection(dbName: dbName);
            }

            return this.connection;
        }
    }
}
