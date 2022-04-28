using BusinessLayer.Interfases;
using BusinessLayer.Servises;
using DataBaseLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entity;
using RepositoryLayer.FundooNotesContext;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    
    public class NoteController : ControllerBase
    {
        INoteBL noteBL;
        FundooContext fundoo;
        public NoteController(INoteBL noteBL, FundooContext fundoo)
        {
            this.noteBL = noteBL;
            this.fundoo = fundoo;
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> AddNote(NotePostModel notePostModel)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(userid.Value);

                await this.noteBL.AddNote(notePostModel, userId);
                return this.Ok(new { success = true, message = "Note Added Successfull " });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet("{noteId}")]
        public async Task<ActionResult>GetNote(int noteId,int userId)
        {

            try
            {
                var result = await this.noteBL.GetNote(noteId, userId);
                return this.Ok(new { success = true, message = $"Below are the Note data", data = result });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        

        
        [Authorize]
        [HttpDelete("Delete/{noteId}")]
        public async Task<ActionResult> DeleteNote(int noteId)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(userid.Value);
                await this.noteBL.DeleteNote(noteId, userId);
                return this.Ok(new { success = true, message = "Note deleted successfully!!!" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpPut("Update/{noteId}")]
        public async Task<IActionResult> UpdateNote(NotePostModel notePostModel, int noteId)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(userid.Value);
                var result = await this.noteBL.UpdateNote(notePostModel, noteId, userId);
                return this.Ok(new { success = true, message = $"Note updated successfully!!!", data = result });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpPut("ChangeColorNote/{noteId}")]
        public async Task<ActionResult> ChangeColorNote(int noteId, string color)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(userid.Value);
                var res = await this.noteBL.ChangeColor(noteId, userId, color);
                if (res != null)
                    return this.Ok(new { success = true, message = "Note color changed successfully!!!" });
                else
                    return this.BadRequest(new { success = false, message = "Failed to change color note or Id does not exists" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpPut("archivenote/{NoteId}")]
        public async Task<IActionResult> IsArchieve(int NoteId)
        {
            try
            {
                await noteBL.ArchieveNote(NoteId);


                return this.Ok(new { Success = true, message = $"NoteArchieve successfull for {NoteId}" });
            }
            catch (Exception)
            {

                throw;
            }
        }

        [Authorize]
        [HttpPut("PinNote/{NoteId}")]
        public async Task<IActionResult> IsPin(int NoteId)
        {
            try
            {
                await noteBL.PinNote(NoteId);


                return this.Ok(new { Success = true, message = $"PinNote successfull for {NoteId}" });
            }
            catch (Exception)
            {

                throw;
            }
        }

        [Authorize]
        [HttpPut("TrashNote/{NoteId}")]
        public async Task<IActionResult> IsTrash(int NoteId)
        {
            try
            {
                await noteBL.TrashNote(NoteId);


                return this.Ok(new { Success = true, message = $"TrashNote successfull for {NoteId}" });
            }
            catch (Exception)
            {

                throw;
            }
        }

        [Authorize]
        [HttpPut("ReminderNote/{NoteId}")]
        public async Task<IActionResult> IsReminder(int NoteId)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(userid.Value);

                await noteBL.ReminderNote(NoteId);

                return this.Ok(new { Success = true, message = $"ReminderNote successfull for {NoteId}" });
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
