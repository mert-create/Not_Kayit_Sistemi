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
namespace Not_Kayıt_Sistemi
{
    public partial class FrmOgretmenDetay : Form
    {
        public FrmOgretmenDetay()
        {
            InitializeComponent();
        }
        SqlConnection bgl = new SqlConnection(@"Data Source=MSI\SQLEXPRESS;Initial Catalog=DbNotKayıt;Integrated Security=True;");

        private void FrmOgretmenDetay_Load(object sender, EventArgs e)
        {
            // TODO: Bu kod satırı 'dbNotKayıtDataSet.TBLDERS' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.tBLDERSTableAdapter.Fill(this.dbNotKayıtDataSet.TBLDERS);

        }

        private void BtnOgrenciKaydet_Click(object sender, EventArgs e)
        {
            bgl.Open();

            SqlCommand komut = new SqlCommand("INSERT INTO TBLDERS (OGRNUMARA,OGRAD,OGRSOYAD) VALUES (@p1,@p2,@p3)", bgl);
            komut.Parameters.AddWithValue("@p1", MskNumara.Text);
            komut.Parameters.AddWithValue("@p2", TxtAd.Text);
            komut.Parameters.AddWithValue("@p3", TxtSoyad.Text);
            komut.ExecuteNonQuery();
            bgl.Close();
            MessageBox.Show("Öğrenci Sisteme Eklendi");
            this.tBLDERSTableAdapter.Fill(this.dbNotKayıtDataSet.TBLDERS);

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secılen=dataGridView1.SelectedCells[0].RowIndex;

            MskNumara.Text = dataGridView1.Rows[secılen].Cells[1].Value.ToString();
            TxtAd.Text = dataGridView1.Rows[secılen].Cells[2].Value.ToString();
            TxtSoyad.Text = dataGridView1.Rows[secılen].Cells[3].Value.ToString();
            TxtSınav1.Text = dataGridView1.Rows[secılen].Cells[4].Value.ToString();
            TxtSınav2.Text = dataGridView1.Rows[secılen].Cells[5].Value.ToString();
            TxtSınav3.Text = dataGridView1.Rows[secılen].Cells[6].Value.ToString();

        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            double ortalama, s1, s2,s3;
            string durum;
            s1 = Convert.ToDouble(TxtSınav1.Text);
            s2 = Convert.ToDouble(TxtSınav2.Text);
            s3 = Convert.ToDouble(TxtSınav3.Text);

            ortalama = (s1 + s2 + s3) / 3;
            LblOrtalama.Text=ortalama.ToString();


            if (ortalama >= 50)
            {
                durum = "true";
            }
            else
            {
                durum = "false";
            }

            bgl.Open();
            SqlCommand komut = new SqlCommand("UPDATE TBLDERS SET OGRS1=@p1,OGRS2=@p2,OGRS3=@p3,ORTALAMA=@p4,DURUM=@p5 WHERE OGRNUMARA=@p6",bgl);
            komut.Parameters.AddWithValue("@p1",TxtSınav1.Text);
            komut.Parameters.AddWithValue("@p2",TxtSınav2.Text);
            komut.Parameters.AddWithValue("@p3",TxtSınav3.Text);
            komut.Parameters.AddWithValue("@p4",decimal.Parse(LblOrtalama.Text));
            komut.Parameters.AddWithValue("@p5",durum);
            komut.Parameters.AddWithValue("@p6",MskNumara.Text);
            komut.ExecuteNonQuery();
            bgl.Close();
            MessageBox.Show("Öğrenci Notları Güncellendi");
            this.tBLDERSTableAdapter.Fill(this.dbNotKayıtDataSet.TBLDERS);
        }
    }
}
