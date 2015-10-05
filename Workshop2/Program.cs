using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workshop2.Model;
using Workshop2.Model.BLL;

namespace Workshop2
{
    class Program
    {
        private BoatClubApp _boatClubApp;

        // Properties
        private BoatClubApp BoatClubApp
        {
            // Auto create object if needed
            get
            {
                return _boatClubApp ?? (_boatClubApp = new BoatClubApp());
            }
        }
        static void Main(string[] args)
        {

            // Uncomment this if you want to reset all the data in database to defaults.
            Model.DAL.DALBase.SetupTables();
            BoatClubApp bc = new BoatClubApp();
            bc.generateMenu();
            //MemberController memberController;
            // Uncomment this if you want to reset all the data in database to defaults.
            Model.DAL.DALBase.SetupTables();
            // Create new member controller, it should do stuff with members
            //memberController = new MemberController();
            bc.generateMenu();
        }
    }
}
