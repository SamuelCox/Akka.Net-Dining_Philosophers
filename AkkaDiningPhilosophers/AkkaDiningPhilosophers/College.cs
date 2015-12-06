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

            IActorRef fork1 = system.ActorOf(Props.Create(() => new Fork(1, report)), "ForkOne");
            IActorRef fork2 = system.ActorOf(Props.Create(() => new Fork(2, report)), "ForkTwo");
            IActorRef fork3 = system.ActorOf(Props.Create(() => new Fork(3, report)), "ForkThree");
            IActorRef fork4 = system.ActorOf(Props.Create(() => new Fork(4, report)), "ForkFour");
            IActorRef fork5 = system.ActorOf(Props.Create(() => new Fork(5, report)), "ForkFive");

            IActorRef plato = system.ActorOf(Props.Create(() => new Philosopher("Plato", fork1, fork2, report)), "Plato");
            IActorRef confucius = system.ActorOf(Props.Create(() => new Philosopher("Confucius", fork2, fork3, report)), "Confucius");
            IActorRef galileo = system.ActorOf(Props.Create(() => new Philosopher("Galileo", fork3, fork4, report)), "Galileo");
            IActorRef socrates = system.ActorOf(Props.Create(() => new Philosopher("Socrates", fork4, fork5, report)), "Socrates");
            IActorRef aristotle = system.ActorOf(Props.Create(() => new Philosopher("Aristotle", fork5, fork1, report)), "Aristotle");
            Console.Read();
        }
    }
}
