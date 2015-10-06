using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workshop2.Model.BLL;

namespace Workshop2.View
{
    public class MenuView
    {
        private static int nestedMenuCount = 0;
        private static int autoExitCount = 0;

        public static string key;

    // Private methods

        private void PrintFooter(String footerString)
        {
            if (footerString != null)
            {
                Console.WriteLine();
                Console.WriteLine(footerString);
            }
        }

        private void PrintListItems(List<String> list)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            foreach (String listString in list)
            {
                Console.Write(String.Format("  {0}", listString));
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
        }

        private void PrintMenuItems(List<MenuItem> menuItems)
        {
            foreach (MenuItem menu in menuItems)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(String.Format("{0,-2}", menu.key));
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(String.Format("{0}", menu.text));
                Console.WriteLine();
            }
        }

        private void PrintBackOrExitKeyOptions()
        {
            Console.WriteLine();
            Console.Write("Press ");
            Console.ForegroundColor = ConsoleColor.Red;
            if (nestedMenuCount > 1)
            {
                Console.Write("B");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" to go back");
            }
            else
            {
                Console.Write("X");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" to exit");
            }
            Console.WriteLine();
        }

        private void PrintModelObjects(List<Object> objects)
        {
            foreach(object obj in objects)
            {

            }
        }

        private void ExecuteMenuAction(MenuItem menu)
        {
            // Action without argument
            if (menu.action != null)
            {
                menu.action();
            }
            // Action with argument
            else if (menu.actionWithArgument != null && menu.argument != null)
            {
                menu.actionWithArgument(menu.argument);
            }
        }

    // Public methods

        public void PrintHeader(String headerString, int Width = 40)
        {

            string frameLine = new String('═', Width);

            Console.ForegroundColor = ConsoleColor.White;

            // Write out centered header text
            Console.WriteLine(" ╔═{0}═╗ ", frameLine);
            Console.WriteLine(" ║ {0} ║ ", headerString.CenterAlignString(new String(' ', Width)));
            Console.WriteLine(" ╚═{0}═╝ ", frameLine);
            Console.WriteLine();
        }

        public void PrintMenu(MenuContainer menuContainer)
        {
            nestedMenuCount++;

            do
            {
                // Clear console before output of header
                Console.Clear();

                // Print header
                PrintHeader(menuContainer.header);

                // Print out each menu item option
                PrintListItems(menuContainer.textLines);

                // Print out each menu item option
                PrintMenuItems(menuContainer.menuItems);

                // Print optional footer
                PrintFooter(menuContainer.footer);

                // Display B for Back, or X for Exit.
                PrintBackOrExitKeyOptions();

                // Get user input, pauses the loop
                string userInput = GetKeyChoice();

                // Get out of menu if user pressed "B" for back or "X" for exit
                if (nestedMenuCount > 1 && userInput == "B" || nestedMenuCount == 1 && userInput == "X")
                {
                    nestedMenuCount--;
                    break;
                }

                // Find the menu that the user chose
                MenuItem chosenMenu = menuContainer.menuItems.Find(x => (x.key == userInput));

                // Execute chosen menu action, if there is one.
                if (chosenMenu != null)
                {
                    ExecuteMenuAction(chosenMenu);

                    // Check if we should auto exit this menu after action
                    if (chosenMenu.autoExit > 0)
                    {

                        autoExitCount += chosenMenu.autoExit;
                    }
                }

                // Auto exit
                if (autoExitCount > 0)
                {
                    autoExitCount--;
                    nestedMenuCount--;
                    break;
                }

            } while (true);
        }

        public static String GetKeyChoice()
        {
            return Console.ReadKey().KeyChar.ToString().ToUpper();
        }

        public string GetUserInputLine(String information)
        {
            Console.Clear();
            Console.WriteLine(information);

            return Console.ReadLine();
        }

        public string GetUserOption(MenuContainer menuContainer)
        {
            do
            {
                // Clear console before output of header
                Console.Clear();

                // Print header
                PrintHeader(menuContainer.header);

                // Print out each menu item option
                PrintMenuItems(menuContainer.menuItems);

                // Display B for Back, or X for Exit.
                PrintBackOrExitKeyOptions();

                // Get user input, pauses the loop
                string userInput = GetKeyChoice();

                // Get out of menu if user pressed "B" for back or "X" for exit
                if (nestedMenuCount > 1 && userInput == "B" || nestedMenuCount == 1 && userInput == "X")
                {
                    break;
                }

                // Find the menu that the user chose
                MenuItem chosenMenu = menuContainer.menuItems.Find(x => (x.key == userInput));

                // Execute chosen menu action, if there is one.
                if (chosenMenu != null)
                {
                    // Check if we should auto exit this menu after action
                    if (chosenMenu.autoExit > 0)
                    {

                        autoExitCount += chosenMenu.autoExit;
                    }

                    return userInput;
                }

            } while (true);

            return null;
        }    

        public void DisplayParentMenu()
        {
            MenuContainer parentMenu = new MenuContainer("Parent menu");

            parentMenu.textLines.Add("Kellogs Cornflakes");
            parentMenu.textLines.Add("Kellogs Frosties");

            parentMenu.menuItems.Add(new MenuItem("V", "Show verbose list", DisplayChildMenu));
            parentMenu.menuItems.Add(new MenuItem(null, "Show nothing at all", DisplayChildMenu));
            parentMenu.menuItems.Add(new MenuItem("1", "Some question to answer", GetSomeStuff));
            
            

            parentMenu.footer = "Choose a member.";

            // Display this menu
            PrintMenu(parentMenu);
        }

        public void GetSomeStuff()
        {
            String myValue = GetUserInputLine("When did it happen?");
            String myOtherValue = GetUserInputLine("Why did it happen?");
        }

        public void DisplayChildMenu()
        {
            MenuContainer childMenu = new MenuContainer("Child menu");

            childMenu.menuItems.Add(new MenuItem("D", "Do stuff", DisplayChildMenu));

            // Display this menu
            PrintMenu(childMenu);
        }

    }


}
