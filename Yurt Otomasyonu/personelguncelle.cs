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
    public partial class personelguncelle : Form
    {
        string TcKimlik;
        OleDbCommand cmd;
        OleDbConnection con;
        OleDbDataReader dr;
        public personelguncelle(string tckimlik)
        {
            TcKimlik = tckimlik;
            InitializeComponent();
        }
        private void personelguncelle_Load(object sender, EventArgs e)
        {
            datacek();
        }
        void datacek()
        {
            con = new OleDbConnection("provider=Microsoft.Ace.Oledb.12.0;Data Source=personel.accdb");
            con.Open();
            cmd = new OleDbCommand("SELECT kadi,sifre,adi,soyadi,dtarihi,cinsiyet,adres,telefon,tcno FROM personel WHERE tcno=@TC", con);
            cmd.Parameters.AddWithValue("@TC", TcKimlik.ToString());
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                textBox4.Text = dr["kadi"].ToString();
                maskedTextBox3.Text = dr["sifre"].ToString();
                textBox1.Text = dr["adi"].ToString();
                textBox2.Text = dr["soyadi"].ToString();
                dateTimePicker1.Value = Convert.ToDateTime(dr["dtarihi"]);
                if (dr["cinsiyet"].ToString() == "Erkek")
                { radioButton1.Checked = true; }
                else { radioButton2.Checked = true; }
                textBox3.Text = dr["adres"].ToString();
                maskedTextBox2.Text = dr["telefon"].ToString();
                maskedTextBox1.Text = dr["tcno"].ToString();
            }
            con.Close();
        }
        void dataguncelle()
        {
            con.Open();
            cmd = new OleDbCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "UPDATE personel SET kadi=@kadi,sifre=@sifre,adi=@adi,soyadi=@soyadi,dtarihi=@dtarihi,cinsiyet=@cinsiyet,adres=@adres,telefon=@telefon,tcno=@tcno WHERE tcno=@tcno";
            cmd.Parameters.AddWithValue("@kadi", textBox4.Text);
            cmd.Parameters.AddWithValue("@sifre", maskedTextBox3.Text);
            cmd.Parameters.AddWithValue("@adi", textBox1.Text);
            cmd.Parameters.AddWithValue("@soyadi", textBox2.Text);
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd.MM.yyyy";
            cmd.Parameters.AddWithValue("@dtarihi", dateTimePicker1.Text);
            if (radioButton1.Checked)
            { cmd.Parameters.AddWithValue("@cinsiyet", radioButton1.Text); }
            else
            { cmd.Parameters.AddWithValue("@cinsiyet", radioButton2.Text); }
            cmd.Parameters.AddWithValue("@adres", textBox3.Text);
            cmd.Parameters.AddWithValue("@telefon",TcKimlik.ToString());
            cmd.Parameters.AddWithValue("@tcno", TcKimlik.ToString());
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            con.Close();
            {
                MessageBox.Show("Güncelleme Tamamlandı!", "Veri Tabanına Kayıt İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                con.Close();
            }
            Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            dataguncelle();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
