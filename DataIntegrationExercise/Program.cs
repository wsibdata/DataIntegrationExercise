using Microsoft.Data.SqlClient;



Console.WriteLine("Starting...");

using SqlConnection connection = new("<DATABASE_CONNECTION_STRING>");

try
{
    connection.Open();

    foreach (var f in Directory.GetFiles(@"<DATA_FILE_DIRECTORY_PATH>"))
    {
        using StreamReader sr = new(f);
        string line;
        int lineCount = 0;
        while ((line = sr.ReadLine()) != null)
        {
            if (lineCount > 0) // Skip the header row (line 0)
            {
                string[] values = line.Split(',');
                using SqlCommand cmdInsert = connection.CreateCommand();
                cmdInsert.CommandText = "INSERT INTO FundData ([Date], [TransactionType], [Fund], [Value]) VALUES (@Date, @TransactionType, @Fund, @Value)";
                cmdInsert.Parameters.AddWithValue("@Date", values[0]);
                cmdInsert.Parameters.AddWithValue("@TransactionType", values[1]);
                cmdInsert.Parameters.AddWithValue("@Fund", values[2]);
                cmdInsert.Parameters.AddWithValue("@Value", values[3]);
                cmdInsert.ExecuteNonQuery();
            }
            lineCount++;
        }
    }


    using SqlCommand command = connection.CreateCommand();
    command.CommandText = "SELECT [Date], [TransactionType], [Fund], [Value] FROM FundData";

    using SqlDataReader reader = command.ExecuteReader();
    while (reader.Read())
    {
        Console.WriteLine(reader["Date"] + " " + reader["TransactionType"] + " " + reader["Fund"] + " " + reader["Value"]);
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
finally
{
    connection.Close();
}


