using Microsoft.Data.SqlClient;
namespace BlazorAppAIChatdemo
{
    public static class DataService
    {
        public static List<List<string>> GetDataTable(string sqlQuery)
        {
            var rows = new List<List<string>>();
            using (SqlConnection connection = new SqlConnection("Server=(LocalDB)\\MSSQLLocalDB;initial catalog=TestDB_1;TrustServerCertificate=True"))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        int count = 0;
                        bool headersAdded = false;
                        while (reader.Read())
                        {
                            count += 1;
                            var cols = new List<string>();
                            var headerCols = new List<string>();
                            if (!headersAdded)
                            {
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    headerCols.Add(reader.GetName(i).ToString());
                                }
                                headersAdded = true;
                                rows.Add(headerCols);
                            }

                            for (int i = 0; i <= reader.FieldCount - 1; i++)
                            {
                                try
                                {
                                    cols.Add(reader.GetValue(i).ToString());
                                }
                                catch
                                {
                                    cols.Add("DataTypeConversionError");
                                }
                            }
                            rows.Add(cols);
                        }
                    }
                }
            }

            return rows;
        }
    }
}
