using System.Data.SqlClient;

namespace tut6.Services
{
    public class SqlServerDbService : IDbService
    {
        public bool CheckIndex(string index)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=db-mssql;Initial Catalog=s18376;Integrated Security=True"))
            {
                using (SqlCommand commmand = new SqlCommand())
                {
                    commmand.Connection = connection;
                    commmand.CommandText = "Select IndexNumber from Student Where IndexNumber = @index";
                    commmand.Parameters.AddWithValue("index", index);

                    connection.Open();
                    SqlDataReader dataReader = commmand.ExecuteReader();
                    return dataReader.Read();
                }
            }
        }
    }
}

