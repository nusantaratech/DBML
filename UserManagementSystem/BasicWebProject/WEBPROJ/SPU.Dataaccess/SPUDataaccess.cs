using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web.Configuration;
using SPU.Entities;

namespace SPU.Dataaccess
{
    public class SPUDataaccess
    {
        public SPUSystemDataContext SPUDataContent = new SPUSystemDataContext(WebConfigurationManager.ConnectionStrings["connDataStrName"].ToString());
    }
}
