using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Clickr
{
    public class Persistence
    {
        private static Persistence _persistence = null;
        public static Persistence GetInstance()
        {
            return _persistence = _persistence ?? new Persistence();
        }
        public StorageFolder LocalFolder { get { return ApplicationData.Current.LocalFolder; } }
        public async void SaveLast(string value)
        {
            Save(value, "last");
        }

        public async void SaveRecord(string value)
        {
            Save(value, "record");
        }

        public async Task<string> ReadLast()
        {
            return await Read("last");
        }

        public async Task<string> ReadRecord()
        {
            return await Read("record");
        }

        private async Task<string> Read(string name)
        {
            var result = "00000";
            if (LocalFolder != null)
            {
                try
                {
                    var dataFolder = await LocalFolder.GetFolderAsync("DataFolder");
                    var file = await dataFolder.OpenStreamForReadAsync(FileName(name));
                    using (var streamReader = new StreamReader(file))
                    {
                        result = streamReader.ReadToEnd();
                    }
                }
                catch
                {
                    // ignored
                }
            }
            return result.ToString();
        }

        private async void Save(string value, string name)
        {
            byte[] fileBytes = Encoding.UTF8.GetBytes(value.ToString().ToCharArray());
            var dataFolder = await LocalFolder.CreateFolderAsync("DataFolder",
                CreationCollisionOption.OpenIfExists);
            var file = await dataFolder.CreateFileAsync(FileName(name),
            CreationCollisionOption.ReplaceExisting);
            using (var s = await file.OpenStreamForWriteAsync())
            {
                s.Write(fileBytes, 0, fileBytes.Length);
            }
        }

        private string FileName(string name)
        {
            return string.Concat(name, ".txt");
        }
    }
}
