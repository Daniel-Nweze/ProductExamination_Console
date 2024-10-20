namespace Resources.Services;

public class FileService(string filepath) : IFileService
{
    private readonly string _filepath = filepath;

    #region Save & Load Service
    public bool SaveToFile(string content)
    {
        try
        {
            using var sw = new StreamWriter(_filepath);
            sw.WriteLine(content);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }
    public string LoadFromFile()
    {
        try
        {
            if (File.Exists(_filepath))
            {
                using var sr = new StreamReader(_filepath);
                var content = sr.ReadToEnd();
                return content;
            }
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine(ex.Message);
        }
        return null!;
    }
    #endregion

}
