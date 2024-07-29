using System.Data;
using System.Data.SqlClient;


namespace DataIntegrationExercise;

class Program
{
    static string _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=\DataIntegrationExercise\WsibFundData.mdf;User=SibUser01;Connect Timeout=30";
    static string _directoryPath = @"..\..\..\FundData"; 


    public static async Task Main(string[] args)
    {
        Console.WriteLine("Starting data exercise.");

        bool success = await LoadCsvFilesToDatabaseAsync();
    }

    private static Task<bool> LoadCsvFilesToDatabaseAsync()
    {
        // Get all CSV files in the directory
        string[] csvFiles = Directory.GetFiles(_directoryPath, "*.csv");

        foreach (string csvFile in csvFiles)
        {
            // Read all lines from the CSV file
            string[] lines = File.ReadAllLines(csvFile);

            if (lines.Length > 0)
            {
                // Assume the first line contains the column names
                string[] columnNames = lines[0].Split(',');

                using SqlConnection connection = new(_connectionString);
                connection.Open();

                foreach (string? line in lines.Skip(1))
                {
                    string[] values = line.Split(',');

                    using SqlCommand command = new();
                    command.Connection = connection;

                    // Build the INSERT command
                    string insertCommand = "INSERT INTO dbo.FundDataTable (";
                    insertCommand += string.Join(", ", columnNames);
                    insertCommand += ") VALUES (";
                    insertCommand += string.Join(", ", values.Select((v, i) => $"@param{i}"));
                    insertCommand += ")";
                    
                    command.CommandText = insertCommand;
                    
                    // Add parameters
                    for (int i = 0; i < values.Length; i++)
                    {
                        command.Parameters.AddWithValue($"@param{i}", values[i]);
                    }
                    
                    command.ExecuteNonQuery();
                }
            }
        }
        return Task.FromResult(true);
    }
}

