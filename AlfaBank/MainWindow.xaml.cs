using AlfaBank.Model;
using System;
using System.Threading.Tasks;
using System.Windows;
using Excel = Microsoft.Office.Interop.Excel;

namespace AlfaBank
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Services.DataService dataService;
        private const string dataWrittenOut = "Данные выписаны";

        public MainWindow()
        {
            dataService = new Services.DataService($"{Environment.CurrentDirectory}/Data/data.xml");
            InitializeComponent();
        }

        private async void btnRead(object sender, RoutedEventArgs e)
        {
            await dataService.Read();
            this.Line.Text = dataWrittenOut;
        }

        private async void btnReadRegular(object sender, RoutedEventArgs e)
        {
            await dataService.ReadRegularExpressions();
            this.Line.Text = dataWrittenOut;
        }

        private async void btnWriteExcel(object sender, RoutedEventArgs e)
        {
            await dataService.WriteExcel();
        }

        private async void btnWriteWord(object sender, RoutedEventArgs e)
        {
            await dataService.WriteWord();
        }

        private async void btnWriteTxt(object sender, RoutedEventArgs e)
        {
            await dataService.WriteTxt();
        }
    }
}
