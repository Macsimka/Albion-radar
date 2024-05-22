using System.IO;
using System.Text.Json;

namespace Holo.Utils;

public static class Serializer
{
    public static void Serialize<T>(T obj, string fileName)
    {
        string jsonString = JsonSerializer.Serialize(obj, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(fileName, jsonString);
    }

    public static T DeserializeFromFile<T>(string path)
    {
        string jsonString = File.ReadAllText(path);
        return JsonSerializer.Deserialize<T>(jsonString);
    }
}
