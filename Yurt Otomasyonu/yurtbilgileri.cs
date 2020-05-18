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
    public partial class yurtbilgileri : Form
    {
        OleDbConnection con;
        OleDbCommand cmd;
        OleDbDataReader dr;
        public yurtbilgileri()
        {
            InitializeComponent();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string baglanti = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=personel.accdb;Persist Security Info=True";
            con = new OleDbConnection(baglanti);
            con.Open();
            string ekle = "insert into yurtbilgi(yadi,yadresi,ytelefon) values (@yadi,@yadresi,@ytelefon)";
            cmd = new OleDbCommand(ekle, con);
            if (textBox1.Text == "" || textBox2.Text == "" || maskedTextBox1.Text == "")
            {
                MessageBox.Show("Lütfen TÜM Alanları Doldurunuz!!!!!");
                con.Close();
            }
            else
            {
                cmd.Parameters.AddWithValue("@yadi", textBox1.Text);
                cmd.Parameters.AddWithValue("@yadresi", textBox2.Text);
                cmd.Parameters.AddWithValue("@ytelefon", maskedTextBox1.Text);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            con.Close();
            Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            
            cmd = new OleDbCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "UPDATE yurtbilgi SET yadi=@yadi,yadresi=@yadresi,ytelefon=@ytelefon WHERE yadi=@yadi";
            cmd.Parameters.AddWithValue("@yadi", textBox1.Text);
            cmd.Parameters.AddWithValue("@yadresi", textBox2.Text);
            cmd.Parameters.AddWithValue("@ytelefon", maskedTextBox1.Text);
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            con.Close();
            {
                MessageBox.Show("Güncelleme Tamamlandı!", "Veri Tabanına Kayıt İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                con.Close();
            }
            datacek();
            Close();
        }
        void kydtbtngizle()
        {
            con = new OleDbConnection("Provider=Microsoft.Ace.OleDb.12.0;Data Source=personel.accdb");
            cmd = new OleDbCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "SELECT COUNT(*) FROM yurtbilgi";
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read() == true)
                {
                    if (Convert.ToString(dr[0]) == "1")
                    {
                        button2.Visible = false;
                    }
                    else
                    {
                        button2.Visible = true;
                    }
                }
            }
        }
        void datacek()
        {
            con = new OleDbConnection("provider=Microsoft.Ace.Oledb.12.0;Data Source=personel.accdb");
            con.Open();
            cmd = new OleDbCommand("SELECT yadi,yadresi,ytelefon FROM yurtbilgi", con);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                textBox1.Text = dr["yadi"].ToString();
                textBox2.Text = dr["yadresi"].ToString();
                maskedTextBox1.Text = dr["ytelefon"].ToString();
            }
            con.Close();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void yurtbilgileri_Load(object sender, EventArgs e)
        {
            datacek();
            kydtbtngizle();
        }
    }
}
