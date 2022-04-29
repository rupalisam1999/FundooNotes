using BusinessLayer.Interfases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entity;
using RepositoryLayer.FundooNotesContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LableController : ControllerBase
    {
        ILableBL lableBL;
        FundooContext fundoo;
        public LableController(ILableBL lableBL, FundooContext fundoo)
        {
            this.lableBL = lableBL;
            this.fundoo = fundoo;
        }

        [Authorize]
        [HttpPost("{noteId}/{LableName}")]
        public  async Task<ActionResult> AddNote(int noteId, string LableName)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(userid.Value);

                await this.lableBL.AddLable(noteId, userId, LableName);
                return this.Ok(new { success = true, message = "Lable Added Successfully " });
            }
            catch (Exception)
            {

                throw;
            }
        }

        [Authorize]
        [HttpGet("GetlabelByNoteId/{noteId}")]
        public async Task<ActionResult> GetLabelByNoteId(int noteId)
        {
            try
            {
                List<Lable> list = new List<Lable>();
                list = await this.lableBL.GetLableByNoteId(noteId);
                if (list == null)
                {
                    return this.BadRequest(new { success = true, message = "Failed to get label" });
                }
                return this.Ok(new { success = true, message = $"Label get successfully", data = list });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        

        [HttpGet("GetLableByUserId")]
        public async Task<ActionResult> GetLableByUserId()
        {

            try
            {
                List<Lable> list = new List<Lable>();
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(userid.Value);

                var result = await this.lableBL.GetLableByUserId(userId);
                return this.Ok(new { success = true, message = $"Below are the Lable data get by UserId", data = result });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpPut("ChangeLable/{lableId}")]
        public async Task<ActionResult> ChangeLable(int lableId, string newLable)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(userid.Value);
                var res = await this.lableBL.ChangeLable(lableId, newLable);
                if (res != null)
                    return this.Ok(new { success = true, message = "Lable changed successfully!!!" });
                else
                    return this.BadRequest(new { success = false, message = "Failed to change lable or Id does not exists" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpPut("UpdateLable/{lableId}")]
        public async Task<ActionResult> UpdateLable(int lableId,string LableName)
        { 
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(userid.Value);
                var res = await this.lableBL.UpdateLable(userId,lableId, LableName);
                if (res != null)
                    return this.Ok(new { success = true, message = "Lable Updated successfully!!!" });
                else
                    return this.BadRequest(new { success = false, message = "Failed to update lable or Id does not exists" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpDelete("Delete/{lableId}")]
        public async Task<ActionResult> DeleteLable(int lableId,int noteId)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(userid.Value);
                await this.lableBL.DeleteLable(lableId,noteId);
                return this.Ok(new { success = true, message = "Lable deleted successfully!!!" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
