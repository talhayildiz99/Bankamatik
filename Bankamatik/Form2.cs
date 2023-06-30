using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bankamatik
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-DSQNOEI\SQLEXPRESS03;Initial Catalog=DbBankamatik;Integrated Security=True");


        public string Hesap;
        private void BtnGonder_Click(object sender, EventArgs e)
        {

            //Gönderilen Hesabın Para Artışı
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Update TblHesap set Bakiye = Bakiye + @P1 where HesapNo = @P2", baglanti);
            komut.Parameters.AddWithValue("@P1", decimal.Parse(TxtTutar.Text));
            komut.Parameters.AddWithValue("@P2", MskHesapNo.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();

            //Gönderen Hesabın Para Azalışı
            baglanti.Open();
            SqlCommand komut2 = new SqlCommand("Update TblHesap set Bakiye = Bakiye - @K1 where HesapNo = @K2", baglanti);
            komut2.Parameters.AddWithValue("@K1", decimal.Parse(TxtTutar.Text));
            komut2.Parameters.AddWithValue("@K2", Hesap);
            komut2.ExecuteNonQuery();
            baglanti.Close();

            //Hareket Tablosu
            baglanti.Open();
            SqlCommand komut3 = new SqlCommand("insert into TblHareket (Gonderen, Alici, Tutar) values (@T1,@T2,@T3)", baglanti);
            komut3.Parameters.AddWithValue("@t1", LblHesapNo.Text);
            komut3.Parameters.AddWithValue("@t2", MskHesapNo.Text);
            komut3.Parameters.AddWithValue("@t3", decimal.Parse(TxtTutar.Text));
            komut3.ExecuteNonQuery();
            MessageBox.Show("İşlem Gerçekleşti!");
            baglanti.Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            LblHesapNo.Text = Hesap;

            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * From TblKisiler Where HesapNo = @P1", baglanti);
            komut.Parameters.AddWithValue("@P1", Hesap);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                LblAdSoyad.Text = dr[1] + " " + dr[2];
                LblTc.Text = dr[3].ToString();
                LblTelefon.Text = dr[4].ToString();
            }
            baglanti.Close();
        }

        private void BtnHesapHareketleri_Click(object sender, EventArgs e)
        {
            Form4 frm= new Form4();
            frm.Show();
        }
    }
}
