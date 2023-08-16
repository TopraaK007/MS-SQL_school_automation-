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

namespace Sql_xml
{
    public partial class Listele : Form
    {
        public Listele()
        {
            InitializeComponent();
        }
        public void Yenile()
        {
            using (SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-9VTPEG5\SQLEXPRESS;Initial Catalog=Okul;Integrated Security=True"))

            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM TabloOgrenciler", conn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr);
                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns["numara"].ReadOnly = true;
                    dataGridView1.Columns["ad"].ReadOnly = true;
                    dataGridView1.Columns["soyad"].ReadOnly = true;
                    dataGridView1.Columns["dt"].ReadOnly = true;
                    dataGridView1.Columns["bolum"].ReadOnly = true;
                    dataGridView1.Columns["cinsiyet"].ReadOnly = true;

                }
                catch (Exception ex)
                {

                    MessageBox.Show("Veri Tabanı Bağlantısı Yapılamıyor!" + ex.ToString());
                }
            }
        }
        private void Listele_Load(object sender, EventArgs e)
        {
           Yenile();
           ogrenciSayisiGoster();

        }
        
   
        

        private void Listele_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int seciliAlan = dataGridView1.SelectedCells[0].RowIndex;
            int numara = Convert.ToInt32(dataGridView1.Rows[seciliAlan].Cells[0].Value);
            string ad = dataGridView1.Rows[seciliAlan].Cells[1].Value.ToString();
            string soyad = dataGridView1.Rows[seciliAlan].Cells[2].Value.ToString();
            string dt = dataGridView1.Rows[seciliAlan].Cells[3].Value.ToString();
            DateTime.TryParse(dt, out DateTime date);
            string bolum = dataGridView1.Rows[seciliAlan].Cells[4].Value.ToString();
            string cinsiyet = dataGridView1.Rows[seciliAlan].Cells[5].Value.ToString();
            if (cinsiyet == "Erkek")
            {
                radioButton2.Checked = true;
            }
            else
            {
                radioButton1.Checked = true;
            }
            txtnumara.Text=numara.ToString();
            txtad.Text = ad;
            txtsoyad.Text = soyad;
            dateTimePicker1.Value=date;
            txtbolum.Text = bolum;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form = new Form1();
            form.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-9VTPEG5\SQLEXPRESS;Initial Catalog=Okul;Integrated Security=True"))
            {
                try
                {
                    string cinsiyet;

                    if (radioButton1.Checked == true)
                    {
                        cinsiyet = "Kız";
                    }
                    else
                    {
                        cinsiyet = "Erkek";
                    }

                    if (!string.IsNullOrEmpty(txtad.Text) && !string.IsNullOrEmpty(txtnumara.Text) && !string.IsNullOrEmpty(txtbolum.Text) && !string.IsNullOrEmpty(cinsiyet) && dateTimePicker1.Value != DateTime.MinValue)
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("DELETE FROM TabloOgrenciler WHERE numara=@numara", conn);
                        cmd.Parameters.AddWithValue("@numara", txtnumara.Text);
                        cmd.ExecuteNonQuery();
                        int secilialan = dataGridView1.SelectedCells[0].RowIndex;
                        dataGridView1.Rows.RemoveAt(secilialan);
                        txtad.Text = "";
                        txtsoyad.Text = "";
                        txtnumara.Text = "";
                        txtbolum.Text = "";
                        radioButton1.Checked = false;
                        radioButton2.Checked = false;
                        dateTimePicker1.Value = DateTime.Now;
                        ogrenciSayisiGoster();
                        
                    }
                    else
                    {
                        MessageBox.Show("Lütfen gerekli alanları doldurunuz!");
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Veri Tabanı Bağlantısı Yapılamıyor!" + ex.ToString());
                }
            }
        }
        string cinsiyet = string.Empty;

        public void ogrenciSayisiGoster()
        {
            using (SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-9VTPEG5\SQLEXPRESS;Initial Catalog=Okul;Integrated Security=True"))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM TabloOgrenciler", conn);
                int sayi = (int)cmd.ExecuteScalar();
                label8.Text = sayi.ToString();

            }
            
        
        }
        private void button2_Click(object sender, EventArgs e)
        {
            
            if (!string.IsNullOrEmpty(txtad.Text) && !string.IsNullOrEmpty(txtnumara.Text) && !string.IsNullOrEmpty(txtbolum.Text) && (radioButton1.Checked || radioButton2.Checked) && dateTimePicker1.Value != DateTime.MinValue)
            {

                using (SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-9VTPEG5\SQLEXPRESS;Initial Catalog=Okul;Integrated Security=True"))
                {
                    try
                    {
                        string cinsiyet;
                        if (radioButton1.Checked)
                        {
                            cinsiyet = "Kız";

                        }
                        else
                        {
                            cinsiyet = "Erkek";
                        }
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("UPDATE TabloOgrenciler SET ad=@ad, soyad=@soyad, dt=@dt, bolum=@bolum, cinsiyet=@cinsiyet WHERE numara=@numara", conn);
                        cmd.Parameters.AddWithValue("@ad", txtad.Text);
                        cmd.Parameters.AddWithValue("@soyad", txtsoyad.Text);
                        cmd.Parameters.AddWithValue("@dt", dateTimePicker1.Value);
                        cmd.Parameters.AddWithValue("@bolum", txtbolum.Text);
                        cmd.Parameters.AddWithValue("@cinsiyet", cinsiyet);
                        cmd.Parameters.AddWithValue("@numara", txtnumara.Text);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Güncelleme Başarılı!");
                        Yenile();
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show("Veri Tabanına Baplanılamadı!" + ex.ToString());
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen gerekli alanları doldurunuz!");
            }
        }
    }
}
