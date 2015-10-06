using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workshop2.Model.BLL
{
    public class MenuItem
    {
        // Properties
        public String text { get; private set; }
        public String key { get; private set; }
        public Action action { get; private set; }
        public object argument { get; private set; }
        public Action<object> actionWithArgument { get; private set; }

        public int autoExit { get; private set; }
        // Constructor
        public MenuItem(String menuText)
        {
            text = menuText;
        }
        public MenuItem(String triggerKey, String menuText)
        {
            text = menuText;
            key = triggerKey;
        }

        public MenuItem(String triggerKey, String menuText, Action methodName)
        {
            text = menuText;
            key = triggerKey;
            action =  methodName;
        }

        public MenuItem(String triggerKey, String menuText, Action<object> methodName, object argumentObj, int autoExitMenuCount = 0)
        {
            text = menuText;
            key = triggerKey;
            actionWithArgument = methodName;
            argument = argumentObj;
            autoExit = autoExitMenuCount;
        }
    }
}
