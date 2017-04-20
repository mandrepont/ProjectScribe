using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScribeClient
{
    public class Note
    {
        /// <summary>
        /// Unique Id to note. 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Title of the Note.
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Content of the note.
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// Name of the Author who owns the note.
        /// </summary>
        public string AuthorName { get; set; }
        /// <summary>
        /// ID of the Author used to get author information via database.
        /// </summary>
        public int AuthorId { get; set; }
        /// <summary>
        /// Is the note in the public listing.
        /// Currently Unused
        /// </summary>
        public bool IsPrivate { get; set; }
        /// <summary>
        /// Determins if the note is encypted using the author key.
        /// Currently Unused
        /// </summary>
        public bool IsEncrypted { get; set; }
        /// <summary>
        /// When the note was entered into the database orginally.
        /// </summary>
        public DateTime CreationDate { get; set; }
        /// <summary>
        /// Time the note was last updated.
        /// </summary>
        public DateTime LastUpdate { get; set; }
    }
}
