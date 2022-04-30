using BusinessLayer.Interfases;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Servises
{
   public class CollaboratorBL:ICollaboratorBL
    {
        ICollaborationRL collaboratorRL;
        public CollaboratorBL(ICollaborationRL collaboratorRL)
        {
            this.collaboratorRL = collaboratorRL;
        }

        public async Task AddCollaborator(int userId, int noteId, String CollaboratorEmail)
        {
            try
            {
                await this.collaboratorRL.AddCollaborator(userId, noteId, CollaboratorEmail);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Collaborator>> GetCollaboratorByNoteId(int userId, int NoteId)
        {
            try
            {
                return await this.collaboratorRL.GetCollaboratorByNoteId(userId,NoteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Collaborator>> GetCollaboratorByUserId(int userId)
        {
            try
            {
                return await this.collaboratorRL.GetCollaboratorByUserId(userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> RemoveCollaborator(int userId, int NoteId, int collaboratorId)
        {
            try
            {
                return await this.collaboratorRL.RemoveCollaborator(userId, NoteId, collaboratorId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
