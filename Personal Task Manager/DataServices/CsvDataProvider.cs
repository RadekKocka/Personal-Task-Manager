using CsvHelper;
using Personal_Task_Manager.Models;
using System.IO;

namespace Personal_Task_Manager.DataServices
{
    public class CsvDataProvider : IDataProvider
    {
        private readonly string defaultFilePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + "tasks.csv";

        public List<TaskItem> LoadData()
        {
            if (!File.Exists(defaultFilePath))
            {
                return new List<TaskItem>();
            }

            if (IsFileInUseGeneric(new FileInfo(defaultFilePath)))
            {
                throw new IOException($"The file {defaultFilePath} is currently in use by another process.");
            }

            using StreamReader streamReader = new StreamReader(defaultFilePath);
            using CsvReader csvReader = new(streamReader, System.Globalization.CultureInfo.InvariantCulture);
            return csvReader.GetRecords<TaskItem>().ToList();
        }

        public void SaveData(List<TaskItem> data)
        {
            if (!File.Exists(defaultFilePath))
            {
                File.Create(defaultFilePath).Close();
            }

            if (IsFileInUseGeneric(new FileInfo(defaultFilePath)))
            {
                throw new IOException($"The file {defaultFilePath} is currently in use by another process.");
            }

            using CsvWriter reader = new(new StreamWriter(defaultFilePath), System.Globalization.CultureInfo.InvariantCulture);
            reader.WriteRecords(data);
        }

        public static bool IsFileInUseGeneric(FileInfo file)
        {
            try
            {
                using var stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
            }
            catch (IOException)
            {
                return true;
            }

            return false;
        }
    }
}
