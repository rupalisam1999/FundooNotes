using DataBaseLayer;
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
    public class NoteRL : INoteRL

    {
        FundooContext fundoo;
        public IConfiguration configuration { get; }
        public NoteRL(FundooContext fundoo, IConfiguration configuration)
        {
            this.fundoo = fundoo;
            this.configuration = configuration;
        }

        public async Task AddNote(NotePostModel notePostModel, int userId)
        {
            try
            {


                var user = fundoo.Users.FirstOrDefault(u => u.UserId == userId);
                Note note = new Note
                {

                    User = user
                };
                note.Title = notePostModel.Title;
                note.Description = notePostModel.Description;
                note.BGColor = notePostModel.BGColor;
                note.registerdDate = DateTime.Now;
                note.ModifiedDate = DateTime.Now;
                fundoo.Add(note);
                await fundoo.SaveChangesAsync();


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

                //var user = fundoo.Users.FirstOrDefault(u => u.UserId == userId);
                return await fundoo.Notes.Where(u => u.NoteId == noteId && u.UserId == userId)

                    .Include(u => u.User).FirstOrDefaultAsync();


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
                var res = fundoo.Notes.FirstOrDefault(u => u.NoteId == noteId && u.UserId == userId);
                if (res != null)
                {
                    res.Title = notePostModel.Title;
                    res.Description = notePostModel.Description;
                    res.BGColor = notePostModel.BGColor;

                    await fundoo.SaveChangesAsync();

                    return await fundoo.Notes.Where(a => a.NoteId == noteId).FirstOrDefaultAsync();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task DeleteNote(int noteId, int userId)
        {
            try
            {
                Note res = fundoo.Notes.FirstOrDefault(u => u.NoteId == noteId && u.UserId == userId);
                fundoo.Notes.Remove(res);
                await fundoo.SaveChangesAsync();
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
                var note = fundoo.Notes.FirstOrDefault(u => u.NoteId == NoteId);
                note.IsArchive = true;
                await fundoo.SaveChangesAsync();

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
                
                var note = fundoo.Notes.FirstOrDefault(u => u.NoteId == NoteId);
                note.IsPin = true;
                await fundoo.SaveChangesAsync();

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
                    var note = fundoo.Notes.FirstOrDefault(u => u.NoteId == NoteId);
                    note.IsTrash = true;
                    await fundoo.SaveChangesAsync();
                
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task ReminderNote(int NoteId)
        {
           
            
                var note = fundoo.Notes.FirstOrDefault(u => u.NoteId == NoteId);
                if( note != null)
                { 
                note.IsReminder = true;
                await fundoo.SaveChangesAsync();

                }
                else
                {
                note.IsReminder = false;
                }
        }

        public async Task<Note> ChangeColor(int noteId, int userId, string newColor)
        {
            try
            {
                var res = fundoo.Notes.FirstOrDefault(u => u.NoteId == noteId && u.UserId == userId);
                if (res != null)
                {
                    res.BGColor = newColor;
                    await fundoo.SaveChangesAsync();
                    return await fundoo.Notes.Where(a => a.NoteId == noteId).FirstOrDefaultAsync();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw e;
            }

        }
    }
}
    

