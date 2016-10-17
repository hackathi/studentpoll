using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace studentpoll.Models
{
    public class Poll
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Code { get; set; }
        
        public int TutorGroupId { get; set; }

        public virtual ICollection<PollAnswer> Answers { get; set; }

        [ForeignKey("TutorGroupId")]
        public virtual TutorGroup TutorGroup { get; set; }
    }
}