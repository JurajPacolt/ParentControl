﻿/* Created on 1.10.2016 */
using log4net;
using ParentControl.DAO;
using ParentControl.ObjectModel;
using Quartz;
using System;
using ParentControl.Business;
using System.Collections.Generic;
using System.Linq;

namespace ParentControl
{
    /// <summary>
    /// Main job for scheduling.
    /// </summary>
    public class ParentControlScheduledJob : IJob
    {
        public const int MILISECONDS_PER_MINUTE = 60000;

        protected ILog logger = LogManager.GetLogger(typeof(ParentControlScheduledJob));

        private BaseSettingsBusiness baseSettings = new BaseSettingsBusiness();

        private static long? timeNow = null;
        private volatile static Object synobj = new Object();

        /// <summary>
        /// </summary>
        /// <param name="context"></param>
        public void Execute(Quartz.IJobExecutionContext context)
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
                bool canContinue = CanContinue(now, ov, rules);


                // If we can not continue, shutdown computer ...
                if (!canContinue)
                {
                    logger.Info("Condition is not truthfully, run command for shutdown ...");

                    BaseSettings bs = baseSettings.GetBaseSettings();
                    if (bs.ShutdownCommand != null && bs.ShutdownCommand.Length > 0)
                    {
                        System.Diagnostics.Process process =
                            System.Diagnostics.Process.Start("cmd", "/C \"" + bs.ShutdownCommand + "\"");
                        // TODO Do something with process outputs.
                    }
                }
            }
        }

        /// <summary>
        /// Checking whether for can be shutting down or not. True value is No :).
        /// </summary>
        /// <param name="now"></param>
        /// <param name="rules"></param>
        /// <param name="ov"></param>
        /// <returns></returns>
        public bool CanContinue(DateTime now, ObservedValues ov, Rule[] rules)
        {
            List<Rule> list = rules.OfType<Rule>().ToList().FindAll(rule => rule.Enabled);
            List<Rule> negativeRules = list.Where(rule =>
                (rule.DayOfWeek != null && rule.DayOfWeek != (int)now.DayOfWeek)
                || (rule.FromDateTime != null && now.CompareTo(rule.FromDateTime) <= 0)
                || (rule.ToDateTime != null && now.CompareTo(rule.ToDateTime) >= 0)
                || (rule.DurationInMinutes != null && ov.Duration != null && ov.Duration >= ((long)(rule.DurationInMinutes * MILISECONDS_PER_MINUTE)))).ToList();
            negativeRules.ForEach(rule => {
                list.Remove(rule);
            });
            return (list.Count > 0);
        }

    }

}
