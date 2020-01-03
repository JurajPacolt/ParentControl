/* Created on 12.10.2018 */
using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParentControl.ObjectModel;

namespace ParentControl.UnitTests
{
    /// <summary>
    /// Test class for ParentControlScheduledJob.
    /// </summary>
    [TestClass]
    public class ParentControlScheduledJobTest
    {
        private ParentControlScheduledJob pcsj = new ParentControlScheduledJob();

        [TestMethod]
        public void TestRulesForCanContinue()
        {
            DateTime now = DateTime.ParseExact("05.01.2018 13:45", "dd.MM.yyyy HH:mm", null);

            ObservedValues ov = new ObservedValues();
            ov.ActualDate = now;
            ov.Duration = (58L * 60000L);

            List<Rule> list = new List<Rule>();

            Rule r1 = new Rule();
            r1.Name = "Test 1";
            r1.Enabled = true;
            r1.FromDateTime = DateTime.ParseExact("01.01.2018 00:00", "dd.MM.yyyy HH:mm", null);
            r1.ToDateTime = DateTime.ParseExact("10.01.2018 23:59", "dd.MM.yyyy HH:mm", null);
            r1.DurationInMinutes = 60;
            list.Add(r1);

            Rule r2 = new Rule();
            r2.Name = "Test 2";
            r2.Enabled = true;
            r2.DayOfWeek = (int?)System.DayOfWeek.Friday; // 05.01.2018 is friday
            r2.DurationInMinutes = 60;
            list.Add(r2);

            Assert.IsTrue(pcsj.CanContinue(now, ov, list.ToArray()));
        }
    }
}
