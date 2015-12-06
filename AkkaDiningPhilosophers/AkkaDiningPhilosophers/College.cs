using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;

namespace AkkaDiningPhilosophers
{
    class College
    {
        public College()
        {
            ActorSystem system = ActorSystem.Create("Philosopher-Actor-System");
            
            IActorRef report = system.ActorOf<Report>("Report");

            IActorRef fork1 = system.ActorOf(Props.Create(() => new Fork(1, report)));
            IActorRef fork2 = system.ActorOf(Props.Create(() => new Fork(2, report)));
            IActorRef fork3 = system.ActorOf(Props.Create(() => new Fork(3, report)));
            IActorRef fork4 = system.ActorOf(Props.Create(() => new Fork(4, report)));
            IActorRef fork5 = system.ActorOf(Props.Create(() => new Fork(5, report)));

            IActorRef plato = system.ActorOf(Props.Create(() => new Philosopher("Plato", fork1, fork2, report)));
            IActorRef confucius = system.ActorOf(Props.Create(() => new Philosopher("Confucius", fork2, fork3, report)));
            IActorRef galileo = system.ActorOf(Props.Create(() => new Philosopher("Galileo", fork3, fork4, report)));
            IActorRef socrates = system.ActorOf(Props.Create(() => new Philosopher("Socrates", fork4, fork5, report)));
            IActorRef aristotle = system.ActorOf(Props.Create(() => new Philosopher("Aristotle", fork5, fork1, report)));
            Console.Read();
        }
    }
}
