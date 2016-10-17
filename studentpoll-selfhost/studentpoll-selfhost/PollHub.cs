using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using studentpoll.Models;
using System.Threading;
using System.Configuration;

namespace studentpoll
{
    public class PollHub : Hub
    {
        public void Display(int pollid, string token)
        {
            if (token != ConfigurationManager.AppSettings["adminkey"])
                return;


            PollContext ctx = new PollContext();

            Poll p = ctx.Polls.Include("Answers").First(g => g.Id == pollid);

            PollViewModel pvm = new PollViewModel()
            {
                Id = p.Id,
                Title = p.Title
            };

            foreach(var answer in p.Answers)
            {
                pvm.Answers.Add(new PollAnswerViewModel()
                {
                    Id = answer.Id,
                    PollId = answer.PollId,
                    Text = answer.Text
                });
            }

            Clients.All.display(pvm);
        }

        public void Activate(int pollid, string token)
        {
            if (token != ConfigurationManager.AppSettings["adminkey"])
                return;

            PollContext ctx = new PollContext();

            Poll p = ctx.Polls.Include("Answers").First(g => g.Id == pollid);

            PollViewModel pvm = new PollViewModel()
            {
                Id = p.Id,
                Title = p.Title,
                Code = p.Code
            };

            foreach (var answer in p.Answers)
            {
                pvm.Answers.Add(new PollAnswerViewModel()
                {
                    Id = answer.Id,
                    PollId = answer.PollId,
                    Text = answer.Text
                });
            }

            Clients.All.activatePoll(pvm);

            ThreadPool.QueueUserWorkItem((o) => {
                Thread.Sleep(30000);

                Results(pollid, token);
            });
        }

        public void Results(int pollid, string token)
        {
            if (token != ConfigurationManager.AppSettings["adminkey"])
                return;

            PollContext ctx = new PollContext();
            var answers = ctx.Answers.Where(x => x.PollId == pollid).ToList();
            var votes = ctx.Votes.Where(x => x.PollId == pollid).ToList();
            string polltitle = ctx.Polls.Where(x => x.Id == pollid).Select(x => x.Title).First();

            BarChart bc = new BarChart();
            bc.labels = answers.Select(x => x.Text).ToArray();
            List<string> barbg = new List<string>();
            List<string> barborder = new List<string>();
            for (int i = 0; i < bc.labels.Length; i++)
                barbg.Add("rgba(54, 162, 235, 0.2)");
            for (int i = 0; i < bc.labels.Length; i++)
                barborder.Add("rgba(54, 162, 235, 1)");

            int[] data = new int[answers.Count];

            for (int i = 0; i < answers.Count; i++)
            {
                int my = votes.Where(x => x.PollAnswerId == answers[i].Id).Count();
                double whole = votes.Count();
                Console.WriteLine(string.Format("My: {0}, Whole: {1}", my, whole));
                data[i] = (int)((my / whole) * 100);
            }

            bc.datasets = new ChartDataset[1];
            bc.datasets[0] = new ChartDataset()
            {
                label = "Ergebnisse",
                backgroundColor = barbg.ToArray(),
                borderColor = barborder.ToArray(),
                borderWidth = 1,
                data = data
            };
            Console.WriteLine("Showing Results for " + polltitle);
            Clients.All.showResult(bc, polltitle);
        }

        public void Vote(int pollid, int answerid)
        {
            PollContext ctx = new PollContext();
            Console.WriteLine(string.Format("Got Vote! Poll {0}, Answer {1}", pollid, answerid));
            if (!ctx.Answers.Any(x => x.PollId == pollid && x.Id == answerid))
                return;

            ctx.Votes.Add(new PollVote() { PollAnswerId = answerid, PollId = pollid });
            ctx.SaveChanges();
            Clients.Caller.voteOk();
        }
    }
}
