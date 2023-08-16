using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sql_xml
{
    public partial class kayit : Form
    {
        public kayit()
        {
            InitializeComponent();
        }

        private void kayit_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        string Cinsiyet = string.Empty;

       

        private void button1_Click(object sender, EventArgs e)
        { 
            if(Kontrol())
            {
                ogrenciEkle();
                
            }
            
            
        }

        private void radiobutton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radiobutton1.Checked)
            {
                Cinsiyet = "Kız";
            }
            else
            {
                Cinsiyet = "Erkek";
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radiobutton1.Checked)
            {
                Cinsiyet = "Kız";
            }
            else
            {
                Cinsiyet = "Erkek";
            }
        }
        

        
        
        public void ogrenciEkle()
        {

            Ogrenci yeni = new Ogrenci(Convert.ToInt32(txtNumara.Text), txtAd.Text, txtSoyad.Text, dateTimePicker1.Value, txtBolum.Text, Cinsiyet);
            SqlConnection conn = null;

            try
            {

                {
                    conn = new SqlConnection(@"Data Source=DESKTOP-9VTPEG5\SQLEXPRESS;Initial Catalog=Okul;Integrated Security=True");
                    conn.Open();

                    SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM TabloOgrenciler where numara='" + yeni.Numara + "'", conn);
                    int ogrenciNumara = (int)command.ExecuteScalar();

                    if (ogrenciNumara > 0)
                    {
                        MessageBox.Show(yeni.Numara + " sahip öğrenci zaten mevcut başka bir numara giriniz!!");
                    }

                    else
                    {

                        SqlCommand cmd = new SqlCommand("INSERT INTO TabloOgrenciler (numara,ad,soyad,dt,bolum,cinsiyet)" + "VALUES(@numara,@ad,@soyad,@dt,@bolum,@cinsiyet)", conn);

                        cmd.Parameters.AddWithValue("@numara", yeni.Numara);
                        cmd.Parameters.AddWithValue("@ad", yeni.Ad);
                        cmd.Parameters.AddWithValue("@soyad", yeni.Soyad);
                        cmd.Parameters.AddWithValue("@dt", yeni.DateTime);
                        cmd.Parameters.AddWithValue("@bolum", yeni.Bolum);
                        cmd.Parameters.AddWithValue("@cinsiyet", yeni.Cinsiyet);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Öğrenci Başarıyla Eklendi!");

                        Listele l =new Listele();
                        l.ogrenciSayisiGoster();
                        Temizle();
                    }

                }
            }
            
            catch (Exception ex)
            {

                MessageBox.Show("Veri Tabanına Bağlanılamadı!" + ex.ToString());
            }
            finally
            {
                if (conn != null) conn.Close();
            }
            
        }
        private void Temizle()
        {
            txtAd.Text = "";
            txtBolum.Text = "";
            txtNumara.Text = "";
            txtSoyad.Text = "";
            radiobutton1.Checked = false;
            radioButton2.Checked = false;
            dateTimePicker1.Value = DateTime.Now;
        }
        public bool Kontrol()
        {
            if (!string.IsNullOrEmpty(txtAd.Text) && !string.IsNullOrEmpty(txtNumara.Text) && !string.IsNullOrEmpty(txtBolum.Text) && !string.IsNullOrEmpty(Cinsiyet) && dateTimePicker1.Value != DateTime.MinValue)
            { 
                return true; 
            }
            else
            {
                MessageBox.Show("Lütfen gerekli alanları doldurunuz!!");
                return false;
            }
        }

        private void txtNumara_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
        
            Form1 form = new Form1();
            this.Hide();
            form.Show();

        }
    }
}
