using System;
using System.IO;
using System.IO.Compression;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace NewGame4
{
    public class Program
    {
        static string sourceFile = "newfile.txt"; // исходный файл
        static string compressedFile = "newfile.gz"; // сжатый файл
        static string targetFile = "uncompressednewfile.txt"; // восстановленный файл

        public static void Main(string[] args)
        {
            // FileHelper.CreateFile();
            // FileHelper.CreateDirectory();

            // FileHelper.DeleteFile();
            // FileHelper.DeleteDirectory();

            // Compress(sourceFile, compressedFile);
            // Decompress(compressedFile, targetFile);
            // DeleteCompressedFile(compressedFile);

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\nServer started\n");
            CreateHostBuilder(args).Build().Run();
            Console.Read();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });


        public static void Compress(string sourceFile, string compressedFile)
        {
            if (File.Exists(sourceFile))
            {
                using (var sourceStream = new FileStream(sourceFile, FileMode.OpenOrCreate))
                {
                    using (var targetStream = File.Create(compressedFile))
                    {
                        using (GZipStream compressionStream = new GZipStream(targetStream, CompressionMode.Compress))
                        {
                            sourceStream.CopyTo(compressionStream);

                            Console.WriteLine("File compress succesfully");
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Source file doesn't exist");
            }
        }

        public static void Decompress(string compressedFile, string targetFile)
        {
            if (File.Exists(compressedFile))
            {
                using (FileStream sourceStream = new FileStream(compressedFile, FileMode.OpenOrCreate))
                {
                    using (FileStream targetStream = File.Create(targetFile))
                    {
                        using (GZipStream decompressionStream =
                            new GZipStream(sourceStream, CompressionMode.Decompress))
                        {
                            decompressionStream.CopyTo(targetStream);
                            Console.WriteLine("File decompress succesfully");
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Compressed file doesn't exist");
            }
        }

        public static void DeleteCompressedFile(string compressedFile)
        {
            if (File.Exists(compressedFile))
            {
                File.Delete(compressedFile);
                Console.WriteLine($"Compressed file {compressedFile} deleted");
            }
        }
    }
}