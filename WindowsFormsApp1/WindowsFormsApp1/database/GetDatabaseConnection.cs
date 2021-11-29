using MySql.Data.MySqlClient;

namespace WindowsFormsApp1
{
    class GetDatabaseConnection
    {
        public MySqlConnection getDatabaseConnection()
        {
            string datasource = "datasource=localhost;username=#######;password=######;database=pimviii";
            MySqlConnection Conexao = new MySqlConnection(datasource);
            Conexao.Open();
            return Conexao;
        }
    }
}
