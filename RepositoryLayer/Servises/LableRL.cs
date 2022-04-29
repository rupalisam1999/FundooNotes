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
    public class LableRL : ILableRL
    {
        FundooContext fundoo;
        public IConfiguration configuration { get; }
        public LableRL(FundooContext fundoo, IConfiguration configuration)
        {
            this.fundoo = fundoo;
            this.configuration = configuration;
        }
        public async Task AddLable(int userId, int noteId, string LableName)
        {
            try
            {
                var user = fundoo.Users.FirstOrDefault(u => u.UserId == userId);
                var note = fundoo.Notes.FirstOrDefault(u => u.NoteId == noteId);
                Lable lable = new Lable
                {
                    User = user,
                    Note = note
                };

                lable.LableName = LableName;
                lable.NoteId = noteId;
                lable.UserId = userId;
                fundoo.Add(lable);

                await fundoo.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<Lable>> GetLableByUserId(int userId)
        {
            try
            {
                List<Lable> reuslt = await fundoo.Lables.Where(u => u.UserId == userId).ToListAsync();
                return reuslt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Lable>> GetLableByNoteId(int noteId)
        {

            try
            {
                List<Lable> reuslt = await fundoo.Lables.Where(u => u.NoteId == noteId).ToListAsync();
                return reuslt;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<Lable> ChangeLable(int lableId, string newLable)
        {
            try
            {
                var res = fundoo.Lables.FirstOrDefault(u => u.LableId == lableId);
                if (res != null)
                {
                    res.LableName = newLable;
                    await fundoo.SaveChangesAsync();
                    return await fundoo.Lables.Where(a => a.LableId == lableId).FirstOrDefaultAsync();
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

        public async Task<Lable> UpdateLable(int userId, int lableId, string LableName)
        {
            try
            {
                var res = fundoo.Lables.FirstOrDefault(u => u.LableId == lableId && u.UserId == userId);
                if (res != null)
                {
                    res.LableName = LableName;
                    await fundoo.SaveChangesAsync();
                    return await fundoo.Lables.Where(a => a.LableId == lableId).FirstOrDefaultAsync();
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

        public async Task DeleteLable(int lableId,int noteId)
        {
            try
            {
                Lable res = fundoo.Lables.FirstOrDefault(u => u.LableId == lableId && u.NoteId == noteId);
                fundoo.Lables.Remove(res);
                await fundoo.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
