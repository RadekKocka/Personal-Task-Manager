using Personal_Task_Manager.Models;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Personal_Task_Manager.DataServices
{
    public class JsonFileProvider : IDataProvider
    {
        private readonly string defaultFilePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + "tasks.json";

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

            return JsonSerializer.Deserialize<List<TaskItem>>(File.ReadAllText(defaultFilePath)) ?? [];
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

            File.WriteAllText(defaultFilePath, JsonSerializer.Serialize(data, new JsonSerializerOptions
            {
                WriteIndented = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            }));
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
