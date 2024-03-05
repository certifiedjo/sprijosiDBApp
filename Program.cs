using Microsoft.Data.SqlClient;
using static System.Console;

class Program
{
    static void Main()
    {


        //ykumar 2/11/2023 CPS*3330 01 - Josiah Springer, Pedro Romero
        WriteLine("It runs!");

        // Create the connection to the resource!
        //using (SqlConnection conn = new SqlConnection()) 
        SqlConnection conn = new SqlConnection();
        // Create the connectionString - USE YOUR CREDENTIALS
        conn.ConnectionString = "Server = CLASSIFIEDPC\\SQLEXPRESS; Database = Users; Trusted_Connection = True; Trust Server Certificate=true";
        conn.Open();

        // Create the command 
        SqlCommand command = new SqlCommand("SELECT * FROM sprijosiTable", conn);

        int max = 0;

        //read from the database
        using (SqlDataReader reader = command.ExecuteReader())
        {
            WriteLine("UserID\tUserName\tUserEmail");
            while (reader.Read())
            {
                WriteLine(String.Format("{0} \t | {1} \t | {2}", reader[0], reader[1], reader[2]));
                max = Convert.ToInt32(reader[0].ToString());//jp 
            }
        }

        WriteLine("Data displayed! Press enter to Proceed!");
        WriteLine("Max ID is " + max + ".");//jp
        WriteLine("Select is successful. Press Enter to Proceed");//jp
        ReadLine();
        Clear();

        //insert data into the database, the query uses parameters - secure way to insert
        WriteLine("INSERT INTO command");

        SqlCommand insertCommand = new SqlCommand("INSERT INTO sprijosiTable (UserId,UserName,  UserEmail) VALUES (@0, @1, @2)", conn);

        insertCommand.Parameters.Add(new SqlParameter("0", (max + 1)));//jp
        insertCommand.Parameters.Add(new SqlParameter("1", "New User"));
        insertCommand.Parameters.Add(new SqlParameter("2", "newuser@kean.edu"));

        // Execute the command, and print the values of the columns affected through  // the command executed. 
        WriteLine("Commands executed! Total rows affected are " + insertCommand.ExecuteNonQuery());

        WriteLine("Insert is successful. Press Enter to Proceed");//jp
        ReadLine();
        Clear();

        // In this section there is an example of the Exception / caught error case 

        WriteLine("Now the error trial! Press Enter to Complete.");
        try
        {
            // Create the command to execute! With the wrong name of the table (Depends on your  Database tables) 
            SqlCommand errorCommand = new SqlCommand("SELECT * FROM sprijosiTable", conn);
            errorCommand.ExecuteNonQuery();
        }

        catch (SqlException er)
        {
            WriteLine("There was an error reported by SQL Server, " + er.Message);
        }

        // Final step, close the resources flush dispose them. ReadLine to prevent the console from  closing. 
        WriteLine("All done! Good Job!");//jp
        ReadLine();


    }
}

