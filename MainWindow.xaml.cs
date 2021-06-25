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
            neural_web.web_structure = new List<int>() { 2,2,1 };
            neural_web.Fill(neural_web);
            //neural_web.FillSetValues(neural_web); // sprawdzenie czy sieć działa na przygotowanym wcześniej przykładzie
            CalculateWebData.Output(neural_web, new List<int>() { 1,0 });
            CalculateWebData.BackwardPropagation(neural_web, new List<int>() { 1 }, 0.1);
            neural_web.Clean();
        }

        private void Button01_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
