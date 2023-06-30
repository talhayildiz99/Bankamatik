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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-DSQNOEI\SQLEXPRESS03;Initial Catalog=DbBankamatik;Integrated Security=True");

        private void LnkKayitOl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form3 Frm = new Form3();
            Frm.Show();
        }

        private void BtnGiris_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * From TblKisiler Where HesapNo = @P1 and Sifre = @P2", baglanti);
            komut.Parameters.AddWithValue("@P1", MskHesapNo.Text);
            komut.Parameters.AddWithValue("@P2", TxtSifre.Text);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read()) 
            {
                Form2 frm = new Form2();
                frm.Hesap = MskHesapNo.Text;
                frm.Show();
            }
            else
            {
                MessageBox.Show("Hatalı Bilgi!");
            }
            baglanti.Close();
        }
    }
}
