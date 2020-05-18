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
    public partial class yoneticiekrani : Form
    {
        Timer t = new Timer();
        OleDbConnection con;
        OleDbCommand cmd;
        OleDbDataAdapter da;
        DataTable dt;
        DataSet ds;
        public yoneticiekrani()
        {
            InitializeComponent();
        }
        private void yoneticiekrani_Load(object sender, EventArgs e)
        {
            //Saat---------------
            t.Interval = 1000;
            t.Tick += new EventHandler(this.saat);
            t.Start();
            //fonksiyonlar
            label1.Text = "Yönetici Kontrol Paneli";
            ogrencidatacek();
            personeldatacek();
        }
        //--YENİ ÖĞRENCİ BUTONU İŞLEMLERİ
        private void yeniOgrencibtn_Click(object sender, EventArgs e)
        {
            ogrencikayit yeniogrenci = new ogrencikayit();
            yeniogrenci.Show();
            Visible = false;
            yeniogrenci.FormClosed += yeniogrenci_FormClosed;
        }
        private void yeniogrenci_FormClosed(object sender, FormClosedEventArgs e)
        {
            Visible = true;
        }
        //--YENİ PERSONEL BUTONU İŞLEMLERİ
        private void yeniPersonelbtn_Click(object sender, EventArgs e)
        {
            personelgirisi yenipersonel = new personelgirisi();
            yenipersonel.Show();
            Visible = false;
            yenipersonel.FormClosed += yenipersonel_FormClosed;
        }
        void yenipersonel_FormClosed(object sender, FormClosedEventArgs e)
        {
            Visible = true;
        }
        //--İZİN ve İZİN RAPORU SAYFALARI İŞLEMLERİ
        private void button5_Click(object sender, EventArgs e)
        {
            İzin_Raporu yenirapor = new İzin_Raporu();
            yenirapor.Show();
            Visible = false;
            yenirapor.FormClosed += yenirapor_FormClosed;
        }
        void yenirapor_FormClosed(object sender, FormClosedEventArgs e)
        {
            Visible = true;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            izinformu yenizin = new izinformu();
            yenizin.Show();
            Visible = false;
            yenizin.FormClosed += yenizin_FormClosed;
        }
        void yenizin_FormClosed(object sender, FormClosedEventArgs e)
        {
            Visible = true;
        }
        //--PERSONEL EKLE/SİL/ARA/GÜNCELLE İŞLEMLERİ
        void personeldatacek()
        {
            con = new OleDbConnection("Provider=Microsoft.Ace.OleDb.12.0;Data Source=personel.accdb");
            da = new OleDbDataAdapter("Select * from personel", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "personel");
            dataGridView2.DataSource = ds.Tables["personel"];
            con.Close();
        }
        void personeldatasil()
        {
            if (MessageBox.Show("Seçili Ögeyi Silmek İstiyor Musunuz ?", "Dikkat!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                con = new OleDbConnection("Provider=Microsoft.Ace.Oledb.12.0;Data Source=personel.accdb");
                con.Open();
                cmd = new OleDbCommand();
                cmd.Connection = con;
                cmd.CommandText = "DELETE FROM personel WHERE tcno=@tcno";
                cmd.Parameters.AddWithValue("@tcno", dataGridView2.CurrentRow.Cells[6].Value.ToString());
                cmd.ExecuteReader();
                con.Close();
                MessageBox.Show("Silme İşlemi Başarılı!", "Silindi!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                personeldatacek();
            }
        }
        void personeldataara()
        {
            con = new OleDbConnection("Provider=Microsoft.Ace.Oledb.12.0;Data Source=personel.accdb");
            con.Open();
            dt = new DataTable();
            string arama = "SELECT * FROM personel WHERE adi+soyadi+tcno like '%" + textBox1.Text + "%'";
            da = new OleDbDataAdapter(arama, con);
            da.Fill(dt);
            con.Close();
            dataGridView2.DataSource = dt;
        }
        public void personeldataguncelle()
        {
            try
            {
                string tckimlik = dataGridView2.CurrentRow.Cells[6].Value.ToString();
                MessageBox.Show("Güncellenmek İstenen Kişinin TC Kimlik Numarası: " + tckimlik);
                personelguncelle persguncelle = new personelguncelle(tckimlik);
                persguncelle.Show();
                Visible = false;
                persguncelle.FormClosed += persguncelle_FormClosed;
            }

            catch (System.NullReferenceException)
            {
                MessageBox.Show("Lütfen Güncellemek İstediğiniz Kişinin Bulunduğu Satırı Seçiniz", "Dikkat!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }
        void persguncelle_FormClosed(object sender, FormClosedEventArgs e)
        {
            ogrencidatacek();
            Visible = true;
        }
        private void button11_Click(object sender, EventArgs e)
        {
            personeldataguncelle();
        }
        private void button8_Click(object sender, EventArgs e)
        {
            personeldatasil();
        }
        private void button9_Click(object sender, EventArgs e)
        {
            personeldatacek();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            personeldataara();
        }
        //--ÖĞRENCİ EKLE/SİL/ARA/GÜNCELLE İŞLEMLERİ
        void ogrencidatacek()
        {
            con = new OleDbConnection("Provider=Microsoft.Ace.OleDb.12.0;Data Source=ogrenci.accdb");
            da = new OleDbDataAdapter("Select * from ogrenci", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "ogrenci");
            dataGridView1.DataSource = ds.Tables["ogrenci"];
            con.Close();
        }
        void ogrencidatasil()
        {
            if (MessageBox.Show("Seçili Ögeyi Silmek İstiyor Musunuz ?", "Dikkat", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                con = new OleDbConnection("Provider=Microsoft.Ace.Oledb.12.0;Data Source=ogrenci.accdb");
                con.Open();
                cmd = new OleDbCommand();
                cmd.Connection = con;
                cmd.CommandText = "DELETE FROM ogrenci WHERE tckimlik=@tckimlik";
                cmd.Parameters.AddWithValue("@tckimlik", dataGridView1.CurrentRow.Cells[8].Value.ToString());
                cmd.ExecuteReader();
                con.Close();
                MessageBox.Show("Silme İşlemi Başarılı", "Silindi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ogrencidatacek();
            }
        }
        void ogrencidataara()
        {
            con = new OleDbConnection("Provider=Microsoft.Ace.Oledb.12.0;Data Source=ogrenci.accdb");
            con.Open();
            dt = new DataTable();
            string arama = "SELECT * FROM ogrenci WHERE tckimlik+adi+soyadi like '%" + textBox2.Text + "%'";
            da = new OleDbDataAdapter(arama, con);
            da.Fill(dt);
            con.Close();
            dataGridView1.DataSource = dt;
        }
        public void ogrencidataguncelle()
        {
            try
            {
                string tckimlik = dataGridView1.CurrentRow.Cells[8].Value.ToString();
                MessageBox.Show("Güncellenmek İstenen Kişinin TC Kimlik Numarası: " + tckimlik);
                ogrenciguncelle ogrguncelle = new ogrenciguncelle(tckimlik);
                ogrguncelle.Show();
                Visible = false;
                ogrguncelle.FormClosed +=ogrguncelle_FormClosed;
            }

            catch (System.NullReferenceException)
            {
                MessageBox.Show("Lütfen Güncellemek İstediğiniz Kişinin Bulunduğu Satırı Seçiniz", "Dikkat!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
        private void ogrguncelle_FormClosed(object sender, FormClosedEventArgs e)
        {
            ogrencidatacek();
            Visible = true;
        }
        private void button12_Click(object sender, EventArgs e)
        {
            ogrencidataguncelle();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            ogrencidatasil();
        }
        private void button7_Click(object sender, EventArgs e)
        {
            ogrencidatacek();
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            ogrencidataara();
        }
        //--ÇIKIŞ BUTONU İŞLEMLERİ
        private void yoneticicikisbtn_Click(object sender, EventArgs e)
        {
            Close();
        }
        //--SAAT FONKSİYONU
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

        private void button10_Click(object sender, EventArgs e)
        {
            yurtbilgileri yurtbilgi = new yurtbilgileri();
            yurtbilgi.Show();
        }
    }
}
