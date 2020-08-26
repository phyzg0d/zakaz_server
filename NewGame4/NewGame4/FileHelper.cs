using System;
using System.IO;

namespace NewGame4
{
    public static class FileHelper
    {
        static string directoryPath = @"NewDirectory";
        static string filePath = @"newfile.txt";
        static string subDirectoryPath = @"NewSubDirectory";
        
        public static void CreateFile()
        {
           
            FileInfo newFile = new FileInfo(filePath);
            
            if (!newFile.Exists)
            {
                newFile.Create();
                Console.WriteLine($"File created {newFile.Name}");
                Console.WriteLine($"File extension {newFile.Extension}");
            }
            else
            {
                Console.WriteLine("File exist");
            }
        }

        public static void DeleteFile()
        {
            FileInfo newFile = new FileInfo(filePath);
            
            if (newFile.Exists)
            {
                newFile.Delete();
                Console.WriteLine("File has been removed");
            }
            else
            {
                Console.WriteLine("File not exist");
            }
        }

        public static void CreateDirectory()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(directoryPath);

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
                directoryInfo.CreateSubdirectory(subDirectoryPath);
                Console.WriteLine("Directory and subdirectory has been created");
            }
            else
            {
                Console.WriteLine("Directory exist");
            }
        }
        
        public static void DeleteDirectory()
        {
            if (Directory.Exists(directoryPath))
            {
                Directory.Delete(directoryPath, true);
                Console.WriteLine("Directory removed");
            }
            else
            {
                Console.WriteLine("Removing directory error");
            }
        }
    }
}