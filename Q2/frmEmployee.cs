using Q2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Q2
{
    public partial class frmEmployee : Form
    {
        PE_PRN_Sum21Context db = new PE_PRN_Sum21Context();

        public frmEmployee()
        {
            InitializeComponent();
        }

        void Reload()
        {
            dgvEmployee.DataSource = db.Employees.ToList();
            txtName.Clear();
            rbMale.Checked = true;
            rbFemale.Checked = false;
            numSalary.Value = 1000;
            mtxtPhone.Clear();
            dgvEmployee.ReadOnly = true;
            dgvEmployee.Columns[5].Visible = false;
        }

        private void frmEmployee_Load(object sender, EventArgs e)
        {
            Reload();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            bool gender = rbMale.Checked;
            float salary = float.Parse(numSalary.Value.ToString());
            string phone = mtxtPhone.Text;
            Employee emp = new Employee()
            {
                EmployeeName = name,
                Male = gender,
                Salary = salary,
                Phone = phone
            };
            db.Employees.Add(emp);
            db.SaveChanges();
            Reload();
        }

        private void dgvEmployee_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtName.Text = dgvEmployee.CurrentRow.Cells[1].Value.ToString();
            bool gender = dgvEmployee.CurrentRow.Cells[2].Value.ToString().Equals("True");
            if (gender)
            {
                rbMale.Checked = true;
                rbFemale.Checked = false;
            }
            else
            {
                rbMale.Checked = false;
                rbFemale.Checked = true;
            }
            numSalary.Value = decimal.Parse(dgvEmployee.CurrentRow.Cells[3].Value.ToString());
            mtxtPhone.Text = dgvEmployee.CurrentRow.Cells[4].Value.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int ID = Int32.Parse(dgvEmployee.CurrentRow.Cells[0].Value.ToString());
            string name = txtName.Text;
            bool gender = rbMale.Checked;
            float salary = float.Parse(numSalary.Value.ToString());
            string phone = mtxtPhone.Text;
            var emp = (from x in db.Employees
                       where x.EmployeeId == ID
                       select x).FirstOrDefault();
            emp.EmployeeName = name;
            emp.Male = gender;
            emp.Salary = salary;
            emp.Phone = phone;
            db.Employees.Update(emp);
            db.SaveChanges();
            Reload();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int ID = Int32.Parse(dgvEmployee.CurrentRow.Cells[0].Value.ToString());
            var emp = (from x in db.Employees
                       where x.EmployeeId == ID
                       select x).FirstOrDefault();
            db.Employees.Remove(emp);
            db.SaveChanges();
            Reload();
        }
    }
}
