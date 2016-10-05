using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace studentpoll.Models
{
    public class PollAnswer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public string Text { get; set; }

        public int PollId { get; set; }

        [ForeignKey("PollId")]
        public Poll Poll { get; set; }
    }
}