using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace Sql_xml
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
   
       
       
        private void button1_Click(object sender, EventArgs e)
        {
            
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                using (SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-9VTPEG5\SQLEXPRESS;Initial Catalog=Okul;Integrated Security=True"))
                {
                    try
                    {
                        conn.Open();
                        Ogrenci ogrenci = new Ogrenci();
                        ogrenci.Numara = Convert.ToInt32(textBox1.Text);
                        SqlCommand cmd = new SqlCommand("SELECT * FROM TabloOgrenciler WHERE numara=@numara", conn);
                        cmd.Parameters.AddWithValue("@numara", ogrenci.Numara);

                        SqlDataReader reader = cmd.ExecuteReader();//SqlDataReader nesnesi, veritabanından gelen verileri satır satır okur.
                        DataTable dataTable = new DataTable();//ExecuteReadr() =SQL sorgusunu veritabanında çalıştırır ve sorgunun sonucu olan veri kümesini içeren bir SqlDataReader nesnesi döndürür
                        dataTable.Load(reader);

                        if (dataTable.Rows.Count > 0)
                        {
                            dataGridView1.Show();
                            dataGridView1.DataSource = dataTable;
                            MessageBox.Show("Öğrenci Bulundu.");

                            button3.Show();
                            button4.Show();

                            dataGridView1.Columns["numara"].ReadOnly = true;
                            dataGridView1.Columns["ad"].ReadOnly = true;
                            dataGridView1.Columns["soyad"].ReadOnly = true;
                            dataGridView1.Columns["dt"].ReadOnly = true;
                            dataGridView1.Columns["bolum"].ReadOnly = true;
                            dataGridView1.Columns["cinsiyet"].ReadOnly = true;
                        }
                        else
                        {
                            MessageBox.Show("Öğrenci Bulunamadı");
                        }
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show("Veri Tabanına Bağlanılamadı!!!" + ex.ToString());
                    }

                }//using bloğu sqlconn otomatik olarak kapatacaktır.
            }
            else
            {
                MessageBox.Show("Lütfen geçerli alanları doldurunuz!");
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            kayit f2 = new kayit();
            this.Hide();
            f2.Show();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {

           
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
         
            dataGridView1.Hide();
            button3.Hide();
            button4.Hide();
            label2.Hide();
            label3.Hide();
            label4.Hide();
            label5.Hide();
            label6.Hide();
            label7.Hide();
            txtad.Hide();
            txtnumara.Hide();
            txtsoyad.Hide();
            txtbolum.Hide();
            dateTimePicker1.Hide();
            radioButton1.Hide();
            radioButton2.Hide();
            button5.Hide();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
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
            string bolum =dataGridView1.Rows[seciliAlan].Cells[4].Value.ToString();
            string cinsiyet=dataGridView1.Rows[seciliAlan].Cells[5].Value.ToString();
            if (cinsiyet == "Erkek")
            {
                radioButton2.Checked = true;
            }
            else
            {
                radioButton1.Checked = true;
            }
            txtnumara.Text = numara.ToString();
            txtad.Text = ad;
            txtsoyad.Text = soyad;
            dateTimePicker1.Value = date;
            txtbolum.Text = bolum.ToString();

            label2.Show();
            label3.Show();
            label4.Show();
            label5.Show();
            label6.Show();
            label7.Show();
            txtad.Show();
            txtnumara.Show();
            txtsoyad.Show();
            txtbolum.Show();
            dateTimePicker1.Show();
            radioButton1.Show();
            radioButton2.Show();
            button5.Show();
            button4.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Tablonun üstüne tıklayınız!");
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            using (SqlConnection conn=new SqlConnection(@"Data Source=DESKTOP-9VTPEG5\SQLEXPRESS;Initial Catalog=Okul;Integrated Security=True"))
            {
                try
                {
                    conn.Open();
                   
                    SqlCommand cmd = new SqlCommand("DELETE FROM TabloOgrenciler WHERE numara=@numara",conn);
                    cmd.Parameters.AddWithValue("@numara",textBox1.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Silme işlemi başarılı!");
                    dataGridView1.Hide();
                    button3.Hide();
                    button4.Hide();
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Veri Tabanına Bağlanılamıyor!" + ex.ToString());
                }
            }
        }
        private void Yenile()
        {
            using (SqlConnection c = new SqlConnection(@"Data Source=DESKTOP-9VTPEG5\SQLEXPRESS;Initial Catalog=Okul;Integrated Security=True"))
            {
                try
                {
                    c.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM TabloOgrenciler where numara=@numara", c);
                    cmd.Parameters.AddWithValue("@numara",txtnumara.Text);

                    SqlDataReader read = cmd.ExecuteReader();
                   DataTable dt = new DataTable();
                   dt.Load(read);
                    dataGridView1.DataSource = dt;
                    
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Veri Tabanına Bağlanılamdı!" + ex.ToString());
                }
              

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(txtad.Text)&&!string.IsNullOrEmpty(txtnumara.Text)&&!string.IsNullOrEmpty(txtbolum.Text)&& (radioButton1.Checked || radioButton2.Checked) && dateTimePicker1.Value != DateTime.MinValue)
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

                        MessageBox.Show("Veri Tabanına Baplanılamadı!"+ex.ToString());
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen gerekli alanları doldurunuz!");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Listele l = new Listele();
            l.Show();
        }
    }
    
}
