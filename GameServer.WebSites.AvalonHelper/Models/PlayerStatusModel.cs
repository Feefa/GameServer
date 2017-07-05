using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.WebSites.AvalonHelper
{
    public class PlayerStatusModel
    {
        public int RoleId { get; set; }

        public string UserId { get; set; }

        public string GetStatusText()
        {
            switch (RoleId)
            {
                case -1: return "Waiting ...";
                case 0: return "Selecting Role";
                default: return "Active";
            }
        }

        public string GetStatusClass()
        {
            switch (RoleId)
            {
                case -1: return "";
                case 0: return "warning";
                default: return "active";
            }
        }
    }
}
