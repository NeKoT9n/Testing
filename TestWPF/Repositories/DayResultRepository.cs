using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using TestWPF.Models;

namespace TestWPF.Repositories
{
    internal class DayResultRepository
    {
        public Dictionary<int, List<DayResult>> All { get; private set; }

        public DayResultRepository()
        {
            All = new Dictionary<int, List<DayResult>>();
            Init();
            OrderByKey();
        }

        private void Init()
        {

            string path = Path.Combine(Environment.CurrentDirectory + "\\Data\\Input\\");
            string[] fileList = Directory.GetFiles(path, "*.json");

            foreach(string file in fileList)
            {
                string fileName = Path.GetFileNameWithoutExtension(file);
                int index;
                int.TryParse(string.Join("", fileName.Where(c => char.IsDigit(c))), out index);
                if (All.Keys.Contains(index))
                    index = All.Count;

                string result = JsonReader.Get(file);
                var dayResult = JsonConvert.DeserializeObject<List<DayResult>>(result);
                All.Add(index, dayResult);
            }
           

        }

        private void OrderByKey()
        {
            All = All.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
        }
    }
}
