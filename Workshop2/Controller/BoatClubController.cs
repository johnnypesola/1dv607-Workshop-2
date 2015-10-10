using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        private List<String> _errorMessages;

        // Constructor
        public BoatClubController()
        {
            _memberService = new MemberService();
            _menuView = new MenuView();
            _errorMessages = new List<String>();
        }

       
        private bool Validate(object objectToValidate)
        {
            // Setup
            ValidationContext validationContext = new ValidationContext(objectToValidate, serviceProvider: null, items: null);
            List<ValidationResult> validationResults = new List<ValidationResult>();

            // Validate
            var isValid = Validator.TryValidateObject(objectToValidate, validationContext, validationResults, true);

            // Update error messages
            foreach (ValidationResult validationResult in validationResults)
            {
                _errorMessages.Add(validationResult.ErrorMessage);
            }

            return isValid;
        }
        
        private void ShowErrorMessages()
        {
            String output = "";

            foreach (String message in _errorMessages)
            {
                output = String.Format("{0} → {1} {2}", output, message, Environment.NewLine);
            }

            _menuView.PrintError(output);

            // Reset error messages
            _errorMessages.Clear();
        }


        public void GenerateMenu()
        {
            try
            {
                PrintMainMenu();
            }
            catch (Exception e)
            {
                // Print out error
                _menuView.PrintError(e.Message);
            }
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

            foreach (Member member in _memberService.MemberList){

                menu.menuItems.Add(
                    new MenuItem(
                        member.Id.ToString(),
                        String.Format("{0} ({1}) {2} Boats", member.Name, member.PersonalNumber, member.Boats.Count),
                        PrintMemberInfo,
                        member
                    )
                );
            }

            menu.footer = "Pick a member.";

            _menuView.PrintMenu(menu);
        }
        private void PrintVerboseList()
        {

            MenuContainer menu = new MenuContainer("Verbose List");

            foreach (Member member in _memberService.MemberList)
            {
                menu.menuItems.Add(
                    new MenuItem(
                        member.Id.ToString(),
                        string.Format("{0} ({1})", member.Name, member.PersonalNumber),
                        PrintMemberInfo,
                        member
                    )
                );

                foreach (Boat b in member.Boats)
                {
                    menu.menuItems.Add(new MenuItem(String.Format(" → {0} {1} meters long", b.BoatType, b.BoatLength)));
                }
            }
            menu.footer = "Pick a member.";

            _menuView.PrintMenu(menu);
        }

        private void AddNewMember()
        {
            Member m =  new Member();

            m.Name = _menuView.GetUserInputLine("Enter new member name");
            m.PersonalNumber = _menuView.GetUserInputLine("Enter new member personal number in the format YYMMDD-NNNN.");

            // Validate input
            if (!Validate(m))
            {
                ShowErrorMessages();
            }
            else
            {
                // Try to save input
                try
                {
                    _memberService.SaveMember(m);
                }
                catch (Exception e)
                {
                    // Print out error
                    _menuView.PrintError(e.Message);
                }
            }
        }
        private void PrintMemberInfo(object member)
        {
            // Cast object type to member
            Member m = (Member)member;

            MenuContainer menu = new MenuContainer(String.Format("{0} ({1})", m.Name, m.PersonalNumber));

            menu.textLines.Add("Boats:");           

            int boatCount = 1;
            foreach (Boat b in m.Boats)
            {
                menu.menuItems.Add(
                    new MenuItem(
                        boatCount.ToString(),
                        String.Format("{0,-12} {1,5} meters long", b.BoatType, b.BoatLength),
                        PrintMemberBoat,
                        b
                    )
                );

                boatCount++;
            }

            menu.menuItems.Add(new MenuItem(""));
            menu.menuItems.Add(new MenuItem("A", "Add boat", PrintAddBoat, m, 2));
            menu.menuItems.Add(new MenuItem(""));
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

            string boatlength = _menuView.GetUserInputLine("Enter new boat length (meters with 1 decimal)");
            decimal boatLengthDecimal;

            if (!decimal.TryParse(boatlength, out boatLengthDecimal))
            {
                _menuView.PrintError();
                return;
            }

            b.BoatLength = boatLengthDecimal;

            // Validate input
            if (!Validate(m))
            {
                ShowErrorMessages();
            }
            else
            {
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

                // Try to save input
                try
                {
                    _memberService.SaveBoat(m, b);
                }
                catch (Exception e)
                {
                    // Print out error
                    _menuView.PrintError(e.Message);
                }
            }
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

            String oldName = m.Name;

            m.Name = _menuView.GetUserInputLine("Enter new name");


            // Validate input
            if (!Validate(m))
            {
                m.Name = oldName;
                ShowErrorMessages();
            }
            else
            {
                // Try to save input
                try
                {
                    _memberService.SaveMember(m);
                }
                catch (Exception e)
                {
                    // Print out error
                    _menuView.PrintError(e.Message);
                }
            }            
        }

        private void PrintEditPersonalNumber(object member)
        {
            Member m = (Member)member;
            String oldPersonalNumber = m.PersonalNumber;

            m.PersonalNumber = _menuView.GetUserInputLine("Enter new personal number in the format YYMMDD-NNNN");

            // Validate input
            if (!Validate(m))
            {
                m.PersonalNumber = oldPersonalNumber;
                ShowErrorMessages();
            }
            else
            {
                // Try to save input
                try
                {
                    _memberService.SaveMember(m);
                }
                catch (Exception e)
                {
                    m.PersonalNumber = oldPersonalNumber;
                    _menuView.PrintError(e.Message);
                }
            }
        }

        private void PrintEditBoatLength(object boat)
        {
            Boat b = (Boat)boat;

            decimal oldBoatLength = b.BoatLength;

            string boatlength = _menuView.GetUserInputLine("Enter new boat length (meters with 1 decimal)");
            decimal boatLengthDecimal;

            if(!decimal.TryParse(boatlength, out boatLengthDecimal))
            {
                _menuView.PrintError();
                return;
            }

            b.BoatLength = boatLengthDecimal;

            // Validate input
            if (!Validate(b))
            {
                b.BoatLength = oldBoatLength;
                ShowErrorMessages();

            }
            else
            {
                // Try to save input
                try
                {
                    _memberService.SaveBoat(b.MemberId, b);
                }
                catch (Exception e)
                {
                    // Print out error
                    _menuView.PrintError(e.Message);
                }
            }
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

            // Try to save input
            try
            {
                _memberService.SaveBoat(b.MemberId, b);
            }
            catch (Exception e)
            {
                // Print out error
                _menuView.PrintError(e.Message);
            }
        }

        private void DeleteMemberBoat(object boat)
        {
            Boat b = (Boat)boat;

            _memberService.DeleteBoat(b.MemberId, b);
        }
    }
}