using System.IO;

namespace TestWPF
{
    public class JsonWriter
    {
        public static void Save(string path, string value)
        {
            using (StreamWriter writer = new StreamWriter(path))
            {
                try
                {
                    writer.Write(value);
                }
                finally
                {
                    writer.Close();
                }
            }

        }

    }
}
