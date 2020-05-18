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
    public partial class Form1 : Form
    {
        Timer t = new Timer();
        OleDbConnection con;
        OleDbCommand cmd;
        OleDbDataReader dr;
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //--Saat
            t.Interval = 1000;
            t.Tick += new EventHandler(this.saat);
            t.Start();
            //--Fonksiyonlar
            kytbtngizle();
        }
        private void girisYap_Click(object sender, EventArgs e)
        {
            giriskontrol();
        }
        private void personel_FormClosed(object sender, FormClosedEventArgs e)
        {
            Visible = true;
        }
        private void yonetici_FormClosed(object sender, FormClosedEventArgs e)
        {Visible = true;}
        private void kayitbtn_Click(object sender, EventArgs e)
        {   yoneticikayit yeniyonetici= new yoneticikayit();
            yeniyonetici.Show();
            Visible = false;
            yeniyonetici.FormClosed += yeniyonetici_FormClosed;}
        private void yeniyonetici_FormClosed(object sender, FormClosedEventArgs e)
        {
            Visible = true;
        }
        private void programkapatbtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {}
        private void panel3_Paint(object sender, PaintEventArgs e)
        {}
        private void kytbtngizle()
        {
            con = new OleDbConnection("Provider=Microsoft.Ace.OleDb.12.0;Data Source=personel.accdb");
            cmd = new OleDbCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "SELECT COUNT(*) FROM personel WHERE gorev=True";
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read() == true)
                {
                    if (Convert.ToString(dr[0]) =="1")
                    {
                        yoneticiKytBtn.Visible = false;
                    }
                    else
                    {
                        yoneticiKytBtn.Visible = true;
                    }
                }
            }

        }
        private void sifregosterbtn_Click(object sender, EventArgs e)
        {
            if (textBox2.PasswordChar == '•')
            {
                textBox2.PasswordChar = '\0';
            }
            else
            {
                textBox2.PasswordChar = '•';
            }
        }
        private void label2_Click(object sender, EventArgs e)
        {
        }
        private void giriskontrol()
        {
            con = new OleDbConnection("Provider=Microsoft.Ace.OleDb.12.0;Data Source=personel.accdb");
            cmd = new OleDbCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "SELECT * FROM personel where kadi='" + textBox1.Text + "' AND sifre='" + textBox2.Text + "'";
            dr = cmd.ExecuteReader();
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Kullanıcı Adı ve Şifre Bölümleri Boş Bırakılamaz\n\n   Lütfen Kullanıcı Adı ve Şifre Alanlarını Doldurun!");
            }
            else
            {
                if (dr.Read())
                {

                    if (dr["gorev"].ToString() == "True")
                    {
                        yoneticiekrani yonetici = new yoneticiekrani();
                        yonetici.Show();
                        Visible = false;
                        yonetici.FormClosed += yonetici_FormClosed;
                    }
                    else
                    {
                        personelekrani personel = new personelekrani();
                        personel.Show();
                        Visible = false;
                        personel.FormClosed += personel_FormClosed;

                    }
                }
                else
                { MessageBox.Show("Yanlış Bir Giriş Yaptınız\n\n  Lütfen Girdiğiniz Kullanıcı Adı ve Şifre Bilgisini Kontrol Edin!"); }
            }
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            sifremiunuttum yenisifre = new sifremiunuttum();
            yenisifre.Show();
        }
        void saat(object sender, EventArgs e)
        {
            int hh = DateTime.Now.Hour;
            int mm = DateTime.Now.Minute;
            int ss = DateTime.Now.Second;
            string time = "";
            if (hh < 10)
            {
                time += "0" + hh;
            }
            else
            {
                time += hh;
            }
            time += ":";

            if (mm < 10)
            {
                time += "0" + mm;
            }
            else
            {
                time += mm;
            }
            time += ":";

            if (ss < 10)
            {
                time += "0" + ss;
            }
            else
            {
                time += ss;
            }
            label6.Text = time;
        }
    }
}
