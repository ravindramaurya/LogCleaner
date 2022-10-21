using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogCleaner
{
    internal static class DummyFolderAndFileGenerator
    {
        private const int _numberOfFolders = 50;
        private const int _numberOfFiles = 10;
        private static readonly string _workingFolderPath = @"E:\Projects\LogCleaner\Logs";
        private static DateTime CreationDate { get; set; }
        private static string FormattedCreationDate { get { return CreationDate.ToString("dd-MM-yyyy"); } }
        
        public static void GenerateFoldersAndFiles()
        {
            string pathString;
            string fileName;
            
            for (int folderCount = 0; folderCount < _numberOfFolders; folderCount++)
            {
                CreationDate = DateTime.Now.AddDays(folderCount - _numberOfFolders);
                pathString = System.IO.Path.Combine(_workingFolderPath, FormattedCreationDate);
                System.IO.Directory.CreateDirectory(pathString);

                //Number of day folder older
                Directory.SetCreationTime(pathString, CreationDate);
                Directory.SetLastWriteTime(pathString, CreationDate);

                for (int fileCount = 0; fileCount < _numberOfFiles; fileCount++)
                {
                    fileName = string.Format("{0}-{1}.txt", FormattedCreationDate, fileCount.ToString());
                    string filePath = System.IO.Path.Combine(pathString, fileName);

                    if (!System.IO.File.Exists(filePath))
                    {
                        using (System.IO.FileStream fs = System.IO.File.Create(filePath))
                        {
                            for (byte i = 0; i < 100; i++)
                            {
                                fs.WriteByte(i);
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("File \"{0}\" already exists.", filePath);
                    }

                    if (System.IO.File.Exists(filePath))
                    {
                        //Number of day file older
                        File.SetCreationTime(filePath, CreationDate);
                        File.SetLastWriteTime(filePath, CreationDate);
                    }
                }
            }
        }
    }
}
