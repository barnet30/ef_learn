using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace teleprof
{
    public partial class Form1 : Form
    {
        teleprofContext db;
        public Form1()
        {
            InitializeComponent();
            db = new teleprofContext();
            db.Cities.Load();

            dgvCity.DataSource = db.Cities.Local.ToBindingList();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            CityForm cityForm = new CityForm();
            DialogResult result = cityForm.ShowDialog(this);

            if (result == DialogResult.Cancel)
                return;

            City city = new City();
            city.Name = cityForm.txtName.Text;

            db.Cities.Add(city);
            db.SaveChanges();

            MessageBox.Show("New object has successfully added!");
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvCity.SelectedRows.Count > 0)
            {
                int index = dgvCity.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(dgvCity[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;

                City city= db.Cities.Find(id);

                CityForm cityForm= new CityForm();

                cityForm.txtName.Text = city.Name;


                DialogResult result = cityForm.ShowDialog(this);

                if (result == DialogResult.Cancel)
                    return;

                city.Name = cityForm.txtName.Text;

                db.SaveChanges();
                dgvCity.Refresh(); // обновляем грид
                MessageBox.Show("Объект обновлен");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvCity.SelectedRows.Count > 0)
            {
                int index = dgvCity.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(dgvCity[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;

                City player = db.Cities.Find(id);
                db.Cities.Remove(player);
                db.SaveChanges();

                MessageBox.Show("Объект удален");
            }
        }

        private void table2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Table2Form tbl2 = new Table2Form();
            tbl2.ShowDialog();
        }
    }
}
