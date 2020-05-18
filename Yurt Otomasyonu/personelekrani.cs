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
    public partial class personelekrani : Form
    {
        Timer t = new Timer();
        OleDbConnection con;
        OleDbCommand cmd;
        OleDbDataAdapter da;
        DataTable dt;
        DataSet ds;
        public personelekrani()
        {
            InitializeComponent();
        }
        private void personelekrani_Load(object sender, EventArgs e)
        {
            //--Saat
            t.Interval = 1000;
            t.Tick += new EventHandler(this.saat);
            t.Start();
            //--Fonkisiyonlar
            ogrencidatacek();
            label1.Text = "Personel Ekranı";
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
        //--YENİ ÖĞRENCİ BUTONU İŞLEMLERİ
        private void button1_Click(object sender, EventArgs e)
        {
            ogrencikayit yeniogrenci = new ogrencikayit();
            yeniogrenci.Show();
            Visible = false;
            yeniogrenci.FormClosed += yeniogrenci_FormClosed;
        }
        void yeniogrenci_FormClosed(object sender, FormClosedEventArgs e)
        {
            Visible = true;
        }
        //--İZİN SAYFASI İŞLEMLERİ
        private void button3_Click(object sender, EventArgs e)
        {
            izinformu yeniizin = new izinformu();
            yeniizin.Show();
            Visible = false;
            yeniizin.FormClosed += yeniizin_FormClosed;
        }
        void yeniizin_FormClosed(object sender, FormClosedEventArgs e)
        {
            Visible = true;
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
                ogrguncelle.FormClosed += ogrguncelle_FormClosed;
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
