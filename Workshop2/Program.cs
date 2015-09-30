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

            // Uncomment this if you want to reset all the data in database to defaults.
            //Model.DAL.DALBase.SetupTables();

            // Create new member controller, it should do stuff with members
            memberController = new MemberController();
        }
    }
}
