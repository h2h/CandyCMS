using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Candy.Plugin.Install.Services
{
    public partial interface IInstallationService
    {
        void InstallData(string email, string password, string username, bool installSampleData = true);
    }
}