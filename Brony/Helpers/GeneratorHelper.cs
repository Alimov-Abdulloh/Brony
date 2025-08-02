using Brony.Models.Common;
using Newtonsoft.Json;

namespace Brony.Helpers;

public static class GeneratorHelper
{
    public static int GenerateId(string filePath)
    {
        if (File.Exists(filePath))
        {
            string text = File.ReadAllText(filePath);

            var baseModels = JsonConvert.DeserializeObject<List<BaseModel>>(text);
            
            int maxId = baseModels.MaxBy(t => t.Id).Id;

            return ++maxId;
        }
        
        return 1;
    }
}