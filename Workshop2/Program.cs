using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workshop2.Controller;

namespace Workshop2
{
    class Program
    {
        static void Main(string[] args)
        {
            MemberController memberController;

            // Setup tables. WARNING: All data in DB tables are reset to defaults.
            Model.DAL.DALBase.SetupTables();


            // Create new member controller, it should do stuff with members
            memberController = new MemberController();
        }
    }
}
