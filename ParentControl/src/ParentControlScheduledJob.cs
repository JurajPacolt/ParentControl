/* Created on 1.10.2016 */
using log4net;
using ParentControl.DAO;
using ParentControl.ObjectModel;
using Quartz;
using System;
using ParentControl.Business;

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
                    if (bs.ShutdownCommand != null && bs.ShutdownCommand.Length > 0 && !ParentControlFactory.Instance.FlagCommandWasAlreadyExecuted)
                    {
                        //System.Diagnostics.Process.Start("cmd", "/C \"" + bs.ShutdownCommand + "\"");
                        ParentControlFactory.Instance.FlagCommandWasAlreadyExecuted = true;
                    }
                    else if (ParentControlFactory.Instance.FlagCommandWasAlreadyExecuted)
                    {
                        logger.Info("Attention, command is already running, is waiting time for done.");
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
            foreach (Rule rule in rules)
            {
                if (!rule.Enabled)
                {
                    continue;
                }

                // FIXME conditions are bad ... fix it

                if (rule.DayOfWeek != null && rule.DayOfWeek != (int)now.DayOfWeek)
                {
                    return false;
                }

                if (rule.FromDateTime != null && now.CompareTo(rule.FromDateTime) <= 0)
                {
                    return false;
                }

                if (rule.ToDateTime != null && now.CompareTo(rule.ToDateTime) >= 0)
                {
                    return false;
                }

                if (rule.DurationInMinutes != null && ov.Duration != null && ov.Duration >= ((long)(rule.DurationInMinutes * MILISECONDS_PER_MINUTE)))
                {
                    return false;
                }
            }

            return true;
        }

    }

}
