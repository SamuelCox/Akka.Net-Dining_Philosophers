using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;

namespace AkkaDiningPhilosophers
{
    class Fork : ReceiveActor
    {
        private IActorRef Report {get; set;}
        private int ForkID { get; set; }
        private bool IsOnTable { get; set; }

        public Fork(int id, IActorRef report)
        {
            Report = report;
            ForkID = id;
            IsOnTable = true;
            MessageHandling();
            
        }

        public void MessageHandling()
        {

            Receive<string>((msg) =>
            {
                if (msg == "PickUp" && IsOnTable)
                {
                    Report.Tell("Fork " + ForkID + " has been picked up");
                    Sender.Tell(new AckMessage());
                    
                    
                }
                
            });
            Receive<string>( (msg) =>
                {
                    if (msg == "PutDown" && !IsOnTable)
                    {
                        Report.Tell("Fork " + ForkID + " has been put down");
                        Sender.Tell(new AckMessage());
                    }
                });

        }

    }
}
