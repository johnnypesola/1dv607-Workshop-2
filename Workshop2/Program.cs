using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workshop2.Model;
using Workshop2.Model.BLL;
using Workshop2.View;

namespace Workshop2
{
    class Program
    {
        static void Main(string[] args)
        {

            // Uncomment this if you want to reset all the data in database to defaults.
            Model.DAL.DALBase.SetupTables(); // TODO: dont setup tables

            // Create and run program
            BoatClubController bc = new BoatClubController();

            bc.GenerateMenu();
        }
    }
}
