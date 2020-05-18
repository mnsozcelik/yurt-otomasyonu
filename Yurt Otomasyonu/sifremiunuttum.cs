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
    public partial class sifremiunuttum : Form
    {
        OleDbConnection con;
        OleDbCommand cmd;
        OleDbDataAdapter da;
        OleDbDataReader dr;
        public sifremiunuttum()
        {
            InitializeComponent();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void sifremiunuttum_Load(object sender, EventArgs e)
        {
            textBox1.Visible = false;
            textBox3.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            button3.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con = new OleDbConnection("Provider=Microsoft.Ace.OleDb.12.0;Data Source=personel.accdb");
            cmd = new OleDbCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "SELECT * FROM personel where tcno='" + maskedTextBox1.Text + "' AND kadi='" + textBox2.Text + "'";
            dr = cmd.ExecuteReader();
            if (maskedTextBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("TC Kimlik ve Kullanıcı Adı Bilgileri Olmadan Güncelleme Yapılamaz\n\n   Lütfen TC Kimlik Alanlarını Doldurun!\n\n   (Bilmiyorsanız Servis Sağlayıcıya Başvurunuz.)");
            }
            else
            {
                if (dr.Read())
                {
                    textBox1.Visible = true;
                    textBox3.Visible = true;
                    label3.Visible = true;
                    label4.Visible = true;
                    button3.Visible = true;
                }
                else
                {
                    textBox1.Visible = false;
                    textBox3.Visible = false;
                    label3.Visible = false;
                    label4.Visible = false;
                    button3.Visible = false;
                    MessageBox.Show("Belirtilen TC Kimlik ve Kullanıcı Adı Bilgilerine Ait Kullanıcı Bulunamadı.\n\n   Lütfen Tekrar Deneyiniz.\n\n   (Eğer Bilgilerden Eminseniz  Servis Sağlayıcıya Başvurunuz.)");
                }
            }
            con.Close();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == textBox3.Text)
            {
                con.Open();
                cmd = new OleDbCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE personel SET sifre=@sifre WHERE tcno=@tcno";
                cmd.Parameters.AddWithValue("@sifre", textBox1.Text);
                cmd.Parameters.AddWithValue("@tcno", maskedTextBox1.Text);
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
                con.Close();
            }
            else
            {
                MessageBox.Show("Lütfen İki Kutucuğa Girilen Yeni Şifre Değerinin Aynı Olduğundan Emin Olun!!");
            }
        }
    }
}
