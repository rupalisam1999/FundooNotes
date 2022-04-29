using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfases
{
   public interface ILableBL
    {
        Task AddLable(int userId, int noteId, string LableName);
        Task<List<Lable>> GetLableByUserId(int userId);
        Task<List<Lable>> GetLableByNoteId(int noteId);
        Task<Lable> ChangeLable(int lableId, string newLable);
        Task<Lable> UpdateLable(int userId, int lableId, string LableName);
        public Task DeleteLable(int lableId,int noteId );
        
    }
}
