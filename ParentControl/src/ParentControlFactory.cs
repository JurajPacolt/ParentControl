using log4net;
using ParentControl.Business;
using ParentControl.ObjectModel;
using ParentControlCommon;
using Quartz;
using Quartz.Impl;
using System;

namespace ParentControl
{
    /// <summary>
    /// Main functionallity class for ParentControl.
    /// </summary>
    class ParentControlFactory
    {
        protected ILog logger = LogManager.GetLogger(typeof(ParentControlFactory));

        /// <summary>
        /// Static instance of the main factory.
        /// </summary>
        private static ParentControlFactory instance;

        private BaseSettingsBusiness baseSettings = new BaseSettingsBusiness();

        private ISchedulerFactory schedulerFactory = null;
        private IScheduler scheduler = null;
        private IJobDetail job = null;
        private ITrigger trigger = null;

        /// <summary>
        /// Default constructor.
        /// </summary>
        private ParentControlFactory()
        {
            DataAccess.Instance.Init(ParentControl.DATABASE_FILE);

            // When is main instance created, we have to try database updates.
            new ParentControlCommon.Updates.Updater().TryToUpdate();

            InitScheduler();
        }

        private void InitScheduler()
        {
            BaseSettings bs = baseSettings.GetBaseSettings();
            if (bs.CheckInterval != null)
            {
                // We need scheduler factory.
                logger.Info("Creating instance of the scheduler factory.");
                schedulerFactory = new StdSchedulerFactory();

                // Now it will be scheduler.
                logger.Info("Getting scheduler instance from factory.");
                scheduler = (IScheduler)schedulerFactory.GetScheduler();

                // Create job.
                logger.Info("Creating job instance.");
                job = JobBuilder.Create<ParentControlScheduledJob>()
                    .WithIdentity("myJob", "group1")
                    .Build();
            }
        }

        /// <summary>
        /// Starting up method of the main factory instance.
        /// </summary>
        public void Startup()
        {
            logger.Info("Starting up main instance ...");

            if (scheduler != null) {
                logger.Info("Starting scheduler ...");
                BaseSettings bs = baseSettings.GetBaseSettings();
                if (bs.CheckInterval != null)
                {
                    // Create trigger.
                    logger.Info("Creating trigger for job.");
                    trigger = TriggerBuilder.Create()
                        .WithIdentity("myTrigger", "group1")
                        .StartAt(DateTimeOffset.Now.AddSeconds((double)(bs.DelayStart != null ? bs.DelayStart : 0)))
                        .WithSimpleSchedule(x => x
                        .WithIntervalInSeconds(5)
                        .RepeatForever())
                        .Build();

                    logger.Info("Register job and trigger for scheduler.");
                    scheduler.ScheduleJob(job, trigger);

                    scheduler.StartDelayed(TimeSpan.FromSeconds((int)bs.CheckInterval));

                    scheduler.Start();
                }
            }

            ParentControlListener.Instance.Startup();

            logger.Info("Ready ...");
        }

        /// <summary>
        /// Shutting down of the main factory instance.
        /// </summary>
        public void Shutdown()
        {
            if (scheduler != null) {
                logger.Info("Shutting down scheduler ...");
                scheduler.Shutdown(false);
            }

            logger.Info("Shutting down main instance ...");
            ParentControlListener.Instance.Shutdown();

            DataAccess.Instance.Close();
        }

        /// <summary>
        /// Restart scheduler.
        /// </summary>
        public void RestartScheduler()
        {
            BaseSettings bs = baseSettings.GetBaseSettings();

            if (scheduler == null) {
                InitScheduler();
            }

            if (scheduler != null)
            {
                logger.Info("Shutting down scheduler ...");
                scheduler.Interrupt(job.Key);
                scheduler.Standby();
                scheduler.DeleteJob(job.Key);

                if (bs.CheckInterval != null && bs.CheckInterval.Value > 0)
                {
                    logger.Info("Creating trigger for job.");
                    trigger = TriggerBuilder.Create()
                        .WithIdentity("myTrigger", "group1")
                        .StartNow()
                        .WithSimpleSchedule(x => x
                        .WithIntervalInSeconds(bs.CheckInterval.Value)
                        .RepeatForever())
                        .Build();

                    logger.Info("Pre-register job and trigger for scheduler.");
                    scheduler.ScheduleJob(job, trigger);

                    logger.Info("Starting scheduler ...");
                    scheduler.Start();
                }
            }
        }

        /// <summary>
        /// Getter for parent control factory.
        /// </summary>
        public static ParentControlFactory Instance {
            get {
                if (instance == null) {
                    instance = new ParentControlFactory();
                }
                return instance;
            }
        }

    }

}
