using BusinessLayer.Interfases;
using DataBaseLayer;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Servises
{
    public class NoteBL : INoteBL

    {
        INoteRL noteRL;
        public NoteBL(INoteRL userRL)
        {
            this.noteRL = userRL;
        }
        public async Task AddNote(NotePostModel notePostModel, int userId)
        {
            try
            {
                try
                {
                    await this.noteRL.AddNote(notePostModel, userId);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }



        public async Task<Note> GetNote(int noteId, int userId)
        {
            try
            {
                return await this.noteRL.GetNote(noteId, userId);
            }
            catch (Exception)
            {

                throw;
            }

        }
      



        public async Task<Note> UpdateNote(NotePostModel notePostModel, int noteId, int userId)
        {
            try
            {
                return await this.noteRL.UpdateNote(notePostModel, noteId, userId);
            }
            catch (Exception ex)
            {
                throw ex;
            };
        }

        public Task DeleteNote(int noteId, int userId)
        {
            try
            {
                return this.noteRL.DeleteNote(noteId, userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       

        public async Task ArchieveNote(int NoteId)
        {
            try
            {
                await noteRL.ArchieveNote(NoteId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task PinNote(int NoteId)
        {
            try
            {
                await noteRL.PinNote(NoteId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task TrashNote(int NoteId)
        {
            try
            {
                await noteRL.TrashNote(NoteId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task ReminderNote(int NoteId)
        {
            try
            {
                await noteRL.ReminderNote(NoteId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Task<Note> ChangeColor(int noteId, int userId, string newColor)
        {
            try
            {
                return this.noteRL.ChangeColor(noteId, userId, newColor);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}