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
        public void WriteOutMember(Member member) {

            // Write out member name for fun
            Console.WriteLine(member.Name);

            // Wait for user input before app closes
            Console.ReadKey();
        }
    }
}
