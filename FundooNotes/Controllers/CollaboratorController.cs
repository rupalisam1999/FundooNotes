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
    public class CollaboratorController : ControllerBase
    {
        ICollaboratorBL collaboratorBL;
        FundooContext fundoo;
        public CollaboratorController(ICollaboratorBL collaboratorBL, FundooContext fundoo)
        {
            this.collaboratorBL = collaboratorBL;
            this.fundoo = fundoo;
        }

        [Authorize]
        [HttpPost("AddCollaborator/{noteId}/{CollaboratorEmail}")]
        public async Task<ActionResult> AddCollaborator(int noteId, String CollaboratorEmail)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(userid.Value);

                await this.collaboratorBL.AddCollaborator(noteId, userId, CollaboratorEmail);
                return this.Ok(new { success = true, message = "Collaborator Added Successfully " });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [Authorize]
        [HttpGet("GetCollaboratorByUserId")]
        public async Task<ActionResult> GetCollaboratorByUserId()
        {

            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(userid.Value);
                var Id = fundoo.Collaborators.Where(x => x.UserId == userId).FirstOrDefault();
                if (Id == null)
                {
                    return this.BadRequest(new { success = false, message = $"UserId doesn't exists" });
                }
                List<Collaborator> result = await this.collaboratorBL.GetCollaboratorByUserId(userId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = $"Collaborator got successfully", data = result });
                }
                return this.BadRequest(new { success = false, message = $"Failed to get collaborator" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpGet("GetCollaboratorByNoteId/{NoteId}")]
        public async Task<ActionResult> GetCollaboratorByNoteId(int NoteId)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(userid.Value);
                var Id = fundoo.Collaborators.FirstOrDefault(x => x.NoteId == NoteId && x.UserId == userId);
                if (Id == null)
                {
                    return this.BadRequest(new { success = false, message = $"NoteId doesn't exists" });
                }
                List<Collaborator> result = await this.collaboratorBL.GetCollaboratorByNoteId(userId,NoteId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = $"Collaborator got successfully", data = result });
                }
                return this.BadRequest(new { success = false, message = $"Failed to get collaborator" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpDelete("RemoveCollaborator/{NoteId}/{collaboratorId}")]
        public async Task<ActionResult> RemoveCollaborator(int NoteId, int collaboratorId)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(userid.Value);
                var re = fundoo.Collaborators.Where(x => x.UserId == userId && x.NoteId == NoteId).FirstOrDefault();
                if (re == null)
                {
                    return this.BadRequest(new { success = false, message = $"Note doesn't exists" });
                }
                bool result = await this.collaboratorBL.RemoveCollaborator(userId, NoteId, collaboratorId);
                if (result == true)
                {
                    return this.Ok(new { success = true, message = $"Collaborator removed successfully" });
                }
                return this.BadRequest(new { success = false, message = $"Failed to remove collaborator" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
