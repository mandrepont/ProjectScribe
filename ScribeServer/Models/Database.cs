using System;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScribeServer.Models
{
    /// <summary>
    /// The Database class is designed to be a model for the database, while also providing
    /// helper methods and static resources for client use.
    /// </summary>
    public class Database
    {
        //Const to where the database file is.
        private const string DATABASE_FILE = "database.dat";

        public List<Note> Notes { get; set; }
        public int LastNoteID { get; set; }

        //Static Database of current information.
        //Not the most practical in production.
        public static Database GlobalDatabase { get; set; }

        /// <summary>
        /// Creates the database for the first time with default values
        /// to avoid null excpetions.
        /// </summary>
        public static void InitDatabase()
        {
            GlobalDatabase = new Database()
            {
                Notes = new List<Note>(),
                LastNoteID = 0
            };
        }

        /// <summary>
        /// Check if the database file exist and load the data, 
        /// otherwise create the database file and initialize it.
        /// </summary>
        public static void CheckDatabase()
        {
            if (File.Exists(DATABASE_FILE))
                GlobalDatabase = JsonConvert.DeserializeObject<Database>(File.ReadAllText(DATABASE_FILE));
            else
            {
                InitDatabase();
                UpdateDB();
            }
            
        }

        /// <summary>
        /// Updates the json file of the database.
        /// </summary>
        public static void UpdateDB()
        {
            using (var sw = File.CreateText(DATABASE_FILE))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                new JsonSerializer().Serialize(writer, GlobalDatabase);
            }
        }
    }


    

}
