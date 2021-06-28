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
using Newtonsoft.Json;
using System.IO;

namespace KiK_SN
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        Web neural_web = new Web();
        public MainWindow()
        {
            InitializeComponent();
            neural_web.web_structure = new List<int>() { 3,3,2,2 };
            neural_web.Fill(neural_web);
        }

        private void Button01_Click(object sender, RoutedEventArgs e)
        {
            /*
            List<List<List<double>>> lista = JsonConvert.DeserializeObject<List<List<List<double>>>>(File.ReadAllText(@"train_data/sumatorek.json"));
            //neural_web.FillSetValues(neural_web); // sprawdzenie czy sieć działa na przygotowanym wcześniej przykładzie
            for (int i = 0; i < 100000; i++)
            {
                List<List<double>> random_data_to_learn = lista[new Random().Next(lista.Count)];
                CalculateWebData.Output(neural_web, random_data_to_learn[0]);
                CalculateWebData.BackwardPropagation(neural_web, random_data_to_learn[1], 0.1);
                neural_web.Clean();
            }
            */

        }
    }
}
