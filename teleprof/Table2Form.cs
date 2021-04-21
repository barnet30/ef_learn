using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace teleprof
{
    public partial class Table2Form : Form
    {

        public Table2Form()
        {
            InitializeComponent();
            LoadData();
            this.Text = "Представление";
        }

        void LoadData()
        {
            using (SqlConnection con = new SqlConnection())
            {
                
                con.ConnectionString = ConfigurationManager.ConnectionStrings["DbConnectionString"].ConnectionString;
                string sql = "select c1.*, c2.Id, c2.Name from Cities c1 left join Cities c2 on c1.Id + 1 = c2.Id";
                con.Open();
                SqlCommand command = new SqlCommand(sql, con);
                SqlDataReader reader = command.ExecuteReader();
                List<string[]> data = new List<string[]>();
                while(reader.Read())
                {
                    data.Add(new string[4]);
                    data[data.Count - 1][0] = reader[0].ToString();
                    data[data.Count - 1][1] = reader[1].ToString();
                    data[data.Count - 1][2] = reader[2].ToString();
                    data[data.Count - 1][3] = reader[3].ToString();
                }
                reader.Close();
                con.Close();

                foreach(string[] s in data)
                {
                    dataGridView1.Rows.Add(s);
                }
            }
        }
      
       
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
