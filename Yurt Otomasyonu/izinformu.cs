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
using Microsoft.Office.Interop;
using System.Reflection.Emit;
namespace Yurt_Otomasyonu
{

    public partial class izinformu : Form
    {
        OleDbConnection con;
        OleDbCommand cmd;
        OleDbDataAdapter da;
        DataTable dt;
        DataSet ds;
        public izinformu()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string vtyolu = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=ogrenciizin.accdb;Persist Security Info=True";
            OleDbConnection con = new OleDbConnection(vtyolu);
            con.Open();
            string ekle = "insert into ogrenciizin(tckimlik,adi,soyadi,izincikistarihi,izingiristarihi,izinyeri) values (@tckimlik,@adi,@soyadi,@izincikistarihi,@izingiristarihi,@izinyeri)";
            OleDbCommand kmt = new OleDbCommand(ekle, con);
            if (textBox1.Text == "" || textBox2.Text == "" || dateTimePicker1.Text == "" || textBox3.Text == "" || maskedTextBox1.Text == "")
            {
                MessageBox.Show("Lütfen TÜM Alanları Doldurunuz!!!!!");
            }
            else
            {
                kmt.Parameters.AddWithValue("@tckimlik", maskedTextBox1.Text);
                kmt.Parameters.AddWithValue("@adi", textBox1.Text);
                kmt.Parameters.AddWithValue("@soyadi", textBox2.Text);
                dateTimePicker1.Format = DateTimePickerFormat.Custom;
                dateTimePicker1.CustomFormat = "dd.MM.yyyy";
                kmt.Parameters.AddWithValue("@izincikistarihi", dateTimePicker1.Text);
                dateTimePicker2.Format = DateTimePickerFormat.Custom;
                dateTimePicker2.CustomFormat = "dd.MM.yyyy";
                kmt.Parameters.AddWithValue("@izingiristarihi", dateTimePicker2.Text);
                kmt.Parameters.AddWithValue("@izinyeri", textBox3.Text);
                kmt.ExecuteNonQuery();
                kmt.Dispose();    
            }
        }
        private void izinformu_Load(object sender, EventArgs e)
        {
            ogrenciizindatacek();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            ogrenciizindatacek();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            ogrenciizindatasil();
        }
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            ogrenciizindataara();
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
        void ogrenciizindatasil()
        {
            if (MessageBox.Show("Seçili Ögeyi Silmek İstiyor Musunuz ?", "Dikkat", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                con = new OleDbConnection("Provider=Microsoft.Ace.Oledb.12.0;Data Source=ogrenciizin.accdb");
                con.Open();
                cmd = new OleDbCommand();
                cmd.Connection = con;
                cmd.CommandText = "DELETE FROM ogrenciizin WHERE tckimlik=@tckimlik";
                cmd.Parameters.AddWithValue("@tckimlik", dataGridView1.CurrentRow.Cells[0].Value.ToString());
                cmd.ExecuteReader();
                con.Close();
                MessageBox.Show("Silme İşlemi Başarılı", "Silindi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ogrenciizindatacek();
            }
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
