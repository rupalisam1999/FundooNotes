using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RepositoryLayer.Entity
{
    public class Collaborator
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CollaboratorId { get; set; }
        public string CollaboratorEmail { get; set; }

        [ForeignKey("User")]
        public int? UserId { get; set; }

        [ForeignKey("Note")]
        public int? NoteId { get; set; }
        public virtual User User { get; set; }
        public virtual Note Note { get; set; }
    }
}
