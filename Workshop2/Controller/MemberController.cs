using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workshop2.Model;

namespace Workshop2.Controller
{
    class MemberController
    {

        // Constructor
        public MemberController()
        {
            // Create a test member
            Member member = new Member
            {
                Name = "Kalle Anka",
                PersonalNumber = "321010-1234"
            };

            // Write out member name for fun
            Console.WriteLine(member.Name);

            // Wait for user input before app closes
            Console.ReadKey();
        }
    }
}
