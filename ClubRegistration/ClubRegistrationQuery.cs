using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Policy;

namespace ClubRegistration
{
    internal class ClubRegistrationQuery
    {
        private SqlConnection sqlConnect;
        private SqlCommand sqlCommand;
        private SqlDataAdapter sqlAdapter;
        public DataTable dataTable;
        public BindingSource bindingSource;
        public string connectionString;
        public string _FirstName, _MiddleName, _LastName, _Gender, _Program;
        public int _Age;
        public ClubRegistrationQuery()
        {
            connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\YoshimaruHaru\source\repos\ClubRegistration\ClubRegistration\ClubDb.mdf;Integrated Security=True";
            sqlConnect = new SqlConnection(connectionString);
            dataTable = new DataTable();
            bindingSource = new BindingSource();
        }

        public bool DisplayList()
        {
          
            string query = "SELECT StudentId, FirstName, MiddleName, LastName, Age, Gender, Program FROM [ClubMember]";
            sqlAdapter = new SqlDataAdapter(query, sqlConnect);
            dataTable.Clear();
            sqlAdapter.Fill(dataTable);
            bindingSource.DataSource = dataTable;
            return true;
        }

        public bool RegisterStudent(int ID ,long StudentId, string FirstName, string MiddleName, string LastName, int Age, string Gender, string Program)
        {
            sqlCommand = new SqlCommand("INSERT INTO [ClubMember] (ID, StudentId, FirstName, MiddleName, LastName, Age, Gender, Program) " +
                                        "VALUES (@ID, @StudentId, @FirstName, @MiddleName, @LastName, @Age, @Gender, @Program)", sqlConnect);
            sqlCommand.Parameters.AddWithValue("@ID", ID);
            sqlCommand.Parameters.AddWithValue("@StudentId", StudentId);
            sqlCommand.Parameters.AddWithValue("@FirstName", FirstName);
            sqlCommand.Parameters.AddWithValue("@MiddleName", MiddleName);
            sqlCommand.Parameters.AddWithValue("@LastName", LastName);
            sqlCommand.Parameters.AddWithValue("@Age", Age);
            sqlCommand.Parameters.AddWithValue("@Gender", Gender);
            sqlCommand.Parameters.AddWithValue("@Program", Program);

            sqlConnect.Open();
            sqlCommand.ExecuteNonQuery();
            sqlConnect.Close();
            return true;
        }
        public SqlConnection GetSqlConnection()
        {
            return sqlConnect;
        }
    }
}
