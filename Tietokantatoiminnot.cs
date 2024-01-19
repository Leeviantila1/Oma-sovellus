using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Oma_sovellus
{
    internal class Tietokantatoiminnot
    {
        string polku = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\k2002137\\Documents\\Tietokanta.mdf;Integrated Security=True;Connect Timeout=30";

        public void paivitaDataGrid(string kysely, string taulu, DataGrid grid)
        {
            SqlConnection kanta = new SqlConnection(polku);
            kanta.Open();

            /*tehdään sql komento*/
            SqlCommand komento = kanta.CreateCommand();
            komento.CommandText = kysely; // kysely

            /*tehdään data adapteri ja taulu johon tiedot haetaan*/
            SqlDataAdapter adapteri = new SqlDataAdapter(komento);
            DataTable dt = new DataTable(taulu);
            adapteri.Fill(dt);

            /*sijoitetaan data-taulun tiedot DataGridiin*/
            grid.ItemsSource = dt.DefaultView;

            kanta.Close();
        }

        // autojen comboboxit
        public void paivitaComboBox(ComboBox kombo1, ComboBox kombo3)
        {
            SqlConnection kanta = new SqlConnection(polku);
            kanta.Open();

            SqlCommand komento = new SqlCommand("SELECT * FROM autot", kanta);
            SqlDataReader lukija = komento.ExecuteReader();

            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(string));
            dt.Columns.Add("REKISTERINUMERO", typeof(string));

            kombo1.ItemsSource = dt.DefaultView;
            kombo1.DisplayMemberPath = "ID";
            kombo1.SelectedValuePath = "ID";
            kombo3.ItemsSource = dt.DefaultView;
            kombo3.DisplayMemberPath = "REKISTERINUMERO";
            kombo3.SelectedValuePath = "ID";

            while (lukija.Read())
            {
                int id = lukija.GetInt32(0);
                string rekisterinumero = lukija.GetString(2);
                dt.Rows.Add(id, rekisterinumero);
            }

            lukija.Close();
            kanta.Close();
        }

        // työntekijöiden comboboxit
        public void paivitaComboBox2(ComboBox kombo2, ComboBox kombo4)
        {
            SqlConnection kanta = new SqlConnection(polku);
            kanta.Open();

            SqlCommand komento = new SqlCommand("SELECT * FROM työntekijät", kanta);
            SqlDataReader lukija = komento.ExecuteReader();

            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(string));
            dt.Columns.Add("TYÖNUMERO", typeof(string));

            kombo2.ItemsSource = dt.DefaultView;
            kombo2.DisplayMemberPath = "ID";
            kombo2.SelectedValuePath = "ID";
            kombo4.ItemsSource = dt.DefaultView;
            kombo4.DisplayMemberPath = "TYÖNUMERO";
            kombo4.SelectedValuePath = "ID";

            while (lukija.Read())
            {
                int id = lukija.GetInt32(0);
                string tuote = lukija.GetString(2);
                dt.Rows.Add(id, tuote);
            }

            lukija.Close();
            kanta.Close();
        }
    }
}
