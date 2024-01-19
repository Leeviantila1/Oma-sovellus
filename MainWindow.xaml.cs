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
using Microsoft.Data.SqlClient;
using System.Data;

namespace Oma_sovellus
{
    public partial class MainWindow : Window
    {
        private string solun_arvo;

        string polku = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\k2002137\\Documents\\Tietokanta.mdf;Integrated Security=True;Connect Timeout=30";

        Tietokantatoiminnot tkt;

        public MainWindow()
        {
            InitializeComponent();
            tkt = new Tietokantatoiminnot();

            tkt.paivitaComboBox(combo_autot, combo_autot2);
            tkt.paivitaComboBox2(combo_ukot, combo_ukot2);
            //paivitaAsiakasComboBox();



            tkt.paivitaDataGrid("SELECT * FROM autot", "autot", huollossa_olevat_autot);
            tkt.paivitaDataGrid("SELECT * FROM työntekijät", "työntekijät", tyontekijat_taulu);
            //tkt.paivitaDataGrid("SELECT * FROM työnjako", "työnjako", työnjako_lista);
            tkt.paivitaDataGrid("SELECT tyo.id_työnjako AS id_työnjako, ty.työnumero AS työntekijä, a.rekisterinumero AS rekisterinumero  FROM työntekijät ty, autot a, työnjako tyo WHERE a.rekisterinumero=tyo.rekisterinumero AND ty.työnumero=tyo.työntekijä ", "työnjako", työnjako_lista);
            // tkt.paivitaDataGrid("SELECT ti.id AS id, a.nimi AS asiakas, tu.nimi AS tuote, ti.toimitettu AS toimitettu  FROM tilaukset ti, asiakkaat a, tuotteet tu WHERE a.id=ti.asiakas_id AND tu.id=ti.tuote_id AND ti.toimitettu='0'", "tilaukset", tilaukset_lista);
            //tkt.paivitaDataGrid("SELECT ti.id AS id, a.nimi AS asiakas, tu.nimi AS tuote, ti.toimitettu AS toimitettu  FROM tilaukset ti, asiakkaat a, tuotteet tu WHERE a.id=ti.asiakas_id AND tu.id=ti.tuote_id AND ti.toimitettu ='1'", "toimitetut", toimitetut_lista);

        }
        
        // painike lisää auto
        private void painike_lisaa_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection kanta = new SqlConnection(polku);
            kanta.Open();

            string sql = "INSERT INTO autot (merkki, rekisterinumero, miksi_huollossa) VALUES ('" + auto_merk.Text + "','" + auto_reknum.Text + "','" + miksi_huoll.Text + "')";

            SqlCommand komento = new SqlCommand(sql, kanta);
            komento.ExecuteNonQuery();

            kanta.Close();

            tkt.paivitaDataGrid("SELECT * FROM autot", "autot", huollossa_olevat_autot);
            tkt.paivitaComboBox(combo_autot, combo_autot2);
        }

        // painike lisää työukko
        private void painike_lisaa_ukko(object sender, RoutedEventArgs e)
        {
            SqlConnection kanta = new SqlConnection(polku);
            kanta.Open();

            string sql = "INSERT INTO työntekijät (nimi, työnumero) VALUES ('" + ukko_nimi.Text + "','" + ukko_numero.Text + "')";

            SqlCommand komento = new SqlCommand(sql, kanta);
            komento.ExecuteNonQuery();

            kanta.Close();

            tkt.paivitaDataGrid("SELECT * FROM työntekijät", "työntekijät", tyontekijat_taulu);
            tkt.paivitaComboBox2(combo_ukot, combo_ukot2);
        }

        // painike lisää työ
        private void painike_lisaa_tyo(object sender, RoutedEventArgs e)
        {
            SqlConnection kanta = new SqlConnection(polku);
            kanta.Open();

            string sql = "INSERT INTO työnjako (työntekijä, rekisterinumero) VALUES ('" + combo_ukot2.Text + "','" + combo_autot2.Text + "')";

            SqlCommand komento = new SqlCommand(sql, kanta);
            komento.ExecuteNonQuery();

            kanta.Close();

            //tkt.paivitaDataGrid("SELECT * FROM työnjako", "työnjako", työnjako_lista);
            tkt.paivitaDataGrid("SELECT tyo.id_työnjako AS id_työnjako, ty.työnumero AS työntekijä, a.rekisterinumero AS rekisterinumero  FROM työntekijät ty, autot a, työnjako tyo WHERE a.rekisterinumero=tyo.rekisterinumero AND ty.työnumero=tyo.työntekijä ", "työnjako", työnjako_lista);
            //tkt.paivitaDataGrid("SELECT ti.id AS id, a.nimi AS asiakas, tu.nimi AS tuote, ti.toimitettu AS toimitettu  FROM tilaukset ti, asiakkaat a, tuotteet tu WHERE a.id=ti.asiakas_id AND tu.id=ti.tuote_id AND ti.toimitettu='0'", "tilaukset", tilaukset_lista);
        }
        

