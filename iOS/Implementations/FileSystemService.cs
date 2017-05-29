using System;
using System.IO;

namespace mvvmLight.iOS {
    public class FileSystemService : IFileSystemService {
        public FileSystemService() {
        }

        int IFileSystemService.GetDBFileSize() {
            var docsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var libPath = Path.Combine(docsPath, "..", "Library/Application Support/com.arctouch.mvvmlight/BlobCache");
            var dir = new DirectoryInfo(libPath);
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
