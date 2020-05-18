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

namespace Yurt_Otomasyonu
{
    public partial class İzin_Raporu : Form
    {
        OleDbConnection con;
        OleDbDataAdapter da;
        DataSet ds;
        DataTable dt;
        public İzin_Raporu()
        {
            InitializeComponent();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            exceleaktar();
        }
        private void İzin_Raporu_Load(object sender, EventArgs e)
        {
            ogrenciizindatacek();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            ogrenciizindatacek();
        }
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            ogrenciizindataara();
        }
        void exceleaktar()
        {
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.Visible = true;
            Microsoft.Office.Interop.Excel.Workbook kitap = excel.Workbooks.Add(System.Reflection.Missing.Value);
            Microsoft.Office.Interop.Excel.Worksheet sheet1 = (Microsoft.Office.Interop.Excel.Worksheet)kitap.Sheets[1];
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                Microsoft.Office.Interop.Excel.Range aralik = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[1, i + 1];
                aralik.Value2 = dataGridView1.Columns[i].HeaderText.ToString().ToUpper();
            }
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                for (int j = 0; j < dataGridView1.Rows.Count; j++)
                {
                    Microsoft.Office.Interop.Excel.Range aralik = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[j + 2, i + 1];
                    aralik.Value2 = dataGridView1[i, j].Value;
                }
            }
        }
        void ogrenciizindatacek()
        {
            con = new OleDbConnection("Provider=Microsoft.Ace.OleDb.12.0;Data Source=ogrenciizin.accdb");
            da = new OleDbDataAdapter("Select * from ogrenciizin", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "ogrenciizin");
            dataGridView1.DataSource = ds.Tables["ogrenciizin"];
            con.Close();
        }
        void ogrenciizindataara()
        {
            con = new OleDbConnection("Provider=Microsoft.Ace.Oledb.12.0;Data Source=ogrenciizin.accdb");
            con.Open();
            dt = new DataTable();
            string arama = "SELECT * FROM ogrenciizin WHERE tckimlik+adi+soyadi like '%" + textBox4.Text + "%'";
            da = new OleDbDataAdapter(arama, con);
            da.Fill(dt);
            con.Close();
            dataGridView1.DataSource = dt;
        }
    }
}
