using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace studentpoll.Models
{
    public class PollViewModel
    {
        public PollViewModel()
        {
            Answers = new List<PollAnswerViewModel>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public List<PollAnswerViewModel> Answers { get; set; }
        public string Code { get; set; }
    }
}
