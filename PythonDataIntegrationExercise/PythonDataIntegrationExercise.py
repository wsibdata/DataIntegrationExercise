from sqlalchemy import create_engine, text, exc
import pandas as pd
import os

# Directory containing the CSV files
directory = r'<DATA_FILE_DIRECTORY_PATH>'

# Database connection string
connection_string = '<DATABASE_CONNECTION_STRING>'

engine = create_engine(connection_string)
connection = engine.connect()

# Starting table data
query = text('SELECT [Date], [TransactionType], [Fund], [Value] FROM FundData')
dfFundData = pd.read_sql_query(query, connection)

print(dfFundData)

# Loop through all CSV files in the directory
for filename in os.listdir(directory):
    if filename.endswith('.csv'):
        file_path = os.path.join(directory, filename)
       
        try: 
            dfCsv = pd.read_csv(file_path)
        
            def validate_dataframe(dfCsv):
                required_columns = ['Date', 'TransactionType', 'Fund', 'Value']
                for column in required_columns:
                    if column not in dfCsv.columns:
                        raise ValueError(f"Missing required column: {column}")

            # Load the DataFrame into the SQL table
            dfCsv.to_sql('FundData', con=engine, if_exists='append', index=False, method=None)
        
        except (pd.errors.EmptyDataError, pd.errors.ParserError) as e: 
            print(f"Error reading {file_path}: {e}")
        except exc.SQLAlchemyError as e:
            print(f"Database error for {file_path}: {e}")
        except Exception as e:
            print(f"Unexpected error for {file_path}: {e}")

# Ending table data
dfFundData = pd.read_sql_query(query, connection)
print(dfFundData)

connection.close()






