using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace studentpoll.Models
{
    public class PollVote
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public int PollAnswerId { get; set; }

        public int PollId { get; set; }

        [ForeignKey("PollAnswerId")]
        public PollAnswer Answer { get; set; }
    }
}