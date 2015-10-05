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
                                ChooseMember();
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

        private void ChooseMember()
        {
            SetKey();
            for (int i = 1; i <= _members.Count; i++)
            {
                if (key == i.ToString())
                {
                    printMemberInfo(_members[i-1]);
                    PrintIndividialMenu();
                    ReadIndividualMenu();
                }
            }
        }
        private void PrintMainMenu()
        {
            Console.WriteLine("Welcome to the happy boat club!");
            Console.WriteLine("1. View compact list");
            Console.WriteLine("2. View verbose list");
            Console.WriteLine("3. Add new member");
            Console.WriteLine("X. Exit the application");
        }
        private void PrintIndividialMenu()
        {
            Console.WriteLine("Press:");
            Console.WriteLine("C to change member info");
            Console.WriteLine("B to go back");
            Console.WriteLine("F to add boat");
            Console.WriteLine("Or type boat number to change boat");
        }
        private void ReadIndividualMenu()
        {
            SetKey();

            if (key.ToUpper() == "C")
            {
                Console.WriteLine("Change member info");
            }
            else if (key.ToUpper() == "F")
            {
                Console.WriteLine("Add boat");
            }
        }
        static bool ReadMainMenuChoise(string key)
        {
            if (key == "1")
            {
                Console.WriteLine("You choosed Compact List");
                return true;
            }
            else if (key == "2")
            {
                Console.WriteLine("You choosed Verbose List");
                return true;
            }
            else if (key == "3")
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
                int boatCount = 1;
                foreach (Boat boat in member.Boats)
                {
                    Console.WriteLine(boat.BoatType);
                    boatCount++;
                }
                memberCount++;
            }

        }
        private void AddNewMember()
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
        private void printMemberInfo(Member m)
        {
            Console.WriteLine(m.Name);
            Console.WriteLine(m.PersonalNumber);
            Console.WriteLine("Boats:");
            int boatCount = 1;
            foreach (Boat b in m.Boats)
            {
                Console.WriteLine("{0}. {1}, {2} meters long", boatCount, b.BoatType, b.BoatLength);
                boatCount++;
            }
            if (m.Boats.Count == 0)
            {
                Console.WriteLine("This person has no boats");
            }
        }
        private void changeMember()
        {

        }
    }
}