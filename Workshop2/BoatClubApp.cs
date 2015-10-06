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

        private void PrintMainMenu()
        {
            MenuContainer menu = new MenuContainer("Welcome to the happy boat club!");

            menu.menuItems.Add(new MenuItem("1", "View compact list", PrintCompactList));
            menu.menuItems.Add(new MenuItem("2", "View verbose list", PrintVerboseList));
            menu.menuItems.Add(new MenuItem("3", "Add new member", AddNewMember));

            _menuView.PrintMenu(menu);
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
            Member m =  new Member();

            m.Name = _menuView.GetUserInputLine("Enter new member name");

            m.PersonalNumber = _menuView.GetUserInputLine("Enter new member personal number");

            _memberService.SaveMember(m);

            /*
            
            //Validate name
            if (true)
            {
                Console.WriteLine("You entered name: {0}", namn);
            }
            else
            {
                Console.WriteLine("Invalid format on name: {0}", namn);
            }*/

        }
        private void printMemberInfo(object member)
        {
            // Cast object type to member
            Member m = (Member)member;

            MenuContainer menu = new MenuContainer(m.Name);

            _menuView.PrintHeader(m.Name);

            menu.textLines.Add(String.Format("Personal number: {0}", m.PersonalNumber));
            menu.textLines.Add("");
            menu.textLines.Add("Boats:");

            int boatCount = 1;
            foreach (Boat b in m.Boats)
            {
                menu.menuItems.Add(
                    new MenuItem(
                        boatCount.ToString(),
                        String.Format("{0}, {1} meters long", b.BoatType, b.BoatLength),
                        printMemberBoat,
                        b
                    )
                );

                boatCount++;
            }

            menu.menuItems.Add(new MenuItem("A", "Add boat", printAddBoat, m, 1));
            menu.menuItems.Add(new MenuItem("D", "Delete this member", deleteMember, m, 2));

            menu.footer = m.Boats.Count > 0 ? "Pick a boat, add a new one or edit member." : "This person has no boats.";

            _menuView.PrintMenu(menu);
        }

        private void deleteMember(object member)
        {
            Member m = (Member)member;
            _memberService.DeleteMember(m);
        }

        private void changeMember()
        {

        }

        private void printAddBoat(object member)
        {
            Member m = (Member)member;
            Boat b = new Boat();

            b.BoatLength = decimal.Parse(_menuView.GetUserInputLine("Enter new boat length"));

            MenuContainer menu = new MenuContainer("Choose new boat type");

            menu.menuItems.Add(new MenuItem("S", "Sailboat"));
            menu.menuItems.Add(new MenuItem("M", "Motorsailer"));
            menu.menuItems.Add(new MenuItem("C", "Canoe"));
            menu.menuItems.Add(new MenuItem("O", "Other"));

            switch (_menuView.GetUserOption(menu))
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

            _memberService.SaveBoat(m, b);
        }

        private void printMemberBoat(object boat)
        {
            // Cast object type to boat
            Boat b = (Boat)boat;

            Member m = _memberService.GetMember(b.MemberId);

            MenuContainer menu = new MenuContainer(m.Name);

            menu.textLines.Add(String.Format("Boat type: {0}", b.BoatType));
            menu.textLines.Add(String.Format("Length: {0}", b.BoatLength));


            menu.menuItems.Add(new MenuItem("D", "Delete boat", deleteMemberBoat, b, 2));
            menu.menuItems.Add(new MenuItem("T", "Change boat type", printEditBoatType, b, 2));
            menu.menuItems.Add(new MenuItem("L", "Change length", printEditBoatLength, b, 2));

            menu.footer = "What would you like to do?";

            _menuView.PrintMenu(menu);
        }

        private void printEditBoatLength(object boat)
        {
            Boat b = (Boat)boat;

            b.BoatLength = decimal.Parse(_menuView.GetUserInputLine("Enter new boat length"));
        }

        private void printEditBoatType(object boat)
        {
            Boat b = (Boat)boat;

            MenuContainer menu = new MenuContainer("Choose new boat type");

            menu.menuItems.Add(new MenuItem("S", "Sailboat"));
            menu.menuItems.Add(new MenuItem("M", "Motorsailer"));
            menu.menuItems.Add(new MenuItem("C", "Canoe"));
            menu.menuItems.Add(new MenuItem("O", "Other"));

            switch (_menuView.GetUserOption(menu))
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

        private void deleteMemberBoat(object boat)
        {
            Boat b = (Boat)boat;

            _memberService.DeleteBoat(b.MemberId, b);
        }
    }
}