using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace UdemySifreliVeriler
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-5HVC58C\SQLEXPRESS;Initial Catalog=Test;Integrated Security=True");


        void Listele()
        {
            SqlDataAdapter da = new SqlDataAdapter("select*from Veriler", baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                row["Ad"] = Coz(row["Ad"] as string);
                row["Soyad"] = Coz(row["Soyad"] as string);
                row["Mail"] = Coz(row["Mail"] as string);
                row["Sifre"] = Coz(row["Sifre"] as string);
                row["HesapNo"] = Coz(row["HesapNo"] as string);
                
            }

            dataGridView1.DataSource = ds.Tables[0];
        }


        private object Coz(string data)
        {
            byte[] cozumdizi = Convert.FromBase64String(data);
            string verisi = ASCIIEncoding.ASCII.GetString(cozumdizi);
            return verisi;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string ad = txtad.Text;
            byte[] adDizi = ASCIIEncoding.ASCII.GetBytes(ad); //metnin her karakterinin ascıı kodunu aldı
            string sifread = Convert.ToBase64String(adDizi);



            string soyad = txtsoyad.Text;
            byte[] soyadDizi = ASCIIEncoding.ASCII.GetBytes(soyad); 
            string Soyadsifre = Convert.ToBase64String(soyadDizi);
            
            
            string mail = txtmail.Text;
            byte[] mailDizi = ASCIIEncoding.ASCII.GetBytes(mail); 
            string mailsifre = Convert.ToBase64String(mailDizi);


            string sifre = textBox4.Text;
            byte[] sifreDizi = ASCIIEncoding.ASCII.GetBytes(sifre); 
            string sifresifre = Convert.ToBase64String(sifreDizi);
            
            
            string hesapNo = textBox5.Text;
            byte[] hesapNoDizi = ASCIIEncoding.ASCII.GetBytes(hesapNo); 
            string hesapNosifre = Convert.ToBase64String(hesapNoDizi);

            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into Veriler (Ad,Soyad,Mail,Sifre,HesapNo) values (@p1,@p2,@p3,@p4,@p5)", baglanti);
            komut.Parameters.AddWithValue("@p1", sifread);
            komut.Parameters.AddWithValue("@p2", Soyadsifre);
            komut.Parameters.AddWithValue("@p3", mailsifre);
            komut.Parameters.AddWithValue("@p4", sifresifre);
            komut.Parameters.AddWithValue("@p5", hesapNosifre);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Veriler Eklendi");


        }

        private void button2_Click(object sender, EventArgs e)
        {
            Listele();
        }
    }
}
