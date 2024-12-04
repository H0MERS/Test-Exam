namespace TranRoutes.Utils
{
    public class FileReader
    {
        public string ReadAsString(string fileName)
        {
            if (File.Exists(fileName))
                return File.ReadAllText(fileName);
            else
                return "File not found";
        }
    }
}
