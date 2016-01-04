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
            RandomGenerator = new Random(DateTime.Today.Millisecond);
            StateTransition();
        }

        public async Task<bool> StateTransition()
        {
            while (true)
            {
                EnterThinkingState();
                await EnterHungryState();
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

        public async Task<bool> EnterHungryState()
        {
            Status = "hungry";
            Report.Tell(Name + " is " + Status);
            bool result = false;
            while(result == false)
            {
                result = await AttemptToPickUpForks();
            }
            return true;

        }

        public async void EnterEatingState()
        {
            Status = "eating";
            Report.Tell(Name + " is " + Status);
            Thread.Sleep(RandomGenerator.Next(1000) + 1);
            await LeftFork.Ask("PutDown");
            Report.Tell(Name + " has put down the Left Fork");
            await RightFork.Ask("PutDown");
            Report.Tell(Name + " has put down the Right Fork");
            
            
        }

        public async Task<bool> AttemptToPickUpForks()
        {
            object leftResult = await LeftFork.Ask("PickUp");
            Report.Tell(Name + " has picked up the left Fork");
            object rightResult = await RightFork.Ask("PickUp");
            Report.Tell(Name + " has picked up the right Fork");                                    
            if(leftResult is AckMessage && rightResult is AckMessage)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        

    }
}
