using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workshop2.Model.BLL
{
    public class MenuContainer
    {
        public String header { get; private set; }

        public List<String> textLines;

        public List<MenuItem> menuItems;

        public String footer { get; set; }

        public MenuContainer(String headerText, String footerText = null)
        {
            textLines = new List<String>();
            menuItems = new List<MenuItem>();

            header = headerText;
            footer = footerText;
        }
    }
}
