using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Test.ViewModels.Base;

namespace Test.Services.GLUpdater
{
    internal class UpdateChecker : ViewModel
    {
        public Version Version { get; private set; }
        private Response updater;
        private IResponse response1;

        public UpdateChecker(IResponse response)
        {
            response1 = response;
        }
        private async Task<string> GetAsync()
        {
            return await response1.RequestAsync( new Uri("https://raw.githubusercontent.com/Sereoj/uploads/main/ds_new/version.txt"));
        }
    }
}
