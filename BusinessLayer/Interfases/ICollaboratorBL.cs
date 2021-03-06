using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfases
{
  public interface ICollaboratorBL
    {
        Task AddCollaborator(int userId, int noteId, String CollaboratorEmail);
        Task<List<Collaborator>> GetCollaboratorByUserId(int userId);
        Task<List<Collaborator>> GetCollaboratorByNoteId(int userId, int NoteId);
        Task<bool> RemoveCollaborator(int userId, int NoteId, int collaboratorId);
    }
}
