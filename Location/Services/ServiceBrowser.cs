using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Location.Models; using Location.DAL;

namespace Location.Services
{
    public class ServiceBrowser
    {
        /// <summary>
        /// Return list of browser model
        /// </summary>
        public List<BrowserModel> ListOfBrowsers()
        {
            var lst = new List<BrowserModel>
            {
               };

            return lst;
        }

    }
}
