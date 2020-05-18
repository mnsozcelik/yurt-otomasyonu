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
    public partial class yoneticikayit : Form
    {
        public yoneticikayit()
        {
            InitializeComponent();
        }
        private void label8_Click(object sender, EventArgs e)
        {
        }
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
        }
        private void yoneticikayitbtn_Click(object sender, EventArgs e)
        {
            vtkayit();
            Close();
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }
        private void vtkayit()
        {
            string baglanti = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=personel.accdb;Persist Security Info=True";
            OleDbConnection con = new OleDbConnection(baglanti);
            con.Open();
            string ekle = "insert into personel(kadi,sifre,adi,soyadi,dtarihi,cinsiyet,adres,telefon,tcno,gorev) values (@kadi,@sifre,@adi,@soyadi,@dtarihi,@cinsiyet,@adres,@telefon,@tcno,@gorev)";
            OleDbCommand kmt = new OleDbCommand(ekle, con);
            if (textBox1.Text == "" || textBox2.Text == "" || radioButton1.Text == "" || textBox3.Text == "" || maskedTextBox1.Text == "" || maskedTextBox2.Text == "" || maskedTextBox3.Text == "" || textBox4.Text == "" || dateTimePicker1.Text == "")
            {
                MessageBox.Show("Lütfen TÜM Alanları Doldurunuz!!!!!");
            }
            else
            {
                kmt.Parameters.AddWithValue("@kadi", textBox4.Text);
                kmt.Parameters.AddWithValue("@sifre", maskedTextBox3.Text);
                kmt.Parameters.AddWithValue("@adi", textBox1.Text);
                kmt.Parameters.AddWithValue("@soyadi", textBox2.Text);
                dateTimePicker1.Format = DateTimePickerFormat.Custom;
                dateTimePicker1.CustomFormat = "dd.MM.yyyy";
                kmt.Parameters.AddWithValue("@dtarihi", dateTimePicker1.Text);
                if (radioButton1.Checked)
                { kmt.Parameters.AddWithValue("@cinsiyet", radioButton1.Text); }
                else
                { kmt.Parameters.AddWithValue("@cinsiyet", radioButton2.Text); }
                kmt.Parameters.AddWithValue("@adres", textBox3.Text);
                kmt.Parameters.AddWithValue("@telefon", maskedTextBox2.Text);
                kmt.Parameters.AddWithValue("@tcno", maskedTextBox1.Text);
                kmt.Parameters.AddWithValue("@gorev", true);
                kmt.ExecuteNonQuery();
                kmt.Dispose();
            }

        }
    }
}
