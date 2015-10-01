using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workshop2.Model;

namespace Workshop2.View
{
    class MemberView
    {
        // Fields
        private MemberService _memberService;

        // Properties
        private MemberService MemberService
        {
            // Auto create object if needed
            get
            {
                return _memberService ?? (_memberService = new MemberService());
            }
        }

        public void WriteOutMembersFromDB()
        {
            // Get all members
            IEnumerable<Member> members = MemberService.GetAll();


            //Write out members in console
            foreach (Member member in members) 
            {
                Console.WriteLine("{0} {1} {2}", member.Id, member.Name, member.PersonalNumber);
            }

            Member otherMember = MemberService.Get(1);

            Console.WriteLine("{0} {1} {2}", otherMember.Id, otherMember.Name, otherMember.PersonalNumber);

            // Wait for user input before app closes
            Console.ReadKey();
        }
    }
}
