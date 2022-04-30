using BusinessLayer.Interfases;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Servises
{
    public class LableBL : ILableBL
    {
        ILableRL lableRL;
        public LableBL(ILableRL lableRL)
        {
            this.lableRL = lableRL;
        }

        public async Task AddLable(int userId, int noteId, string LableName)
        {
            try
            {
                try
                {
                    await this.lableRL.AddLable(userId, noteId, LableName);
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

        public async Task<Lable> ChangeLable(int lableId, string newLable)
        {
            try
            {
                return await this.lableRL.ChangeLable(lableId, newLable);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task DeleteLable(int lableId, int noteId)
        {
            try
            {
                return this.lableRL.DeleteLable(lableId, noteId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<Lable>> GetLableByNoteId(int noteId)
        {
            try
            {
                return await this.lableRL.GetLableByNoteId(noteId);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<List<Lable>> GetLableByUserId(int userId)
        {
            try
            {
                return await this.lableRL.GetLableByUserId(userId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Lable> UpdateLable(int userId,int lableId,string LableName)
        {
            try
            {
                return await this.lableRL.UpdateLable(userId,lableId,LableName);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
