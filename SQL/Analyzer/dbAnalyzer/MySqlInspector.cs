using MySql.Data.MySqlClient;
using SQL.Service.Data_Base;

namespace SQL.Analyzer.dbAnalyzer
{
    public class MySqlInspector
    {
        public static bool checkId(int id,string dbName,string tabelName)
        {
            Singleton.GetInstance().getConnection(dbName: dbName).Open();
            string sqlRequest = 
                $"Select `id`"+
                $"From `{tabelName}`"+
                "Where `id` = "+id;

            MySqlCommand comand = new MySqlCommand(sqlRequest, Singleton.GetInstance().getConnection(dbName: dbName));
            MySqlDataReader reader = comand.ExecuteReader();
            try
            {
                return reader.Read();
            }
            finally
            {
                reader.Close();
                Singleton.GetInstance().getConnection().Close();
            }
        }
    }
}
