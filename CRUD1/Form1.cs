using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace CRUD1
{
    public partial class Form1 : Form
    {
        string databaseFileName = "DatabaseCRUD.accdb";
        string databaseFilePath;
        string connectionString;

        public Form1()
        {
            InitializeComponent();

            databaseFilePath = System.IO.Path.Combine(Application.StartupPath, databaseFileName);
            connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + databaseFilePath;
        }

        private void btnAddRecord_Click(object sender, EventArgs e)
        {
            string username = txtName.Text;
            string email = txtEmail.Text;
            string fname = txt_fname.Text;
            string mi = txt_mi.Text;
            string lname = txt_lname.Text;
            string dob = txt_dob.Text;
            string age = txt_age.Text;
            string gender = combo_gender.Text;
            string yr = combo_yrlvl.Text;
            string sec = combo_section.Text;
            string cont = txt_contact.Text;


            string query = "INSERT INTO Customers (Username, Email, FirstName, MiddleInitial, LastName, DateofBirth, Age, Gender, YrLevel, StudentSection, ContactNo) VALUES (@Username, @Email, @fname, @mi, @lname, @dob, @age, @gender, @yr, @sec, @cont)";

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@fname", fname);
                    command.Parameters.AddWithValue("@mi", mi);
                    command.Parameters.AddWithValue("@lname", lname);
                    command.Parameters.AddWithValue("@dob", dob);
                    command.Parameters.AddWithValue("@age", age);
                    command.Parameters.AddWithValue("@gender", gender);
                    command.Parameters.AddWithValue("@yr", yr);
                    command.Parameters.AddWithValue("@sec", sec);
                    command.Parameters.AddWithValue("@cont", cont);
                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        MessageBox.Show("Record added successfully!");
                        txtName.Text ="";
                        txtEmail.Text = "";
                        txt_fname.Text = "";
                        txt_mi.Text = "";
                        txt_lname.Text = "";
                        txt_dob.Text = "";
                        stxt_age.Text = "";
                        combo_gender.Text = "";
                        combo_yrlvl.Text = "";
                        combo_section.Text = "";
                        txt_contact.Text = "";
                        txtName.Focus();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            string email = txtEmail.Text;



            string query = "SELECT * FROM Customers";
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@Username", name);
                        command.Parameters.AddWithValue("@Email", email);

                        connection.Open();
                        OleDbDataAdapter adapter = new OleDbDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        if (dataTable.Rows.Count == 0)
                        {
                            MessageBox.Show("No matching records found.");
                        }
                        else
                        {
                            dgvCustomers.DataSource = dataTable;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }

            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string username = txtName.Text;
            string email = txtEmail.Text;
            string query = "UPDATE Customers SET Email = @Email WHERE Username = @Username";

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Username", username);

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Record updated successfully!");
                        }
                        else
                        {
                            MessageBox.Show("No matching record found.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string username = txtName.Text;

            string query = "DELETE FROM Customers WHERE Username = @Username";
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {

                    command.Parameters.AddWithValue("@Name", username);
                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Record deleted successfully!");
                        }
                        else
                        {
                            MessageBox.Show("No matching record found.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }

                }
            }
        }


    }
}
