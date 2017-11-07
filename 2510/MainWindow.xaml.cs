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
using FileHelpers;
using System.IO;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace _2510
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        // .csproj
        //http://www.wpf-tutorial.com/dialogs/the-openfiledialog/
        //adresy
        /*
         using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
{
    System.Windows.Forms.DialogResult result = dialog.ShowDialog();
}

 */
        string[] filesa;

        //trys = seznam!  
        string[] typy = new string[] { "*.exe","*.sln","*.txt","*.csv","*.csproj"};
        Searcher finder = new Searcher();
        string lokace = "D:\\fiserkl15\\";

        public MainWindow()
        {
            InitializeComponent();
            for (int p = 0; p < typy.Count(); p++) { typsouboru.Items.Add(typy[p]);}
            string typ = "*.exe";
            filesa = finder.SearchFiles(lokace, typ);
            ZapisDoListu();


            filesa = finder.SearchFiles(lokace, typy[4]);
            for(int u = 0; u < filesa.Count(); u++)
            {
                //string spoustec = GenSpoust();
                string spoustec = filesa[u];
                string slozka = System.IO.Path.GetDirectoryName(spoustec);

                string adresa = slozka + "/info"+u+".txt";
                if (!File.Exists(adresa))
                {
                    string textInfoClanku = "Informace o projektu";
                    File.WriteAllText(adresa, textInfoClanku);
                }
                //string slozka = System.IO.Path.GetDirectoryName(spoustec);


            }


            //File.ReadAllText();
        }

        private void Spoustec_Click(object sender, RoutedEventArgs e)
        {
            bool eh = CheckIfExists();
            if (eh == true)
            {
                string spoustec = GenSpoust();
                MessageBox.Show(spoustec);
                Process.Start(spoustec);
            }
            else
            {
                MessageBox.Show("Nezvolili jste žádný soubor!");
            }
        }


        private void typsouboru_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var typ = typsouboru.SelectedValue.ToString();
            filesa = finder.SearchFiles(lokace, typ);
            trys.Items.Clear();
            ZapisDoListu();          
        }
        private void infowrite_Click(object sender, RoutedEventArgs e)
        {
            
            bool eh = CheckIfExists();
            if (eh == true)
            {
                string spoustec = GenSpoust();
                string slozka = System.IO.Path.GetDirectoryName(spoustec);
                MessageBox.Show(slozka);

                //if (slozka)
                vypisinfo.Text = File.ReadAllText(spoustec);
            }
            else
            {
                MessageBox.Show("Nezvolili jste žádný soubor!");
            }
        }

        public bool ZapisDoListu()
        {
            for (int r = 0; r < filesa.Count(); r++)
            {
                trys.Items.Add(System.IO.Path.GetFileName((filesa[r])));
            }
            return true;
        }
       public bool CheckIfExists()
       {
            try
            {
                var zvoleno = trys.SelectedItem;
                int index = trys.Items.IndexOf(zvoleno);
                if (index == -1)
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }
            catch (Exception)
            {
                return false;
            }
            
       }
        private string GenSpoust()
        {
            var zvoleno = trys.SelectedItem;
            int index = trys.Items.IndexOf(zvoleno);
            string spoustec = filesa[index];
            //MessageBox.Show("Zvolili jste soubor " + zvoleno);
            return spoustec;
        }

        private void Ulozit_Click(object sender, RoutedEventArgs e)
        {
            //ONCLICK, ULOŽÍ ROZEPSÁNO
            //https://stackoverflow.com/questions/5603274/how-to-overwrite-not-append-a-text-file-in-asp-net-using-c-sharp
            //System.IO.File.WriteAllText(, vypisinfo.Text);
            //Schovat button! 
            //!!!!!!!!!!!!!PŘEPÍŠE VŠECHNY PROTOŽE SE JMENUJOU STEJNĚ 
            string path = GenSpoust();
            System.IO.File.WriteAllText(path , vypisinfo.Text);
            MessageBox.Show("Soubor byl uložen");
        }
    }
    class Searcher
    {
        string[] filesa;
        public string[] SearchFiles(string lokace, string typ)
        {
            filesa = Directory.GetFiles(lokace, typ, SearchOption.AllDirectories);
            return filesa;
        }

    }
}
