using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Entity;
using RepositoryLayer.FundooNotesContext;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Servises
{
    public class CollaboratorRL : ICollaborationRL
    {
        FundooContext fundoo;
        public IConfiguration configuration { get; }
        public CollaboratorRL(FundooContext fundoo, IConfiguration configuration)
        {
            this.fundoo = fundoo;
            this.configuration = configuration;
        }
        public async Task AddCollaborator(int userId, int noteId, String CollaboratorEmail)
        {
            try
            {
                var user = fundoo.Users.FirstOrDefault(u => u.UserId == userId);
                var note = fundoo.Notes.FirstOrDefault(u => u.NoteId == noteId);
                Collaborator collaborator = new Collaborator
                {
                    User = user,
                    Note = note
                };

                collaborator.CollaboratorEmail = CollaboratorEmail;
                collaborator.NoteId = noteId;
                   collaborator.UserId = userId;
                fundoo.Collaborators.Add(collaborator);

                await fundoo.SaveChangesAsync();
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
                List<Collaborator> result = await fundoo.Collaborators.Where(u => u.UserId == userId).Include(u => u.User).Include(U => U.Note).ToListAsync();
                return result;
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
                List<Collaborator> result = await fundoo.Collaborators.Where(u => u.UserId == userId && u.NoteId == NoteId).Include(u => u.User).Include(U => U.Note).ToListAsync();
                return result;
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
                var result = fundoo.Collaborators.FirstOrDefault(u => u.UserId == userId && u.NoteId == NoteId && u.CollaboratorId == collaboratorId);
                if (result != null)
                {
                    fundoo.Collaborators.Remove(result);
                    await fundoo.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
 }

