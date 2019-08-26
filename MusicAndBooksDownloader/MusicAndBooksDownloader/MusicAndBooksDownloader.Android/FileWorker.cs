using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;
using MusicAndBooksDownloader.Interfaces;
using System.Net;
using System;
using MusicAndBooksDownloader.Model;

[assembly: Dependency(typeof(MusicAndBooksDownloader.Droid.FileWorker))]
namespace MusicAndBooksDownloader.Droid
{
    public class FileWorker : IFileWorker
    {
        
        public Task<bool> ExistsAsync(string filename)
        {
            // получаем путь к файлу
            string filepath = GetFilePath(filename);
            // существует ли файл
            bool exists = File.Exists(filepath);
            return Task<bool>.FromResult(exists);
        }

        public Task<IEnumerable<string>> GetFilesAsync()
        {
            // получаем все все файлы из папки
            IEnumerable<string> filenames = from filepath in Directory.EnumerateFiles(GetDocsPath())
                                            select Path.GetFileName(filepath);
            return Task<IEnumerable<string>>.FromResult(filenames);
        }


        public async Task SaveFileAsync(Songs song)
        {
            string filepath = GetFilePath(song.Name);
            await Task.Run(() =>
            {
                WebClient client = new WebClient();
                client.DownloadFileAsync(new Uri(song.downloadLink), filepath);
            });
           
        }
        // вспомогательный метод для построения пути к файлу
        string GetFilePath(string filename)
        {
            return Path.Combine(GetDocsPath(), filename);
        }
        // получаем путь к папке
        string GetDocsPath()
        {
            return Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryMusic).AbsolutePath;
        }
    }
}