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
    public partial class ogrenciguncelle : Form
    {
        public string TcKimlik;
        OleDbCommand cmd;
        OleDbConnection con;
        OleDbDataReader dr;
        public ogrenciguncelle(string tckimlik)
        {
            TcKimlik = tckimlik;
            InitializeComponent();
        }
        void datacek()
        {
            con = new OleDbConnection("provider=Microsoft.Ace.Oledb.12.0;Data Source=ogrenci.accdb");
            con.Open();
            cmd = new OleDbCommand("SELECT adi,soyadi,dtarihi,cinsiyet,adres,telefon,vtelefon,tckimlik,okuladi,bolum,sinif,nort FROM ogrenci WHERE tckimlik=@TC", con);
            cmd.Parameters.AddWithValue("@TC", TcKimlik.ToString());
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                textBox9.Text = dr["adi"].ToString();
                textBox8.Text = dr["soyadi"].ToString();
                dateTimePicker2.Value = Convert.ToDateTime(dr["dtarihi"]);
                if (dr["cinsiyet"].ToString() == "Erkek")
                { radioButton4.Checked = true; }
                else { radioButton3.Checked = true; }
                textBox7.Text = dr["adres"].ToString();
                maskedTextBox5.Text = dr["telefon"].ToString();
                maskedTextBox4.Text = dr["vtelefon"].ToString();
                maskedTextBox6.Text = dr["tckimlik"].ToString();
                textBox4.Text = dr["okuladi"].ToString();
                textBox5.Text = dr["bolum"].ToString();
                if (dr["sinif"].ToString() == "Hazırlık")
                {
                    comboBox1.SelectedIndex = 0;
                }
                else if (dr["sinif"].ToString() == "1")
                {
                    comboBox1.SelectedIndex = 1;
                }
                else if (dr["sinif"].ToString() == "2")
                {
                    comboBox1.SelectedIndex = 2;
                }
                else if (dr["sinif"].ToString() == "3")
                {
                    comboBox1.SelectedIndex = 3;
                }
                else if (dr["sinif"].ToString() == "4")
                {
                    comboBox1.SelectedIndex = 4;
                }
                else if (dr["sinif"].ToString() == "5")
                {
                    comboBox1.SelectedIndex = 5;
                }
                else if (dr["sinif"].ToString() == "6")
                {
                    comboBox1.SelectedIndex = 6;
                }
                else if (dr["sinif"].ToString() == "Uzatma")
                {
                    comboBox1.SelectedIndex = 7;
                }
                textBox6.Text = dr["nort"].ToString();
            }
            con.Close();
        }
        void dataguncelle()
        {
            con.Open();
            cmd = new OleDbCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "UPDATE ogrenci SET adi=@adi,soyadi=@soyadi,dtarihi=@dtarihi,cinsiyet=@cinsiyet,adres=@adres,telefon=@telefon,vtelefon=@vtelefon,tckimlik=@tckimlik,okuladi=@okuladi,bolum=@bolum,sinif=@sinif,nort=@nort WHERE tckimlik=@tckimlik";
            cmd.Parameters.AddWithValue("@adi", textBox9.Text);
            cmd.Parameters.AddWithValue("@soyadi", textBox8.Text);
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "dd.MM.yyyy";
            cmd.Parameters.AddWithValue("@dtarihi", dateTimePicker2.Text);
            if (radioButton4.Checked)
            { cmd.Parameters.AddWithValue("@cinsiyet", radioButton4.Text);}
            else
            { cmd.Parameters.AddWithValue("@cinsiyet", radioButton3.Text);}
            cmd.Parameters.AddWithValue("@adres", textBox7.Text);
            cmd.Parameters.AddWithValue("@telefon", maskedTextBox5.Text);
            cmd.Parameters.AddWithValue("@vtelefon", maskedTextBox4.Text);
            cmd.Parameters.AddWithValue("@tckimlik", maskedTextBox6.Text);
            cmd.Parameters.AddWithValue("@okuladi", textBox4.Text);
            cmd.Parameters.AddWithValue("@bolum", textBox5.Text);
            cmd.Parameters.AddWithValue("@sinif", comboBox1.GetItemText(comboBox1.SelectedItem));
            cmd.Parameters.AddWithValue("@nort", textBox6.Text);
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            con.Close();
            {
                MessageBox.Show("Güncelleme Tamamlandı!", "Veri Tabanına Kayıt İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                con.Close();
            }
            Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            dataguncelle();
        }
        private void ogrenciguncelle_Load(object sender, EventArgs e)
        {
            datacek();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
