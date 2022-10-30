using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using TestWPF.Models;

namespace TestWPF.Repositories
{
    internal class SavedUsers
    {
        public List<User> All { get; set; }

        private string _path;

        public SavedUsers()
        {
            _path = Path.Combine(Environment.CurrentDirectory + "\\Data\\Output\\test.json");
            string json = JsonReader.Get(_path);
            if (json.Length > 0)
                All = JsonConvert.DeserializeObject<List<User>>(json);
            else
                All = new List<User>();
        }

        public bool Save(User selectedUser)
        {
            try
            {
                All.Add(selectedUser);
                string text = JsonConvert.SerializeObject(All);
                JsonWriter.Save(_path, text);
                return true;
            }
            catch
            {
                All.Remove(selectedUser);
                return false;
            }
            
        }
    }
}
