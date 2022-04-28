using DataBaseLayer;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfases
{
   public interface INoteBL
    {
        Task AddNote(NotePostModel notePostModel, int userId);
        Task<Note> GetNote(int noteId, int userId);
        Task DeleteNote(int noteId, int userId);
        Task<Note> UpdateNote(NotePostModel notePostModel, int noteId, int userId);

        Task<Note> ChangeColor(int noteId, int userId, string newColor);
        Task ArchieveNote(int NoteId);

        Task PinNote(int NoteId);

        Task TrashNote(int NoteId);
        Task ReminderNote(int NoteId);




    }
}
