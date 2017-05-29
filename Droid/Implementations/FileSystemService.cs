using System;
namespace mvvmLight.Droid {
    public class FileSystemService : IFileSystemService {
        public FileSystemService() {
        }

        public int GetDBFileSize() {
            var dir = new System.IO.DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.Personal));
            int result =0;
            var files = dir.GetFiles();

            foreach (var file in files)
            {
                if (file.Extension.StartsWith(".db"))
                {
                    result += (int)file.Length;
                }
            }

            return result;
        }
    }
}
