using System;
using System.IO;

namespace FileConsolidator
{
    class Consolidator
    {
        public void Run()
        {
            string workingDirectory = GetWorkingDirectory();
            ConsolidateFiles(workingDirectory);
        }

        private string GetWorkingDirectory()
        {
            Console.WriteLine("Enter the root directory the files are stored in.");
            string directory = Console.ReadLine();

            while (!Directory.Exists(directory))
            {
                Console.WriteLine();
                Console.WriteLine("The directory {0} does not exist.", directory);
                directory = Console.ReadLine();
            }

            return directory;
        }

        private void ConsolidateFiles(string directory)
        {
            string consolidatedDirectory = directory + @"\consolidated\";
            Directory.CreateDirectory(consolidatedDirectory);

            // Get each file path in the main directory.
            foreach (string filePath in Directory.GetFiles(directory))
            {
                CopyFile(filePath, consolidatedDirectory);
            }

            // Get each file path in the sub directories.
            foreach (string directoryPath in Directory.GetDirectories(directory, "*", SearchOption.AllDirectories))
            {
                foreach (string filePath in Directory.GetFiles(directoryPath))
                {
                    CopyFile(filePath, consolidatedDirectory);
                }
            }

            Console.WriteLine("Done");
            Console.ReadKey();
        }

        private void CopyFile(string source, string destination)
        {
            Console.WriteLine("Moving {0} to {1}", source, destination + Path.GetFileName(source));

            try
            {
                File.Copy(source, destination + Path.GetFileName(source), true);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
