//using SchedulerManager.Mechanism;
using AFIS360.Scheduler;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFIS360
{
    class FingerprintTemplateLoaderJob : Job
    {
        /// <summary>
        /// Counter used to count the number of times this job has been
        /// executed.
        /// </summary>
        private int counter = 0;

        /// <summary>
        /// Get the Job Name, which reflects the class name.
        /// </summary>
        /// <returns>The class Name.</returns>
        public override string GetName()
        {
            return this.GetType().Name;
        }

        /// <summary>
        /// Execute the Job itself. Just print a message.
        /// </summary>
        public override void DoJob()
        {
            System.Console.WriteLine(String.Format("This is the execution number \"{0}\" of the Job \"{1}\".", counter.ToString(), this.GetName()) + " Current Time: " + DateTime.Now);
            counter++;

            //load the all fingerprint templates from the Database
            Program.loadFingerptintTemplates();

        }

        /// <summary>
        /// Determines this job is repeatable.
        /// </summary>
        /// <returns>Returns true because this job is repeatable.</returns>
        public override bool IsRepeatable()
        {
            return true;
        }

        /// <summary>
        /// Determines that this job is to be executed again after
        /// 1 sec.
        /// </summary>
        /// <returns>1 sec, which is the interval this job is to be
        /// executed repeatadly.</returns>
        public override int GetRepetitionIntervalTime()
        {
            int intervalTime = 0;
            if(AFISMain.clientSetup != null)
            {
                intervalTime = AFISMain.clientSetup.DataRefreshInterval;
                if(intervalTime > 0)
                {
                    intervalTime = intervalTime * 3600 * 1000;
                } else
                {
                    intervalTime = Convert.ToInt32(ConfigurationManager.AppSettings["FingerprintTemplateLoaderDefaultInterval"]);
                }
            } else
            {
                intervalTime = Convert.ToInt32(ConfigurationManager.AppSettings["FingerprintTemplateLoaderDefaultInterval"]);
            }
            return intervalTime;
        }
    }
}

