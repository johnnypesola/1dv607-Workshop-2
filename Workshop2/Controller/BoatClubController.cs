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
    class BoatClubController
    {
        private MemberService _memberService;
        private MenuView _menuView;

        // Constructor
        public BoatClubController()
        {
            _memberService = new MemberService();
            _menuView = new MenuView();
        }

        public void GenerateMenu()
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

        private void PrintCompactList()
        {
            MenuContainer menu = new MenuContainer("Compact list");

            int memberCount = 1;
            foreach (Member member in _memberService.MemberList){

                menu.menuItems.Add(
                    new MenuItem(
                        memberCount.ToString(),
                        String.Format("{0}, {1}", member.Name, member.PersonalNumber),
                        PrintMemberInfo,
                        member
                    )
                );
                memberCount++;
            }

            menu.footer = "Pick a member.";

            _menuView.PrintMenu(menu);
        }
        private void PrintVerboseList()
        {
            // TODO, add boats to verbose list - how?

            MenuContainer menu = new MenuContainer("Verbose List");
            _menuView.PrintHeader("Verbose List");
            int memberCount = 1;
            foreach (Member member in _memberService.MemberList)
            {
                menu.menuItems.Add( new MenuItem(memberCount.ToString(),string.Format("{0}, {1}", member.Name, member.PersonalNumber),PrintMemberInfo,member));
                foreach (Boat b in member.Boats)
                {
                    menu.menuItems.Add(new MenuItem(String.Format("{0}, {1} meters long", b.BoatType, b.BoatLength)));
                }
                memberCount++;
            }
            menu.footer = "Pick a member.";

            _menuView.PrintMenu(menu);
            

        }
        private void AddNewMember()
        {
            Member m =  new Member();

            m.Name = _menuView.GetUserInputLine("Enter new member name");

            m.PersonalNumber = _menuView.GetUserInputLine("Enter new member personal number");

            _memberService.SaveMember(m);


            // TODO Validate input. Catch errors somewhere? New error template in MenuView?
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
        private void PrintMemberInfo(object member)
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
                        PrintMemberBoat,
                        b
                    )
                );

                boatCount++;
            }

            menu.menuItems.Add(new MenuItem("A", "Add boat", PrintAddBoat, m, 1));
            menu.menuItems.Add(new MenuItem("N", "Change name", PrintEditMemberName, m, 2));
            menu.menuItems.Add(new MenuItem("P", "Change personal number", PrintEditPersonalNumber, m, 2)); 
            menu.menuItems.Add(new MenuItem("D", "Delete this member", DeleteMemberMenu, m));

            menu.footer = m.Boats.Count > 0 ? "Pick a boat, add a new one or edit member." : "This person has no boats.";

            _menuView.PrintMenu(menu);
        }

        private void DeleteMember(object member)
        {
            Member m = (Member)member;
            _memberService.DeleteMember(m);
        }
        private void DeleteMemberMenu(object member)
        {
            Member m = (Member)member;
            MenuContainer menu = new MenuContainer("Confirm your choise");

            menu.menuItems.Add(new MenuItem("Y", string.Format("Do you really want to delete {0}", m.Name), DeleteMember,member, 3));

            _menuView.PrintMenu(menu);

        }

        private void PrintAddBoat(object member)
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

        private void PrintMemberBoat(object boat)
        {
            // Cast object type to boat
            Boat b = (Boat)boat;

            Member m = _memberService.GetMember(b.MemberId);

            MenuContainer menu = new MenuContainer(m.Name);

            menu.textLines.Add(String.Format("Boat type: {0}", b.BoatType));
            menu.textLines.Add(String.Format("Length: {0}", b.BoatLength));


            menu.menuItems.Add(new MenuItem("D", "Delete boat", DeleteMemberBoat, b, 2));
            menu.menuItems.Add(new MenuItem("T", "Change boat type", PrintEditBoatType, b, 2));
            menu.menuItems.Add(new MenuItem("L", "Change length", PrintEditBoatLength, b, 2));

            menu.footer = "What would you like to do?";

            _menuView.PrintMenu(menu);
        }
        private void PrintEditMemberName(object member)
        {
            Member m = (Member)member;

            m.Name = _menuView.GetUserInputLine("Enter new name");
        }
        private void PrintEditPersonalNumber(object member)
        {
            Member m = (Member)member;
            m.PersonalNumber = _menuView.GetUserInputLine("Enter new personal number in the format NNNNNN-NNNN");
        }
        private void PrintEditBoatLength(object boat)
        {
            Boat b = (Boat)boat;

            b.BoatLength = decimal.Parse(_menuView.GetUserInputLine("Enter new boat length"));
        }

        private void PrintEditBoatType(object boat)
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

        private void DeleteMemberBoat(object boat)
        {
            Boat b = (Boat)boat;

            _memberService.DeleteBoat(b.MemberId, b);
        }
    }
}