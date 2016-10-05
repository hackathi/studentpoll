using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace studentpoll.Models
{
    public class PollContext : DbContext
    {
        public PollContext() : base("Polls") { }

        public DbSet<Poll> Polls { get; set; }

        public DbSet<PollAnswer> Answers { get; set; }

        public DbSet<PollVote> Votes { get; set; }
    }
}