using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ObjectBinding
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cbGender.DataSource = Enum.GetValues(typeof(Gender));
            bindingObj.DataSource = new List<User>();
            bindingObj.AddNew();
            BindControl();
        }

        private void BindControl()
        {
            //Clear all the bindings
            txtFirstName.DataBindings.Clear();
            txtLastName.DataBindings.Clear();
            cbGender.DataBindings.Clear();
            dpDob.DataBindings.Clear();
            lblFullName.DataBindings.Clear();
            btnSave.DataBindings.Clear();

            txtFirstName.DataBindings.Add("Text", bindingObj, "FirstName");
            txtLastName.DataBindings.Add("Text", bindingObj, "LastName");
            cbGender.DataBindings.Add("SelectedItem", bindingObj, "Gender");
            dpDob.DataBindings.Add("Value", bindingObj, "DateOfBirth");
            lblFullName.DataBindings.Add("Text", bindingObj, "FullName");
            btnSave.DataBindings.Add("Enabled", bindingObj, "IsValid");
        }


        private void Save()
        {
            User _current = (User)bindingObj.Current;
            if (_current != null && _current.IsValid)
            {
                bindingObj.AddNew();
                bindingObj.MoveLast();

                txtFirstName.Focus();
            }
            else
            {
                MessageBox.Show("First name and last name is required");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }
    }

    public enum Gender
    {
        Male=0,
        Female=1
    }

    public class User
    {
        public User()
        {
            DateOfBirth = DateTime.Now;
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }




        public string FullName => $"{this.FirstName} {this.LastName}".Trim();
        public bool IsAgeValid => DateOfBirth.Year <= DateTime.Now.AddYears(-18).Year;
        public bool IsValid => !string.IsNullOrEmpty(FirstName) && !string.IsNullOrEmpty(LastName) && IsAgeValid; 
    }

}
