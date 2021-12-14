using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;
using MD5Hash;

namespace PasswordEncryptation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DB.Data d;
        public MainWindow()
        {
            InitializeComponent();
            d = new DB.Data("DESKTOP-644QAJI", "Evaluacion4", "sa", "casita123");
        }

        private void MD5Button_Click(object sender, RoutedEventArgs e)
        {

            string encrypt = TextToEncrypt.Text;
            EncryptedText.Text = MD5Hash.Hash.Content(encrypt);

            SaveValues();
        }

        private void SHA1Button_Click(object sender, RoutedEventArgs e)
        {
            string encrypt = TextToEncrypt.Text;

            SHA1 sha1 = SHA1CryptoServiceProvider.Create();
            Byte[] OGText = ASCIIEncoding.Default.GetBytes(encrypt);
            Byte[] hash = sha1.ComputeHash(OGText);
            StringBuilder textstring = new StringBuilder();
            foreach (byte i in hash)
            {
                textstring.AppendFormat("{0:x2}", i);
            }
            EncryptedText.Text = textstring.ToString();

            SaveValues();


        }

        private void SHA2Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AESButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DecryptButton_Click(object sender, RoutedEventArgs e)
        {

            SqlConnectionStringBuilder scsb = new SqlConnectionStringBuilder();
            scsb.DataSource = "DESKTOP-644QAJI";
            scsb.InitialCatalog = "ProyectoAzure";
            scsb.UserID = "sa";
            scsb.Password = "casita123";

            SqlConnection sqlConnection = new SqlConnection(scsb.ConnectionString);
            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand("SELECT TEXTO FROM TEXTOENCRIPTADO WHERE ENCRIPCION = @Encripcion", sqlConnection);

            sqlCommand.Parameters.Add("@Encripcion", SqlDbType.VarChar).Value = TextToEncrypt.Text;

            var response = sqlCommand.ExecuteScalar();

            EncryptedText.Text = Convert.ToString(sqlCommand.ExecuteScalar());
            sqlConnection.Close();

        }



        private void SaveValues()
        {
            SqlConnectionStringBuilder scsb = new SqlConnectionStringBuilder();
            scsb.DataSource = "DESKTOP-644QAJI";
            scsb.InitialCatalog = "ProyectoAzure";
            scsb.UserID = "sa";
            scsb.Password = "casita123";

            SqlConnection sqlConnection = new SqlConnection(scsb.ConnectionString);
            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand("INSERT INTO TextoEncriptado VALUES (@Texto, @Encripcion)", sqlConnection);

            sqlCommand.Parameters.Add("@Texto", SqlDbType.VarChar).Value = TextToEncrypt.Text;
            sqlCommand.Parameters.Add("@Encripcion", SqlDbType.VarChar).Value = EncryptedText.Text;

            var response = sqlCommand.ExecuteNonQuery();

            sqlConnection.Close();
        }
    }
}
