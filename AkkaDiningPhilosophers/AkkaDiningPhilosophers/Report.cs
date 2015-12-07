﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using System.Threading;
namespace AkkaDiningPhilosophers
{
    class Report : ReceiveActor
    {
        public Report()
        {
            Receive<string>((message) =>
                {
                    Thread.Sleep(1000);
                    Console.WriteLine(message);
                });
                
        }

        
    }
}
