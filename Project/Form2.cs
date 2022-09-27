using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Globalization;

namespace Project
{
    public partial class 
        Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            comboBox1.Items.Add("K");
            comboBox1.Items.Add("E");
        }

        SqlConnection baglanti = new SqlConnection("Data Source=LAPTOP-SPIDVNKL\\MSSQLSERVER077;Initial Catalog=ETRADE4;Integrated Security=True");


        private void Form2_Load(object sender, EventArgs e)
        {
          
            
        }


      
        private void button1_Click(object sender, EventArgs e) //VERİ GETİRME
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from USERS ", baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }
        private void button2_Click(object sender, EventArgs e)// KAYDET
        {

            int hataSayisi = HataKontrolu();
             
            if (hataSayisi == 0)
            {

            baglanti.Open();
            SqlCommand kayitekle = new SqlCommand("insert into USERS (USERNAME_,PASSWORD_,NAMESURNAME,EMAIL,GENDER,BIRTHDATE,CREATEDDATE,TELNR1,TELNR2) VALUES (@U1,@U2,@U3,@U4,@U5,@U6,@U7,@U8,@U9 )", baglanti);
            kayitekle.Parameters.AddWithValue("@U1", textBox1.Text);
            kayitekle.Parameters.AddWithValue("@U2", textBox4.Text);
            kayitekle.Parameters.AddWithValue("@U3", textBox3.Text);
            kayitekle.Parameters.AddWithValue("@U4", textBox2.Text);
            kayitekle.Parameters.AddWithValue("@U5", comboBox1.Text);
            kayitekle.Parameters.AddWithValue("@U6", Convert.ToDateTime(textBox6.Text));
            kayitekle.Parameters.AddWithValue("@U7", (DateTime.Now));
            kayitekle.Parameters.AddWithValue("@U8", textBox9.Text);
            kayitekle.Parameters.AddWithValue("@U9", textBox8.Text);

            kayitekle.ExecuteNonQuery();
            baglanti.Close();
            TextSil();
            

                MessageBox.Show("KAYIT BAŞARILI..");
            }
            else
            {
                MessageBox.Show("HATALI GİRİŞ !");
            }

        }
        private void button4_Click(object sender, EventArgs e) //GÜNCELLEME
        {
            int hataSayisi = HataKontrolu();

            if (hataSayisi == 0)
            {
                baglanti.Open();
                SqlCommand komutguncelle = new SqlCommand("Update USERS set USERNAME_=@U1,PASSWORD_=@U2,NAMESURNAME=@U3,EMAIL=@U4,GENDER=@U5,BIRTHDATE=@U6,TELNR1=@U8,TELNR2=@U9 where  USERNAME_ = @U1", baglanti);
                komutguncelle.Parameters.AddWithValue("@U1", textBox1.Text);
                komutguncelle.Parameters.AddWithValue("@U2", textBox4.Text);
                komutguncelle.Parameters.AddWithValue("@U3", textBox3.Text);
                komutguncelle.Parameters.AddWithValue("@U4", textBox2.Text);
                komutguncelle.Parameters.AddWithValue("@U5", comboBox1.Text);
                komutguncelle.Parameters.AddWithValue("@U6", textBox6.Text);
                komutguncelle.Parameters.AddWithValue("@U8", textBox9.Text);
                komutguncelle.Parameters.AddWithValue("@U9", textBox8.Text);
                komutguncelle.ExecuteNonQuery();
                baglanti.Close();
                TextSil();
                MessageBox.Show("GÜNCELLEME BAŞARILI.");
            }
            else
            {
                MessageBox.Show("GÜNCELLEME BAŞARISIZ !");
            }

        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e) //VERİLERİ DOLDURMA
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            DateTime dt = Convert.ToDateTime(dataGridView1.Rows[secilen].Cells[6].Value.ToString());
            string ad = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            string sifre = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            string adsoyad = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            string email = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            string cinsiyet = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            string dtrh = dt.ToString("yyyy.MM.dd");
            string otrh = dataGridView1.Rows[secilen].Cells[7].Value.ToString();
            string tn1 = dataGridView1.Rows[secilen].Cells[8].Value.ToString().Replace("(","").Replace(")", "");
            string tn2 = dataGridView1.Rows[secilen].Cells[9].Value.ToString().Replace("(", "").Replace(")", "");

            textBox1.Text = ad;
            textBox4.Text = sifre;
            textBox3.Text = adsoyad;
            textBox2.Text = email;
            comboBox1.SelectedText = cinsiyet;
            textBox6.Text = dtrh;
            textBox9.Text = tn1;
            textBox8.Text = tn2;
        }
        private void button3_Click(object sender, EventArgs e) //SİL
        {
            baglanti.Open();
            SqlCommand komusil = new SqlCommand("Delete from USERS where USERNAME_=@adi", baglanti);
            komusil.Parameters.AddWithValue("@adi", textBox1.Text);
            komusil.ExecuteNonQuery();
            baglanti.Close();
        }
        private void button5_Click(object sender, EventArgs e) //ARAMA
        {

            SqlDataAdapter da = new SqlDataAdapter(" select * from USERS where NAMESURNAME='" + textBox10.Text + "'", baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }
        private void button6_Click(object sender, EventArgs e) //KADINLARIN SAYISI
        {
            baglanti.Open();
            SqlCommand komugetir = new SqlCommand(" select count(ID) from USERS where GENDER ='K' ", baglanti);
            SqlDataReader oku = komugetir.ExecuteReader();
            while (oku.Read())
            {
                label12.Text = oku[0].ToString();
            }
            baglanti.Close();
        }
        private void button7_Click(object sender, EventArgs e)//TOPLAM KAYIT
        {
            baglanti.Open();
            SqlCommand komutgetir = new SqlCommand(" select count(ID) from USERS ", baglanti);
            SqlDataReader oku = komutgetir.ExecuteReader();
            while (oku.Read())
            {
                label13.Text = oku[0].ToString();
            }
            baglanti.Close();
        }
        private void button8_Click(object sender, EventArgs e)//YAŞI EN KÜÇÜK OLAN
        {
            baglanti.Open();
            SqlCommand komutgetir = new SqlCommand(" Select top 1 NAMESURNAME,DATEDIFF(yy, BIRTHDATE, GETDATE()) as 'Yaşı'From USERS order by DATEDIFF(yy, BIRTHDATE, GETDATE()) ", baglanti);
            SqlDataReader oku = komutgetir.ExecuteReader();
            while (oku.Read())
            {
                label15.Text = oku[0].ToString() + " " + oku[1].ToString() + " Yaşında";
            }
            baglanti.Close();
        }

        private void button9_Click(object sender, EventArgs e)//YAŞI EN BÜYÜK OLAN
        {
            baglanti.Open();
            SqlCommand komutgetir = new SqlCommand(" Select top 1 NAMESURNAME,DATEDIFF(yy, BIRTHDATE, GETDATE()) as 'Yaşı'From USERS order by DATEDIFF(yy, BIRTHDATE, GETDATE()) DESC ", baglanti);
            SqlDataReader oku = komutgetir.ExecuteReader();
            while (oku.Read())
            {
                label17.Text = oku[0].ToString() + " " + oku[1].ToString() + " Yaşında";
            }
            baglanti.Close();
        }
        private void TextSil()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox6.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            comboBox1.Text = "";
        }
        private int HataKontrolu()
        {
            int hataSayisi = 0;

            string metin = textBox1.Text;
            if (metin == "")
            {
                label11.Text = "* KULLANICI ADI BOŞ GEÇİLEMEZ !";
                hataSayisi++;
            }
            else
            {
                label11.Text = "";
            }


            string metin4 = textBox4.Text;
            if (metin4 == "")
            {
                label14.Text = "*ŞİFRE BOŞ GEÇİLEMEZ !";
                hataSayisi++;
            }
            else
            {
                label14.Text = "";
            }


            string metin3 = textBox3.Text;
            if (metin3 == "")
            {
                label16.Text = "*AD SOYAD BOŞ GEÇİLEMEZ !";
                hataSayisi++;
            }
            else
            {
                label16.Text = "";
            }


            string metin2 = textBox2.Text;
            if (metin2 == "")
            {
                label18.Text = " *EMAIL BOŞ GEÇİLEMEZ !";
                hataSayisi++;
            }
            else
            {
                label18.Text = "";
            }


            string metin6 = textBox6.Text;
            if (metin6 == "")
            {
                label20.Text = " *DOĞUM TARİHİ BOŞ GEÇİLEMEZ !";
                hataSayisi++;
            }
            else
            {
                label20.Text = "";
            }


            string metin09 = textBox9.Text;
            if (metin09 == "")
            {
                label21.Text = "* TELEFON NUMARASI 1 BOŞ GEÇİLEMEZ !";
                hataSayisi++;
            }
            else
            {
                label21.Text = "";
            }


            string metinCombo = comboBox1.Text;
            if (metinCombo == "")
            {
                label22.Text = " *CİNSİYET BOŞ GEÇİLEMEZ !";
                hataSayisi++;
            }
            else if (metinCombo != "K" && metinCombo != "E")
            {
                label22.Text = "CİNSİYETİ KONTROL EDİNİZ";
                hataSayisi++;
            }
            else
            {
                if (metinCombo.Length > 1)
                {
                    label22.Text = "GEÇERSİZ KARAKTER UZUNLUĞU !";
                    hataSayisi++;  // hataSayisi =hataSayisi+1;
                }
                else
                {
                    label22.Text = "";
                }                
            }

            

            string metin9 = textBox9.Text;
            if (metin9.Length > 10)
            {

                hataSayisi++;  // hataSayisi =hataSayisi+1;
                MessageBox.Show("TELEFON NUMARASINI BAŞINDA 0 KULLANMADAN DENEYİN !");
            }

            string metin8 = textBox8.Text;
            if (metin8.Length > 10)
            {

                hataSayisi++;  // hataSayisi =hataSayisi+1;
                MessageBox.Show("TELEFON NUMARASINI BAŞINDA 0 KULLANMADAN DENEYİNİZ !");
            }
            return hataSayisi;
        }

        public void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
      {
         
           
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {


            //string metin = textBox9.Text;
            //    if (metin.Length > 10)
            //    {
            //        errorProvider1.SetError(textBox9, "hyfgb ");
            //    }
            //    else if (metin.Length < 10)
            //    {
            //        errorProvider1.SetError(textBox9, "GEÇERSİZ KARAKTER UZUNLUĞU !");
            //    }

            //    else
            //    {
            //        errorProvider1.Clear();
            //    }

            //string metin9 = textBox9.Text;
            //if (metin9 == "")
            //{
            //    errorProvider1.SetError(textBox9, " TELEFON NUMARASI 1 BOŞ GEÇİLEMEZ");
            //    hataSayisi++;
            //}
            //else
            //{
            //    errorProvider1.Clear();
            //}
        }
        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            //string metin = textBox8.Text;
            //if (metin.Length > 10)
            //{
            //    errorProvider1.SetError(textBox8, "GEÇERSİZ KARAKTER UZUNLUĞU !");
            //}
            //else if (metin.Length < 10)
            //{
            //    errorProvider1.SetError(textBox8, "GEÇERSİZ KARAKTER UZUNLUĞU !");
            //}

            //else
            //{
            //    errorProvider1.Clear();
            //}
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
          
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
         
        }
        private void textBox6_TextChanged(object sender, EventArgs e)
       {
         
        }
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            
        }
        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }
          
        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
