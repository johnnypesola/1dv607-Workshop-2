using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workshop2.Model;
using Workshop2.Model.BLL;


namespace Workshop2
{
    class BoatClubApp
    {
        public static string[] members;
        public static string key;
        private List<Member> _members;

        //Constructor
        public BoatClubApp()
        {
            MemberService ms = new MemberService();
            _members = ms.MemberList;
        }
        public void generateMenu()
        {
            members = new string[] { "Matt", "Joanne", "Robert" };
            do
            {
                PrintMainMenu();
                SetKey();
                
                if (ReadMainMenuChoise(key))
                {
                    do
                    {
                        switch (key)
                        {
                            case "1":
                                PrintCompactList();
                                ChooseMember();
                                break;
                            case "2":
                                PrintVerboseList();
                                break;
                            case "3":
                                AddNewMember();
                                break;
                            default:
                                Console.WriteLine("Invalid choise, please pick again or choose B to go back");
                                break;
                        }
                       SetKey();
                    }
                    while (key.ToUpper() != "B");
                    
                }
            }
            while (key.ToUpper() != "X");
            Console.WriteLine("Closing the application. Press any key to close.");
            Console.ReadKey();
        }

        private static void ChooseMember()
        {
            SetKey();
            for (int i = 1; i <= members.Length; i++)
            {
                if (key == i.ToString())
                {
                    Console.WriteLine("Okej, du valde nummer {0} som heter {1}", i, members[i-1]);
                }
            }
        }
        private static void PrintSingleMember(int memberID){

        }
        private void ReadInput(string key)
        {

        }
        static void PrintMainMenu()
        {
            Console.WriteLine("Welcome to the happy boat club!");
            Console.WriteLine("1. View compact list");
            Console.WriteLine("2. View verbose list");
            Console.WriteLine("3. Add new member");
            Console.WriteLine("X. Exit the application");
        }
        static bool ReadMainMenuChoise(string key)
        {
            if (key == "1")
            {
                Console.WriteLine("You choosed Compact List");
                return true;
            }
            if (key == "2")
            {
                Console.WriteLine("You choosed Verbose List");
                return true;
            }
            if (key == "3")
            {
                Console.WriteLine("You choosed Add new member");
                return true;
            }
            return false;

        }
        static void SetKey()
        {
            key = Console.ReadLine().ToString();
        }
        private void PrintCompactList()
        {
            Console.WriteLine("All members:");
            int memberCount = 1;
            foreach (Member member in _members){
                Console.WriteLine("{0}. {1}, {2}", memberCount, member.Name, member.PersonalNumber);
                memberCount++;
            }
            Console.WriteLine("Pick a member or choose B to go back");
        }
        private void PrintVerboseList()
        {
            Console.WriteLine("Verbose List");
            int memberCount = 1;
            foreach (Member member in _members)
            {
                Console.WriteLine("{0}. {1}, {2}", memberCount, member.Name, member.PersonalNumber);
                foreach (Boat boat in member.Boats)
                {
                    Console.WriteLine(boat.BoatType);
                }
                memberCount++;
            }

        }
        static void AddNewMember()
        {
            Console.WriteLine("Add new member");
            Console.WriteLine("Ange namn");
            string namn = Console.ReadLine().ToString();
            //Validate name
            if (true)
            {
                Console.WriteLine("Du angav namn: {0}", namn);
            }
            else
            {
                Console.WriteLine("Ogiltigt format på namn: {0}", namn);
            }
        }
    }
}