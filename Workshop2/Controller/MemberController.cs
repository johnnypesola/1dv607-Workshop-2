using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workshop2.Model;
using Workshop2.View;

namespace Workshop2.Controller
{
    class MemberController
    {
        MemberView memberView;

        // Constructor
        public MemberController()
        {
            // Create view
            memberView = new MemberView();

            // Create a test member
            Member member = new Member
            {
                Name = "Kalle Anka",
                PersonalNumber = "321010-1234"
            };

            // Write out member
            memberView.WriteOutMember(member);
        }
    }
}
