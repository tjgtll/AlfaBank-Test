using AlfaBank.Model;
using System.Threading.Tasks;

namespace AlfaBank.Services
{
    interface IDataService
    {
        Task Read();
        Task ReadRegularExpressions();
        Task WriteTxt();
        Task WriteWord();
        Task WriteExcel();
    }
}
