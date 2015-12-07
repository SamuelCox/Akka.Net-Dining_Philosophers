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

        public async void EnterThinkingState()
        {
            Thread.Sleep(RandomGenerator.Next(3000) + 1000);
            Status = "thinking";
            await Report.Ask(Name + " is " + Status);
            Thread.Sleep(RandomGenerator.Next(3000) + 1000);

        }

        public async void EnterHungryState()
        {
            Status = "hungry";
            Report.Tell(Name + " is " + Status);
            await LeftFork.Ask("PickUp");
            Report.Tell(Name + " has picked up the left Fork");
            await RightFork.Ask("PickUp");
            Report.Tell(Name + " has picked up the right Fork");                        
            EnterEatingState();
        }

        public async void EnterEatingState()
        {
            Status = "eating";
            await Report.Ask(Name + " is " + Status);
            Thread.Sleep(RandomGenerator.Next(1000) + 1);
            await LeftFork.Ask("PutDown");
            Report.Tell(Name + " has put down the Left Fork");
            await RightFork.Ask("PutDown");
            Report.Tell(Name + " has put down the Right Fork");
            
            
        }

    }
}
