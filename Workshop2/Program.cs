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
            //Model.DAL.DALBase.SetupTables();



            MemberService memberService = new MemberService();


            List<Member> members = memberService.GetAllMembers();

            // Add boat to member
            //memberService.SaveBoat(members[0], new Boat { BoatType = "Canoe", Length = 12, MemberId = members[0].Id });
//            memberService.SaveBoat(members[0], new Boat { BoatType = "Motorsailer", Length = 12, MemberId = members[0].Id });
            
            // 
            members = memberService.GetAllMembers();

            foreach (Member member in members)
            {
                Console.WriteLine("{0}, {1}", member.Name, member.Boats.Count);
            }

            Boat boatToModify = members[0].Boats[0];

            //boatToModify.Length = 999;

            //memberService.SaveBoat(members[0], boatToModify);

            memberService.DeleteBoat(members[0], boatToModify);

            Console.ReadLine();



            //BoatClubApp.generateMenu();
        }
    }
}
