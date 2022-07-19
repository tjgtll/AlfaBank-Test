using AlfaBank.Model;
using System;
using System.IO;
using System.Xml.Serialization;
using System.Windows;
using Word = Microsoft.Office.Interop.Word;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data;
using System.Threading.Tasks;
using System.Xml;
using System.Text;

namespace AlfaBank.Services
{
    public class DataService : IDataService
    {
        private Channel[] list;
        private string path;

        public DataService(string path)
        {
            this.path = path;
        }

        public async Task Read()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Channels));
            StreamReader reader = new StreamReader(path);
            list = ((Channels)serializer.Deserialize(reader)).Items;
            reader.Close();
        }

        public async Task ReadRegularExpressions()
        {
            string content;
            using (StreamReader reader = new StreamReader(path, Encoding.UTF8))
            {
                content = reader.ReadToEnd();
            }
            
        }

        public async Task WriteTxt()
        {
            if (list == null)
            {
                MessageBox.Show("Данных нет");
                return;
            }
            string path = $"{Environment.CurrentDirectory}/Data/output.txt";
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                foreach (Channel channel in list)
                {
                    await writer.WriteLineAsync(channel.Titel);
                    await writer.WriteLineAsync(channel.Link);
                    await writer.WriteLineAsync(channel.Description);
                    await writer.WriteLineAsync(channel.Category);
                    await writer.WriteLineAsync(channel.PubDate);
                }
            }
        }
        
        public async Task WriteWord()
        {
            if (list == null)
            {
                MessageBox.Show("Данных нет");
                return;
            }
            Word.Application winword = new Word.Application()
            {
                ShowAnimation = false,
                Visible = false
            };
            Word.Document document = winword.Documents.Add();
            Word.Paragraph paragraph = document.Content.Paragraphs.Add();
            foreach (Channel channel in list)
            {
                paragraph.Range.Text = $"\t{channel.Titel}{Environment.NewLine}";
                paragraph.Range.Text = $"\t{channel.Link}{Environment.NewLine}";
                paragraph.Range.Text = $"\t{channel.Description}{Environment.NewLine}";
                paragraph.Range.Text = $"\t{channel.Category}{Environment.NewLine}";
                paragraph.Range.Text = $"\t{channel.PubDate}{Environment.NewLine}";
                paragraph.Range.Text = Environment.NewLine;
            }
            string filename = $"{Environment.CurrentDirectory}/Data/output.docx";
            document.SaveAs2(filename);
            document.Close();
            winword.Quit();
        }

        public async Task WriteExcel()
        {
            if (list == null)
            {
                MessageBox.Show("Данных нет");
                return;
            }
            Excel.Application excel = new Excel.Application()
            {
                Visible = false,
                DisplayAlerts = false
            };
            Excel.Workbook worKbooK = excel.Workbooks.Add(Type.Missing);
            Excel.Worksheet worKsheeT = worKbooK.ActiveSheet;
            worKsheeT.Name = "New`s";
            worKsheeT.Cells.Font.Size = 12;
            worKsheeT.Rows[3].Style.WrapText = true;
            int j = 1;
            foreach (Channel channel in list)
            {
                worKsheeT.Cells[1, j] = channel.Titel;
                worKsheeT.Cells[2, j] = channel.Link;
                worKsheeT.Rows[3].ColumnWidth = 40;
                worKsheeT.Cells[3, j] = channel.Description;
                worKsheeT.Cells[4, j] = channel.Category;
                worKsheeT.Cells[5, j] = channel.PubDate;
                j++;
            }

            worKsheeT.get_Range("A1", "A1").Style.VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
            worKbooK.SaveAs($"{Environment.CurrentDirectory}\\Data\\output.xlsx");
            worKbooK.Close();
            excel.Quit();
        }
    }
}
