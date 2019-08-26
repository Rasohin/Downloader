using MusicAndBooksDownloader.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicAndBooksDownloader.Interfaces
{
    public interface IFileWorker
    {
        Task<bool> ExistsAsync(string filename); // проверка существования файла
        Task SaveFileAsync(Songs song);   // сохранение текста в файл
        Task<IEnumerable<string>> GetFilesAsync();  // получение файлов из определнного каталога
    }
}
