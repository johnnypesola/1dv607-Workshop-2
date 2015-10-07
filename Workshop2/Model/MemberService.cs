using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workshop2.Model.BLL;
using Workshop2.Model.DAL;

namespace Workshop2.Model
{
    class MemberService
    {
    // Fields
        private MemberDAL _memberDAL;
        private BoatDAL _boatDAL;
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

        private BoatDAL BoatDAL
        {
            // Auto create object if needed
            get
            {
                return _boatDAL ?? (_boatDAL = new BoatDAL());
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
            foreach (Member member in memberReturnList)
            {
                member.Boats = GetBoatsForMember(member);
            }

            return memberReturnList;
        }

        private List<Boat> GetBoatsForMember(Member member)
        {
            return BoatDAL.GetBoats(new Boat { MemberId = member.Id });
        }

        private void AddMember(Member member)
        {
            // Check if there is a member with this PersonalNumber already
            if(GetMember(member) != null)
            {
                throw new Exception(String.Format("There is already a member with this personal number: {0}", member.PersonalNumber));
            }

            // Add member to database
            MemberDAL.Add(member);

            // Add member to local Memberlist
            MemberList.Add(member);
        }

        private void UpdateMember(Member member)
        {
            Member _memberFromList;

            // Get member from list searching by properties: Id, PersonalNumber
            _memberFromList = GetMember(member);

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
            MemberList[MemberList.IndexOf(GetMember(member))] = member;
        }

        private void AddBoat(Member member, Boat boat)
        {
            // Add boat in BoatDAL
            BoatDAL.RegisterNewBoat(boat, member);

            // Add member id to boat, in case this is not done.
            boat.MemberId = member.Id;

            // Add boat in local MemberList
            member.Boats.Add(boat);
        }

        private void UpdateBoat(Member member, Boat boat)
        {
            // Update boat in BoatDAL
            BoatDAL.EditBoat(boat);

            // Update boat in local MembersList
            int _index = member.Boats.IndexOf(GetBoat(member, boat));
            member.Boats[_index] = boat;
        }

    // Public Methods

        // Member
        public void SaveMember(Member member)
        {
            // If a new member should be added
            if (member.Id == 0)
            {
                AddMember(member);
            }
            else
            {
                UpdateMember(member);
            }
        }

        public void DeleteMember(int memberId)
        {
            DeleteMember(new Member { Id = memberId });
        }

        public void DeleteMember(Member member)
        {
            // Check if there is a member with this PersonalNumber
            if (MemberDAL.Get(member) != null)
            {
                // Delete boats in DAL
                foreach (Boat boat in member.Boats)
                {
                    BoatDAL.DeleteBoat(boat);
                }

                // Delete member in DAL
                MemberDAL.Delete(member);

                // Delete from local MemberList
                MemberList.Remove(GetMember(member));
            }
            else
            {
                throw new Exception(String.Format("No member with personal number '{0}' or id '{1}' could be found: {0}", member.PersonalNumber, member.Id));
            }
        }

        public Member GetMember(int memberId)
        {
            return GetMember(new Member { Id = memberId });
        }

        public Member GetMember(Member member)
        {
            // Match by id number or personal number
            return MemberList.Find(x => (x.Id == member.Id) || (x.PersonalNumber == member.PersonalNumber));
        }

        public List<Member> GetAllMembers()
        {
            return MemberList;
        }

        // Boat
        public void SaveBoat(int memberId, Boat boat)
        {
            // If boat does not exist in member
            SaveBoat(new Member { Id = memberId }, boat);
        }

        public void SaveBoat(Member member, Boat boat)
        {
            // If boat does not exist in member
            if (GetBoat(member, boat) == null)
            {
                AddBoat(member, boat);
            }
            else
            {
                UpdateBoat(member, boat);
            }
        }

        public void DeleteBoat(int memberId, Boat boat)
        {
            DeleteBoat(GetMember(memberId), boat);
        }

        public void DeleteBoat(Member member, Boat boat)
        {
            // Delete Boat in BoatDAL
            BoatDAL.DeleteBoat(boat);

            // Delete in local MemberList
            member.Boats.Remove(boat);
        }

        public Boat GetBoat(int memberId, int boatId)
        {
            return GetBoat(new Member { Id = memberId }, new Boat { BoatId = boatId });
        }

        public Boat GetBoat(Member member, int boatId)
        {
            return GetBoat(member, new Boat { BoatId = boatId });
        }

        public Boat GetBoat(Member member, Boat boat)
        {
            // Return null if the boat has no valid id. It's a new boat perhaps?
            if (boat.BoatId == 0)
            {
                return null;
            }

            return member.Boats.Find(x => (x.BoatId == boat.BoatId));
        }
        public bool IsBoatLenghtValid(string boatLenght)
        {
            return true;
        }
        public bool IsMemberNameValid(string memberName)
        {
            return true;
        }
        public bool IsMemberPersonNumberValid(string personNumber)
        {
            return true;
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
            AddMember(member);

            // Get test member from database
            GetMember(member);

            // Delete member from database
            DeleteMember(member);

            // Get all members from database
            GetAllMembers();
        }
    }
}
