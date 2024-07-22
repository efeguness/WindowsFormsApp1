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

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        SqlConnection baglan = new SqlConnection("Data Source=B1-16_42PC12279\\SQLEXPRESS01;Initial Catalog=deneme;Integrated Security=True");

        int hak = 0;
        int toplamhak = 3;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Kullanıcı Girişi";
            this.AcceptButton = button1;
            this.CancelButton = button2;
            radioButton1.Checked = true;
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;

            Random rnd = new Random();

            int harfdegeri1, harfdegeri2,sayi;
            harfdegeri1 = rnd.Next(65, 91);
            harfdegeri2 = rnd.Next(65, 91);
            sayi = rnd.Next(100, 1000);

            char karakter1, karakter2;
            karakter1 = Convert.ToChar(harfdegeri1);
            karakter2= Convert.ToChar(harfdegeri2);
            label4.Text = karakter1.ToString() + karakter2.ToString() + sayi.ToString();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            KullaniciGirisKayitSayfasi yeni = new KullaniciGirisKayitSayfasi();
            yeni.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglan.Open();
            SqlCommand komut = new SqlCommand("Select * from KayitTablosu where k_giris=@1 and sifre=@2",baglan);
            komut.Parameters.AddWithValue("@1", textBox1.Text);
            komut.Parameters.AddWithValue("@2", textBox2.Text);
            SqlDataReader oku = komut.ExecuteReader();

            if(radioButton1.Checked==true)
            {
                //Bu seçiliyse yönetici olarak giriş yapılacak.

                if (oku.Read())
                {
                    //Bilgiler doğruysa Giriş Yap.
                    if (oku["yetki"].ToString() == "Yonetici")
                    {
                        //Giriş yapabilir.

                        if(label4.Text == textBox3.Text)
                        {
                            MessageBox.Show("Başarıyla Giriş Yaptınız!", "Tebrikler!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            YoneticiSayisi yeni = new YoneticiSayisi();
                            yeni.Show();
                            this.Hide();
                        }

                        else
                        {
                            MessageBox.Show("Güvenlik Kodu Hatalı!");
                        }
                    }

                    else
                    {
                        MessageBox.Show("Siz Yönetici Değilsiniz!");
                    }
                }

                else
                {
                    //Bilgiler yanlışsa Hata Mesajı Ver.
                    hak = hak + 1;
                    MessageBox.Show("Hatalı Şifre Girdiniz! " +(toplamhak-hak)+ " hakkınız kaldı.");
                    
                        if(hak==3)
                       {
                        MessageBox.Show("Tekrar Deneme Şansınız Kalmamıştır!");
                        button1.Enabled = false;
                       }
                }

            }




            if (radioButton2.Checked == true)
            {
                //Bu seçiliyse kullanıcı olarak giriş yapılacak.

                if (oku.Read())
                {
                    //Bilgiler doğruysa Giriş Yap.
                    if (oku["yetki"].ToString() == "Kullanici")
                    {
                        //Giriş yapabilir.

                        if (label4.Text == textBox3.Text)
                        {
                            MessageBox.Show("Başarıyla Giriş Yaptınız!", "Tebrikler!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            KullaniciSayfasi yeni = new KullaniciSayfasi();
                            yeni.Show();
                            this.Hide();
                        }

                        else
                        {
                            MessageBox.Show("Güvenlik Kodu Hatalı!");
                        }
                    }

                    else
                    {
                        MessageBox.Show("Siz Kullanıcı Değilsiniz!");
                    }
                }

                else
                {
                    //Bilgiler yanlışsa Hata Mesajı Ver.
                    hak = hak + 1;
                    MessageBox.Show("Hatalı Şifre Girdiniz! " + (toplamhak - hak) + " hakkınız kaldı.");

                    if (hak == 3)
                    {
                        MessageBox.Show("Tekrar Deneme Şansınız Kalmamıştır!");
                        button1.Enabled = false;
                    }
                }

            }




            baglan.Close();



        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            textBox3.Text = textBox3.Text.ToUpper();
        }
    }
}
