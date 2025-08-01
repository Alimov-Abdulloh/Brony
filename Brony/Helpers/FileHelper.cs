using Newtonsoft.Json;

namespace Brony.Helpers;

public static class FileHelper
{
    public static List<T> ReadFromFile<T>(string filePath)
    {
        if (!File.Exists(filePath))
        {
            File.Create(filePath).Close(); // Ensure the file exists
        }
        var text = File.ReadAllText(filePath);
        return JsonConvert.DeserializeObject<List<T>>(text) ?? Enumerable.Empty<T>().ToList();
    }

    public static void WriteToFile<T>(string filePath, List<T> source)
    {
        var json = JsonConvert.SerializeObject(source, Formatting.Indented);
        File.WriteAllText(filePath, json);
    }
}
