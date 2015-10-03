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
        static void Main(string[] args)
        {

            // Uncomment this if you want to reset all the data in database to defaults.
            Model.DAL.DALBase.SetupTables();

            BoatClubApp.generateMenu();
        }
    }
}
