using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using System.Threading;


namespace AkkaDiningPhilosophers
{
    class Philosopher : ReceiveActor
    {
        private IActorRef LeftFork {get; set;}
        private IActorRef RightFork { get; set;}
        private IActorRef Report {get; set;}
        private string Name {get; set;}
        private string Status { get; set;}
        private Random RandomGenerator {get; set;} 
        public Philosopher(string name, IActorRef leftFork, IActorRef rightFork, IActorRef report)
        {
            Name = name;
            LeftFork = leftFork;
            RightFork = rightFork;
            Report = report;
            RandomGenerator = new Random();
            StateTransition();
        }

        public void StateTransition()
        {
            while (true)
            {
                EnterThinkingState();
                EnterHungryState();
                EnterEatingState();
            }

        }

        public void EnterThinkingState()
        {
            Thread.Sleep(RandomGenerator.Next(1000) + 1);
            Status = "thinking";
            Report.Tell(Name + " is " + Status);
            Thread.Sleep(RandomGenerator.Next(1000) + 1);

        }

        public void EnterHungryState()
        {
            Status = "hungry";
            Report.Tell(Name + " is " + Status);
            LeftFork.Tell("PickUp");
            Receive<AckMessage>((message) =>
                Report.Tell(Name + " has picked up the left Fork"));
            RightFork.Tell("PickUp");
            Receive<AckMessage>((message) =>
                Report.Tell(Name + " has picked up the right Fork"));
            EnterEatingState();
        }

        public void EnterEatingState()
        {
            Status = "eating";
            Report.Tell(Name + " is " + Status);
            Thread.Sleep(RandomGenerator.Next(1000) + 1);
            LeftFork.Tell("PutDown");
            Receive<AckMessage>((message) =>
                Report.Tell(Name + " has put down the left Fork"));
            RightFork.Tell("PutDown");
            Receive<AckMessage>((message) =>
                Report.Tell(Name + " has put down the right Fork"));
            
        }

    }
}
