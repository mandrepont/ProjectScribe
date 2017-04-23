using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ScribeClient.Service
{
    public class NoteService
    {
        /// <summary>
        /// Post a note to the API server.
        /// NOTE: USER MUST BE AUTHED
        /// </summary>
        /// <param name="note">Note to post.</param>
        /// <returns>true if posted otherwise false.</returns>
        public static async Task<bool> PostGeneralNote(Note note)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(Constants.BASE_URI_API);
                client.SetBearerToken(Constants.AccessToken);
                var jsonnote = JsonConvert.SerializeObject(note);
                var content = new StringContent(jsonnote, Encoding.UTF8, "application/json");
                try
                {
                    HttpResponseMessage response = await client.PostAsync("/api/notes", content);
                    if (response.StatusCode == HttpStatusCode.Created)
                        return true;
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.InnerException.Message);
                }
                return false;
                
            }
        }

        /// <summary>
        /// Retreives the list made public to everyone.
        /// </summary>
        /// <returns>List of notes. </returns>
        public static async Task<List<Note>> GetPublicList()
        {
            var publiclist = new List<Note>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(Constants.BASE_URI_API);
                try
                {
                    var respone = await client.GetAsync("/api/notes");
                    string list = await respone.Content.ReadAsStringAsync();
                    publiclist = JsonConvert.DeserializeObject<List<Note>>(list);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.InnerException.Message);
                }

            }

                return publiclist;
        }

        /// <summary>
        /// Get the current logged in user's private list. 
        /// NOTE: MUST BE AUTHED
        /// </summary>
        /// <returns>List of notes. </returns>
        public static async Task<List<Note>> GetPrivateList()
        {
            var publiclist = new List<Note>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(Constants.BASE_URI_API);
                client.SetBearerToken(Constants.AccessToken);
                try
                {
                    var respone = await client.GetAsync("/api/notes/private");
                    string list = await respone.Content.ReadAsStringAsync();
                    publiclist = JsonConvert.DeserializeObject<List<Note>>(list);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.InnerException.Message);
                }

            }

            return publiclist;
        }

        /// <summary>
        /// Gets a private note of the current user via ID
        /// NOTE: USER MUST BE AUTHED
        /// </summary>
        /// <param name="id">ID of note.</param>
        /// <returns>Private note</returns>
        public static async Task<Note> GetPrivateNoteById(int id)
        {
            var note = new Note();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(Constants.BASE_URI_API);
                client.SetBearerToken(Constants.AccessToken);
                try
                {
                    var respone = await client.GetAsync("/api/notes/private/" + id);
                    string jsonnote = await respone.Content.ReadAsStringAsync();
                    note = JsonConvert.DeserializeObject<Note>(jsonnote);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.InnerException.Message);
                }
            }

            return note;
        }

        /// <summary>
        /// Retreieves a public note from the general repo.
        /// </summary>
        /// <param name="id">Id of note.</param>
        /// <returns>Note Object.</returns>
        public static async Task<Note> GetNoteById(int id)
        {
            var note = new Note();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(Constants.BASE_URI_API);
                try
                {
                    var respone = await client.GetAsync("/api/notes/" + id);
                    string jsonnote = await respone.Content.ReadAsStringAsync();
                    note = JsonConvert.DeserializeObject<Note>(jsonnote);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.InnerException.Message);
                }
            }

            return note;
        }

        /// <summary>
        /// Update note will update an existing note.
        /// </summary>
        /// <param name="note">Updated version of the note.</param>
        /// <returns>true = worked, false = failed.</returns>
        public static async Task<bool> UpdateNote(Note note)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(Constants.BASE_URI_API);
                //TODO: Add token support when the server accepts it.
                var jsonnote = JsonConvert.SerializeObject(note);
                var content = new StringContent(jsonnote, Encoding.UTF8, "application/json");
                try
                {
                    HttpResponseMessage response = await client.PutAsync("/api/notes/"+note.Id, content);
                    if (response.StatusCode == HttpStatusCode.Created)
                        return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.InnerException.Message);
                }
            }
            return false;
        }

        /// <summary>
        /// Removes note based on ID provided. 
        /// </summary>
        /// <param name="id">Id of note to remove.</param>
        /// <returns>true = removed, false = failed.</returns>
        public static async Task<bool> RemoveNote(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(Constants.BASE_URI_API);
                //TODO: Add token support when the server accepts it.
                try
                {
                    var respone = await client.DeleteAsync("/api/notes/" + id);
                    return respone.IsSuccessStatusCode;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.InnerException.Message);
                }
            }

            return false;
        }
    }
}