        // auton poisto nappula
        private void painike_poista_auto(object sender, RoutedEventArgs e)
        {
            SqlConnection kanta = new SqlConnection(polku);
            kanta.Open();

            string id = combo_autot.SelectedValue.ToString();
            SqlCommand komento = new SqlCommand("DELETE FROM autot WHERE id_auto =" + id + ";", kanta);
            komento.ExecuteNonQuery();
            kanta.Close();

            tkt.paivitaDataGrid("SELECT * FROM autot", "autot", huollossa_olevat_autot);
            tkt.paivitaComboBox(combo_autot, combo_autot);
        }

        // työntekijän poisto nappula
        private void painike_poista_työntekijä(object sender, RoutedEventArgs e)
        {
            SqlConnection kanta = new SqlConnection(polku);
            kanta.Open();

            string id = combo_ukot.SelectedValue.ToString();
            SqlCommand komento = new SqlCommand("DELETE FROM työntekijät WHERE id_työntekijä =" + id + ";", kanta);
            komento.ExecuteNonQuery();
            kanta.Close();

            tkt.paivitaDataGrid("SELECT * FROM työntekijät", "työntekijät", tyontekijat_taulu);
            tkt.paivitaComboBox2(combo_ukot, combo_ukot);
        }
        // painike valmis
        private void painike_valmis(object sender, RoutedEventArgs e)
        {
            DataRowView rivinakyma = (DataRowView)((Button)e.Source).DataContext;
            String id_auti = rivinakyma[0].ToString();

            SqlConnection kanta = new SqlConnection(polku);
            kanta.Open();

            // Päivitä työnjako-tauluun
            string tyonjakoUpdateSql = "UPDATE työnjako SET Valmis = 1 WHERE id_työnjako = '" + id_auti + "'";
            SqlCommand komento = new SqlCommand(tyonjakoUpdateSql, kanta);
            komento.ExecuteNonQuery();

            // Päivitä autojen_tilanne-tauluun
            string autojenTilanneUpdateSql = "UPDATE autojen_tilanne SET valmis = valmis WHERE id_auti = '" + id_auti + "'";
            SqlCommand autojenTilanneUpdateKomento = new SqlCommand(autojenTilanneUpdateSql, kanta);
            autojenTilanneUpdateKomento.ExecuteNonQuery();

            kanta.Close();

            // Päivitä DataGridit
            tkt.paivitaDataGrid("SELECT tyo.id_työnjako AS id_työnjako, ty.työnumero AS työntekijä, a.rekisterinumero AS rekisterinumero  FROM työntekijät ty, autot a, työnjako tyo WHERE a.rekisterinumero=tyo.rekisterinumero AND ty.työnumero=tyo.työntekijä ", "työnjako", työnjako_lista);
            tkt.paivitaDataGrid("SELECT ty.id_työntekijä as id_työntekijä, a.id_auto as id_auto, tyo.id_työnjako as id_auti  FROM työntekijät ty, autot a, työnjako tyo, autojen_tilanne auti WHERE ty.id_työntekijä=auti.id_työntekijä AND a.id_auto=auti.id_auto AND tyo.id_työnjako=auti.id_auti AND auti.valmis ='1'", "valmiit", autojen_tilanne);
        }

        /*
        private void painike_valmis(object sender, RoutedEventArgs e)
        {
            DataRowView rivinakyma = (DataRowView)((Button)e.Source).DataContext;
            String id_auti = rivinakyma[0].ToString();

            SqlConnection kanta = new SqlConnection(polku);
            kanta.Open();

            string sql = "UPDATE autojen_tilanne SET valmis=1 WHERE id_auti='" + id_auti + "';";

            SqlCommand komento = new SqlCommand(sql, kanta);
            komento.ExecuteNonQuery();
            kanta.Close();

            tkt.paivitaDataGrid("SELECT tyo.id_työnjako AS id_työnjako, ty.työnumero AS työntekijä, a.rekisterinumero AS rekisterinumero  FROM työntekijät ty, autot a, työnjako tyo WHERE a.rekisterinumero=tyo.rekisterinumero AND ty.työnumero=tyo.työntekijä ", "työnjako", työnjako_lista);
            tkt.paivitaDataGrid("SELECT ty.id_työntekijä as id_työntekijä, a.id_auto as id_auto, tyo.id_työnjako as id_auti  FROM työntekijät ty, autot a, työnjako tyo, autojen_tilanne auti WHERE ty.id_työntekijä=auti.id_työntekijä AND a.id_auto=auti.id_auto AND tyo.id_työnjako=auti.id_auti AND auti.valmis ='1'", "valmiit", autojen_tilanne);

        }
        */
    }
}
