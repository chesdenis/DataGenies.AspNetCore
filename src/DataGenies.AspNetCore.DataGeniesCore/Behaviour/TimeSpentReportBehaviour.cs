﻿using System;
using System.Diagnostics;
using DataGenies.AspNetCore.DataGeniesCore.Abstraction;
using DataGenies.AspNetCore.DataGeniesCore.Abstraction.Behaviour;

namespace DataGenies.AspNetCore.DataGeniesCore.Behaviour
{
    public class TimeSpentReportBehaviour : GenericBehaviour
    {
        public override BehaviourType Type { get; set; } = BehaviourType.DuringRun;

        private Stopwatch sw = new Stopwatch();

        public override void ExecuteAction(Action action)
        {
            sw.Restart();
            sw.Start();

            action();

            sw.Stop();
            Console.WriteLine($"Elapsed {sw.ElapsedMilliseconds}ms...");
        }

        public override void ExecuteException(Exception ex = null)
        {
            throw new NotImplementedException();
        }

        public override void ExecuteScalar()
        {
            throw new NotImplementedException();
        }
    }
}