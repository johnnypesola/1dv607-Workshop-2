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
        private List<Member> _memberList;

    // Properties
        private MemberDAL MemberDAL
        {
            // Auto create object if needed
            get
            {
                return _memberDAL ?? (_memberDAL = new MemberDAL());
            }
        }

        public List<Member> MemberList
        {
            // Auto create object if needed
            get
            {
                return _memberList ?? (_memberList = GetAllFromDAL());
            }
        }

    // Private Methods
        private Member FindMemberInLocalList(Member member)
        {
            // Match by id number or personal number
            return MemberList.Find(x => (x.Id == member.Id) || (x.PersonalNumber == member.PersonalNumber));
        }

        private void Add(Member member)
        {
            // Check if there is a member with this PersonalNumber already
            if(Get(member) != null)
            {
                throw new Exception(String.Format("There is already a member with this personal number: {0}", member.PersonalNumber));
            }

            // Add member to database
            MemberDAL.Add(member);

            // Add member to local Memberlist
            MemberList.Add(member);
        }

        private List<Member> GetAllFromDAL()
        {
            // Get members from DB
            List<Member> memberReturnList = MemberDAL.GetAll();

            // If there wasn't any members in DB
            if (memberReturnList.Count() == 0)
            {
                throw new Exception("No members found.");
            }

            // Get boats for members
            foreach (Member member in memberReturnList) {
                member.Boats = GetBoatsForMember(member);
            }

            return memberReturnList;
        }

        private void Update(Member member)
        {
            Member _memberFromList;

            // Get member from list searching by properties: Id, PersonalNumber
            _memberFromList = Get(member);

            // Check if there is no member to update
            if (_memberFromList == null) 
            {
                throw new Exception(String.Format("No member with personal number '{0}' or id '{1}' could be found: {0}", member.PersonalNumber, member.Id));
            }
            // Check if the desired personal number is already taken. MemberDAL.Get() matches members by PersonalNumber and Id.
            else if (_memberFromList.Id != member.Id)
            {
                throw new Exception(String.Format("There is already a member with this personal number: {0}", member.PersonalNumber));
            }

            // Update member in database
            MemberDAL.Update(member);

            // Update local MemberList
            MemberList[MemberList.IndexOf(FindMemberInLocalList(member))] = member;
        }

        private List<Boat> GetBoatsForMember(Member member)
        {
            return new List<Boat>();
        }

    // Public Methods
        public void Save(Member member)
        {
            // If a new member should be added
            if (member.Id == 0)
            {
                Add(member);
            }
            // Member should be updated
            else
            {
                Update(member);
            }
        }

        public void Delete(Member member)
        {
            // Check if there is a member with this PersonalNumber
            if (MemberDAL.Get(member) != null)
            {
                // Delete member from DB
                MemberDAL.Delete(member);

                // Delete from local MemberList
                MemberList.Remove(FindMemberInLocalList(member));

                //TODO Delete members boats in DB
            }
            else
            {
                throw new Exception(String.Format("No member with personal number '{0}' or id '{1}' could be found: {0}", member.PersonalNumber, member.Id));
            }
        }

        public void Delete(int memberId)
        {
            Delete(new Member { Id = memberId });
        }

        public Member Get(Member member)
        {
            return FindMemberInLocalList(member);
        }

        public Member Get(int memberId)
        {
            return Get(new Member { Id = memberId });
        }

        public List<Member> GetAll()
        {
            return MemberList;
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
