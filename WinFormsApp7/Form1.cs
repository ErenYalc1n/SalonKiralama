using System.Data.SqlClient;

namespace WinFormsApp7
{
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-TNAKM3U;Initial Catalog=Toplanti;Integrated Security=True;TrustServerCertificate=True;");
        public Form1()
        {
            InitializeComponent();
            dateTimePicker1.MinDate = DateTime.Today;
        }           

        public void gor()
        {
            listView1.Items.Clear();
            SqlCommand gor = new SqlCommand("Select ad,soyad,salon,tarih,saat,ucret From kira", con);
            SqlDataReader rd = gor.ExecuteReader();
            while (rd.Read())
            {
                ListViewItem ekle = new ListViewItem();
                ekle.Text = rd[0].ToString();
                ekle.SubItems.Add(rd[1].ToString());
                ekle.SubItems.Add(rd[2].ToString());
                ekle.SubItems.Add(rd[3].ToString());
                ekle.SubItems.Add(rd[4].ToString());
                ekle.SubItems.Add(rd[5].ToString());

                listView1.Items.Add(ekle);
            }           
        }
        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            
            SqlCommand kontrol = new SqlCommand("Select salon,tarih,saat from kira where salon=@1 and tarih=@2 and saat=@3",con);
            kontrol.Parameters.AddWithValue("@1", comboBox1.Text);
            kontrol.Parameters.AddWithValue("@2", dateTimePicker1.Text);
            kontrol.Parameters.AddWithValue("@3", comboBox2.Text);
            SqlDataReader knt = kontrol.ExecuteReader();
            while(knt.Read())
            {
                label12.Text = knt[0].ToString();
                label13.Text = knt[1].ToString();
                label17.Text = knt[2].ToString();
            }
            knt.Close();
            if(comboBox1.Text==label12.Text && dateTimePicker1.Text==label13.Text && comboBox2.Text==label17.Text)
            {
                label16.Text = "Seçilen salon seçilen saat ve tarihte dolu!";
            }
            else
            {
                if (comboBox1.Text == "Seçiniz" || textBox1.Text == "" || textBox2.Text == "" || comboBox1.Text == "Seçiniz")
                {
                    label16.Text = "Tüm alanlarý doldurunuz!";
                }
                else
                {

                    SqlCommand kaydet = new SqlCommand("Insert Into kira Values (@1,@2,@3,@4,@5,@6)", con);
                    kaydet.Parameters.AddWithValue("@1", textBox1.Text);
                    kaydet.Parameters.AddWithValue("@2", textBox2.Text);
                    kaydet.Parameters.AddWithValue("@3", comboBox1.Text);
                    kaydet.Parameters.AddWithValue("@4", dateTimePicker1.Text);
                    kaydet.Parameters.AddWithValue("@5", comboBox2.Text);
                    kaydet.Parameters.AddWithValue("@6", label11.Text);
                    kaydet.ExecuteNonQuery();

                }
            }
            gor();
            con.Close();           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "")
            {
                label11.Text = "";
            }
            else if (comboBox1.Text == "Mavi Salon")
            {
                label4.Text = "6 Kiþilik";
                label5.Text = "ADSL";
                label11.Text = "1000 TL";
            }
            else if (comboBox1.Text == "Yeþil Salon")
            {
                label4.Text = "6 Kiþilik";
                label5.Text = "ADSL";
                label6.Text = "Projeksiyon";
                label11.Text = "1200 TL";
            }
            else if (comboBox1.Text == "Sarý Salon")
            {
                label4.Text = "8 Kiþilik";
                label5.Text = "ADSL";
                label6.Text = "Projeksiyon";
                label7.Text = "Yazýcý";
                label11.Text = "1500 TL";
            }
            else if (comboBox1.Text == "Beyaz Salon")
            {
                label4.Text = "10 Kiþilik";
                label5.Text = "ADSL";
                label6.Text = "Projeksiyon";
                label7.Text = "Yazýcý";
                label8.Text = "Tarayýcý";
                label11.Text = "2000 TL";
            }
        }
       

        private void button4_Click(object sender, EventArgs e)
        {
            con.Open();
            gor();
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand iptal = new SqlCommand("delete from kira where ad=@1 and soyad=@2",con);
            iptal.Parameters.AddWithValue("@1", textBox1.Text);
            iptal.Parameters.AddWithValue("@2", textBox2.Text);
            iptal.ExecuteNonQuery();
            gor();
            con.Close();
        }
    }
}