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
        string[] filesa;
        string[] typy = new string[] { "*.exe","*.sln","*.txt","*.csv"};
        Searcher finder = new Searcher();
        string lokace = "D:\\fiserkl15\\";

        public MainWindow()
        {
            InitializeComponent();
            for (int p = 0; p < typy.Count(); p++) { typsouboru.Items.Add(typy[p]);}
            string typ = "*.exe";
            filesa = finder.SearchFiles(lokace, typ);
            ZapisDoListu();
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
            
            //File.ReadAllText();

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
