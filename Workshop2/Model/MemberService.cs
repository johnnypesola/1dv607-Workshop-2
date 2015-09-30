using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workshop2.Model.DAL;

namespace Workshop2.Model
{
    class MemberService
    {
        // Fields
        private MemberDAL _memberDAL;

        // Properties
        private MemberDAL MemberDAL
        {
            get
            {
                return _memberDAL ?? (_memberDAL = new MemberDAL());
            }
        }

        // Methods
        public void Add(Member member)
        {
            // Check if there is a member with this PersonalNumber already
            if(MemberDAL.Get(member) != null)
            {
                throw new Exception(String.Format("There is already a member with this personal number: {0}", member.PersonalNumber));
            }

            // Add member to database
            MemberDAL.Add(member);
        }

        public void Delete(Member member)
        {
            // Check if there is a member with this PersonalNumber
            if (MemberDAL.Get(member) != null)
            {
                // Delete member from DB
                MemberDAL.Delete(member);

                //TODO Delete members boats from DB   
            }
            else
            {
                throw new Exception(String.Format("No member with this personal number could be found: {0}", member.PersonalNumber));
            }
        }

        public Member Get(Member member)
        {
            // Get member from DB
            Member returnMember = MemberDAL.Get(member);

            // If there wasn't a member in DB
            if (returnMember == null)
            {
                throw new Exception(String.Format("No member with this personal number could be found: {0}", member.PersonalNumber));
            }

            return returnMember;
        }

        public IEnumerable<Member> GetAll()
        {
            // Get members from DB
            IEnumerable<Member> memberReturnList = MemberDAL.GetAll();

            // If there wasn't any members in DB
            if (memberReturnList.Count() == 0)
            {
                throw new Exception("No members found.");
            }

            return memberReturnList;
        }

        public void TestAll() // Test all methods
        {
            // Create a test member
            Member member = new Member
            {
                Name = "Test Member",
                PersonalNumber = "321010-1234"
            };

            // Add test member to database
            Add(member);

            // Get test member from database
            Get(member);

            // Delete member from database
            Delete(member);

            // Get all members from database
            GetAll();
        }
    }
}
