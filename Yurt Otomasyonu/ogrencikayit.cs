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
    public partial class ogrencikayit : Form
    {
        public ogrencikayit()
        {
            InitializeComponent();
        }
        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }
        private void label8_Click(object sender, EventArgs e)
        {

        }
        private void yeniogrencibtn_Click(object sender, EventArgs e)
        {
            ogrenciekle();
        }
        private void ogrenciekle()
        {
            string vtyolu = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=ogrenci.accdb;Persist Security Info=True";
            OleDbConnection con = new OleDbConnection(vtyolu);
            con.Open();
            string ekle = "insert into ogrenci(adi,soyadi,dtarihi,cinsiyet,adres,telefon,vtelefon,tckimlik,okuladi,bolum,sinif,nort) values (@adi,@soyadi,@dtarihi,@cinsiyet,@adres,@telefon,@vtelefon,@tckimlik,@okuladi,@bolum,@sinif,@nort)";
            OleDbCommand kmt = new OleDbCommand(ekle, con);
            if (textBox1.Text == "" || textBox2.Text == "" || dateTimePicker1.Text == "" || radioButton1.Text == "" || textBox3.Text == "" || maskedTextBox1.Text == "" || maskedTextBox2.Text == "" || maskedTextBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "" || comboBox1.GetItemText(comboBox1.SelectedItem)=="")
            {MessageBox.Show("Lütfen TÜM Alanları Doldurunuz!!!!!");
            }
            else { 
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
            kmt.Parameters.AddWithValue("@vtelefon", maskedTextBox3.Text);
            kmt.Parameters.AddWithValue("@tckimlik", maskedTextBox1.Text);
            kmt.Parameters.AddWithValue("@okuladi", textBox4.Text);
            kmt.Parameters.AddWithValue("@bolum", textBox5.Text);
            kmt.Parameters.AddWithValue("@sinif", comboBox1.GetItemText(comboBox1.SelectedItem));
            kmt.Parameters.AddWithValue("@nort", textBox6.Text);
            kmt.ExecuteNonQuery();
            Close();
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
