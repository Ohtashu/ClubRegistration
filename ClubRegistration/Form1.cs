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
using Microsoft.SqlServer.Server;

namespace ClubRegistration
{
    public partial class FrmClubRegistration : Form
    {
        ComboBoxValue ComboBoxValue = new ComboBoxValue();
        public FrmClubRegistration()
        {
            InitializeComponent();
        }
        ClubRegistrationQuery clubQuery = new ClubRegistrationQuery();

        private void FrmClubRegistration_Load(object sender, EventArgs e)
        {
            RefreshListOfClubMembers();


            cmbGender.Items.AddRange(ComboBoxValue.GetGenderOptions());
            cmbProgram.Items.AddRange(ComboBoxValue.GetProgramOptions());
        }
        private void RefreshListOfClubMembers()
        {
            clubQuery.DisplayList();
            dgvClubMembers.DataSource = clubQuery.bindingSource;
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            int ID = GenerateID();
            long StudentId = long.Parse(txtStudentID.Text);
            string FirstName = txtFirstName.Text;
            string MiddleName = txtMiddleName.Text;
            string LastName = txtLastName.Text;
            int Age = int.Parse(txtAge.Text);
            string Gender = cmbGender.SelectedItem.ToString();
            string Program = cmbProgram.Text;

            clubQuery.RegisterStudent(ID, StudentId, FirstName, MiddleName, LastName, Age, Gender, Program);
            RefreshListOfClubMembers();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
         
            FrmUpdateMember frmUpdateMember = new FrmUpdateMember();

           
            if (frmUpdateMember.ShowDialog() == DialogResult.OK)
            {
              
                RefreshListOfClubMembers();
            }
        }
        private int GenerateID()
        {
            return new Random().Next(1000,9999);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            clubQuery.DisplayList();
            dgvClubMembers.DataSource = clubQuery.bindingSource;
        }
    }
}
