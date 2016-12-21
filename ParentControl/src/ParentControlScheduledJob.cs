﻿/* Created on 1.10.2016 */
using log4net;
using ParentControl.DAO;
using ParentControl.ObjectModel;
using Quartz;
using System;

namespace ParentControl
{
    /// <summary>
    /// Main job for scheduling.
    /// </summary>
    public class ParentControlScheduledJob : IJob
    {
        public const int MILISECONDS_PER_MINUTE = 60000;

        protected ILog logger = LogManager.GetLogger(typeof(ParentControlScheduledJob));

        private static long? timeNow = null;
        private volatile static Object synobj = new Object();

        /// <summary>
        /// </summary>
        /// <param name="context"></param>
        public void Execute(IJobExecutionContext context)
        {
            long now = (DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond);
            if (timeNow != null)
            {
                long diff = (now - timeNow.Value);
                timeNow = now;

                if (diff > 0)
                {
                    logger.Debug("Scheduler job executed ... ");
                    doSomething(diff);
                }
            }
            else
            {
                timeNow = now;
            }
        }

        #pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        private async void doSomething(long diff)
        #pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            lock (synobj)
            {
                ObservedValuesDAO observedValuesDao = new ObservedValuesDAO();
                ObservedValues ov = observedValuesDao.GetObservedValues();
                DateTime now = DateTime.Now;

                // If at least one of the rules is true, code is here.
                ov.Duration = (ov.ActualDate != null && ov.Duration != null && ov.ActualDate.Value.Date.Equals(now.Date)) ? (ov.Duration = ov.Duration + diff) : diff;
                ov.ActualDate = now.Date;
                observedValuesDao.StoreObservedValues(ov);

                logger.Debug("Duration ... " + ov.Duration);

                // We need list of the rules. And control it.
                RulesDAO rulesDao = new RulesDAO();
                Rule[] rules = rulesDao.ListRules();
                bool canContinue = true;
                foreach (Rule rule in rules)
                {
                    if (!rule.Enabled) {
                        continue;
                    }

                    if (rule.DayOfWeek != null && rule.DayOfWeek.Value != (int)now.DayOfWeek) {
                        canContinue = false;
                    }

                    if (rule.FromDateTime != null && !(rule.FromDateTime.Value.CompareTo(now) <= 0)) {
                        canContinue = false;
                    }

                    if (rule.ToDateTime != null && !(rule.ToDateTime.Value.CompareTo(now) >= 0)) {
                        canContinue = false;
                    }

                    if (rule.DurationInMinutes != null && ov.Duration != null && !((long)(rule.DurationInMinutes * MILISECONDS_PER_MINUTE) >= ov.Duration)) {
                        canContinue = false;
                    }
                }


                // If we can not continue, shutdown computer ...
                if (!canContinue)
                {
                    // TODO ...
                    logger.Info("Condition is not truthfully, run command for shutdown ...");
                }
            }
        }

    }

}