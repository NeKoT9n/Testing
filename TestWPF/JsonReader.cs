using System.IO;

namespace TestWPF
{
    public class JsonReader
    {
        public static string Get(string path)
        {
            using (StreamReader reader = new StreamReader(path))
            {
                try
                {
                    string content = reader.ReadToEnd();
                    return content;
                }
                finally
                {
                    reader.Close();
                }
            }

        }
    }
}
