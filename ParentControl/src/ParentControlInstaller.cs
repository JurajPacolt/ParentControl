using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace ParentControl
{
    [RunInstaller(true)]
    class ParentControlInstaller : Installer
    {
        private ServiceProcessInstaller serviceProcessInstaller;
        private ServiceInstaller serviceInstaller;

        public ParentControlInstaller()
        {
            serviceProcessInstaller = new ServiceProcessInstaller();
            serviceInstaller = new ServiceInstaller();
            serviceProcessInstaller.Account = ServiceAccount.LocalSystem;
            serviceProcessInstaller.Username = null;
            serviceProcessInstaller.Password = null;

            serviceInstaller.ServiceName = ParentControl.MyServiceName;
            serviceInstaller.Description = ParentControl.MyServiceDescription;
            serviceInstaller.StartType = ServiceStartMode.Automatic;
            this.Installers.AddRange(new Installer[] { serviceProcessInstaller, serviceInstaller });
        }

        public override void Install(IDictionary savedState)
        {
            base.Install(savedState);
        }
    }
}
