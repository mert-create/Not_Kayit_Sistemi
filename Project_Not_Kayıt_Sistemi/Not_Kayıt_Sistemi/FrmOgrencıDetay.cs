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
    public partial class FrmOgrencıDetay : Form
    {
        public FrmOgrencıDetay()
        {
            InitializeComponent();
        }
        public string numara;

        SqlConnection bgl =new SqlConnection(@"Data Source=MSI\SQLEXPRESS;Initial Catalog=DbNotKayıt;Integrated Security=True;");
        private void FrmOgrencıDetay_Load(object sender, EventArgs e)
        {
            LblNumara.Text = numara;

            bgl.Open();
            SqlCommand komut = new SqlCommand("SELECT * FROM TBLDERS WHERE OGRNUMARA=@p1",bgl);
            komut.Parameters.AddWithValue("@p1", numara);
            SqlDataReader dr = komut.ExecuteReader();
            while(dr.Read())
            {
                LblAdSoyad.Text = dr[2].ToString() +" "+ dr[3].ToString();
                LblSınav1.Text = dr[4].ToString();
                LblSınav2.Text = dr[5].ToString();
                LblSınav3.Text = dr[6].ToString();
                LblOrtalama.Text = dr[7].ToString();
                LblDurum.Text = dr[8].ToString();
            }
            bgl.Close();
        }
    }
}
