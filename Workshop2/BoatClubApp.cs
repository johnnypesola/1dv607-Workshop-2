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
    class BoatClubApp
    {
        public static string key;
        private MemberService _memberService;
        private MenuView _menuView;

        // Constructor
        public BoatClubApp()
        {
            _memberService = new MemberService();
            _menuView = new MenuView();
        }

        public void generateMenu()
        {
            PrintMainMenu();
        }

        /*
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
         */ 

        private void PrintMainMenu()
        {
            MenuContainer mainMenu = new MenuContainer("Welcome to the happy boat club!");

            mainMenu.menuItems.Add(new MenuItem("1", "View compact list", PrintCompactList));
            mainMenu.menuItems.Add(new MenuItem("2", "View verbose list", PrintVerboseList));
            mainMenu.menuItems.Add(new MenuItem("3", "Add new member", AddNewMember));

            _menuView.PrintMenu(mainMenu);
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


        static void SetKey()
        {
            key = Console.ReadKey().KeyChar.ToString();
        }
        private void PrintCompactList()
        {
            MenuContainer compactListMenu = new MenuContainer("Compact list");

            int memberCount = 1;
            foreach (Member member in _memberService.MemberList){

                compactListMenu.menuItems.Add(
                    new MenuItem(
                        memberCount.ToString(),
                        String.Format("{0}, {1}", member.Name, member.PersonalNumber),
                        printMemberInfo,
                        member
                    )
                );

                memberCount++;
            }

            compactListMenu.footer = "Pick a member.";

            _menuView.PrintMenu(compactListMenu);
        }
        private void PrintVerboseList()
        {
            _menuView.PrintHeader("Verbose List");
            int memberCount = 1;
            foreach (Member member in _memberService.MemberList)
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
            Console.WriteLine("Enter name:");
            string namn = Console.ReadLine().ToString();
            //Validate name
            if (true)
            {
                Console.WriteLine("You entered name: {0}", namn);
            }
            else
            {
                Console.WriteLine("Invalid format on name: {0}", namn);
            }
        }
        private void printMemberInfo(object member)
        {
            // Cast object type to member
            Member m = (Member)member;

            MenuContainer memberInfoMenu = new MenuContainer(m.Name);

            _menuView.PrintHeader(m.Name);

            memberInfoMenu.textLines.Add(String.Format("Personal number: {0}", m.PersonalNumber));
            memberInfoMenu.textLines.Add("");
            memberInfoMenu.textLines.Add("Boats:");

            int boatCount = 1;
            foreach (Boat b in m.Boats)
            {
                memberInfoMenu.menuItems.Add(
                    new MenuItem(
                        boatCount.ToString(),
                        String.Format("{0}, {1} meters long", b.BoatType, b.BoatLength),
                        printMemberBoat,
                        b
                    )
                );

                boatCount++;
            }
            if (m.Boats.Count == 0)
            {
                memberInfoMenu.footer = "This person has no boats.";
            }

            _menuView.PrintMenu(memberInfoMenu);
        }

        private void printMemberBoat(object boat)
        {
            // Cast object type to boat
            Boat b = (Boat)boat;

            Member m = _memberService.GetMember(b.MemberId);

            MenuContainer memberBoatMenu = new MenuContainer(m.Name);

            memberBoatMenu.textLines.Add(String.Format("Boat type: {0}", b.BoatType));
            memberBoatMenu.textLines.Add(String.Format("Length: {0}", b.BoatLength));


            memberBoatMenu.menuItems.Add(new MenuItem("D", "Delete boat", DeleteMemberBoat, b, 2));
            memberBoatMenu.menuItems.Add(new MenuItem("T", "Change boat type", printEditBoatType, b, 2));
            memberBoatMenu.menuItems.Add(new MenuItem("L", "Change length", printEditBoatLength, b, 2));

            _menuView.PrintMenu(memberBoatMenu);
        }

        private void printEditBoatLength(object boat)
        {
            Boat b = (Boat)boat;

            b.BoatLength = decimal.Parse(_menuView.GetUserInputLine("Enter new boat length"));
        }

        private void printEditBoatType(object boat)
        {
            Boat b = (Boat)boat;

            MenuContainer memberBoatMenu = new MenuContainer("Choose new boat type");

            memberBoatMenu.menuItems.Add(new MenuItem("S", "Sailboat"));
            memberBoatMenu.menuItems.Add(new MenuItem("M", "Motorsailer"));
            memberBoatMenu.menuItems.Add(new MenuItem("C", "Canoe"));
            memberBoatMenu.menuItems.Add(new MenuItem("O", "Other"));

            switch (_menuView.GetUserOption(memberBoatMenu))
            {
                case "S": b.BoatType = "Sailboat";
                    break;
                case "M": b.BoatType = "Motorsailer";
                    break;
                case "C": b.BoatType = "Canoe";
                    break;
                case "O": b.BoatType = "Other";
                    break;
            }
        }


        private void DeleteMemberBoat(object boat)
        {
            Boat b = (Boat)boat;

            _memberService.DeleteBoat(b.MemberId, b);
        }


        private void changeMember()
        {

        }
    }
}