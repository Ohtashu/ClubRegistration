using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClubRegistration
{
    public partial class FrmUpdateMember : Form
    {
        ComboBoxValue ComboBoxValue = new ComboBoxValue();
        private int selectedId = 0;
        ClubRegistrationQuery clubQuery = new ClubRegistrationQuery();
        public FrmUpdateMember()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void FrmUpdateMember_Load(object sender, EventArgs e)
        {

            LoadStudentIds();



            cmbGender.Items.AddRange(ComboBoxValue.GetGenderOptions());
            cmbProgram.Items.AddRange(ComboBoxValue.GetProgramOptions());
        }
        private void LoadStudentIds()
        {
            try
            {
                string query = "SELECT StudentId, FirstName, MiddleName, LastName, Age, Gender, Program FROM [ClubMember]";
                SqlDataAdapter adapter = new SqlDataAdapter(query, clubQuery.GetSqlConnection());
                DataTable table = new DataTable();
                adapter.Fill(table);

                cmbStudentID.DataSource = table;
                cmbStudentID.DisplayMember = "StudentId";
                cmbStudentID.ValueMember = "StudentId";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading Student IDs: " + ex.Message);
            }
        }

        private void cmbStudentID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbStudentID.SelectedValue == null || cmbStudentID.SelectedValue is DataRowView)
            {

                return;
            }


            LoadMemberDetails();
        }
        private void LoadMemberDetails()
        {
            try
            {
                if (cmbStudentID.SelectedValue == null || cmbStudentID.SelectedValue is DataRowView)
                {
                    MessageBox.Show("Invalid student selected. Please try again.");
                    return;
                }

                string query = "SELECT * FROM ClubMember WHERE StudentId = @StudentId";
                SqlCommand command = new SqlCommand(query, clubQuery.GetSqlConnection());
                command.Parameters.AddWithValue("@StudentId", cmbStudentID.SelectedValue);

                clubQuery.GetSqlConnection().Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    selectedId = Convert.ToInt32(reader["ID"]); 
                    txtLastName.Text = reader["LastName"].ToString();
                    txtFirstName.Text = reader["FirstName"].ToString();
                    txtMiddleName.Text = reader["MiddleName"].ToString();
                    txtAge.Text = reader["Age"].ToString();
                    cmbGender.SelectedItem = reader["Gender"].ToString();
                    cmbProgram.Text = reader["Program"].ToString();
                }
                else
                {
                    MessageBox.Show("No details found for the selected student.");
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading member details: " + ex.Message);
            }
            finally
            {
                if (clubQuery.GetSqlConnection().State == ConnectionState.Open)
                {
                    clubQuery.GetSqlConnection().Close();
                }
            }
        }




        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedId == 0)
                {
                    MessageBox.Show("No valid member selected for update.");
                    return;
                }

                string query = @"UPDATE ClubMember
                         SET FirstName = @FirstName,
                             MiddleName = @MiddleName,
                             LastName = @LastName,
                             Age = @Age,
                             Gender = @Gender,
                             Program = @Program
                         WHERE ID = @ID"; 

                SqlCommand command = new SqlCommand(query, clubQuery.GetSqlConnection());
                command.Parameters.AddWithValue("@ID", selectedId);
                command.Parameters.AddWithValue("@FirstName", txtFirstName.Text);
                command.Parameters.AddWithValue("@MiddleName", txtMiddleName.Text);
                command.Parameters.AddWithValue("@LastName", txtLastName.Text);
                command.Parameters.AddWithValue("@Age", int.Parse(txtAge.Text));
                command.Parameters.AddWithValue("@Gender", cmbGender.SelectedItem?.ToString());
                command.Parameters.AddWithValue("@Program", cmbProgram.Text);

                clubQuery.GetSqlConnection().Open();
                int rowsAffected = command.ExecuteNonQuery();
                clubQuery.GetSqlConnection().Close();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Member updated successfully!");
                }
                else
                {
                    MessageBox.Show("No rows were updated. Please check the selected member.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating member: " + ex.Message);
            }
            finally
            {
                if (clubQuery.GetSqlConnection().State == ConnectionState.Open)
                {
                    clubQuery.GetSqlConnection().Close();
                }
            }
        }
    }
}

