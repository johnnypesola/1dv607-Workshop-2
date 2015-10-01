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
        // Fields
        private MemberService _memberService;
        private MemberView _memberView;

        // Properties
        private MemberService MemberService
        {
            // Auto create object if needed
            get
            {
                return _memberService ?? (_memberService = new MemberService());
            }
        }

        private MemberView MemberView
        {
            // Auto create object if needed
            get
            {
                return _memberView ?? (_memberView = new MemberView());
            }
        }


        // Constructor
        public MemberController()
        {

            Member testMember = new Member
            {
                Id = 4,
                Name = "Kajsa Ank",
                PersonalNumber = "521010-3121"
            };

            //MemberService.Save(testMember);

            

            MemberView.WriteOutMembersFromDB();
        }

        // Methods
    }
}
