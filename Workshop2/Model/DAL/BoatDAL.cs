using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workshop2.Model.BLL;

namespace Workshop2.Model.DAL
{
    class BoatDAL : DALBase
    {
        public void RegisterNewBoat(Boat boat, Member member)
        {
            try
            {
                using(connection = CreateConnection())
                {
                    connection.Open();

                    using (command = CreateCommand())
                    {
                        // Prepare statement
                        command.CommandText = "INSERT INTO Boat(MemberId, Length, BoatType) VALUES(@MemberId, @BoatLength, @BoatType)";
                        command.Prepare();

                        // Add parameters
                        command.Parameters.AddWithValue("@MemberId", member.Id);
                        command.Parameters.AddWithValue("@BoatLength", boat.Length);
                        command.Parameters.AddWithValue("@BoatType", boat.BoatType);

                        // Add to DB
                        command.ExecuteNonQuery();
                    }

                }
            }
            catch(Exception)
            {
                throw new Exception(DAL_ERROR_MSG);
            }
        }
        public void DeleteBoat(Boat boat)
        {
            //Add functionality
        }
        public Boat GetBoat(Boat boat)
        {
            //Add functionality
            return boat;
        }
        public void EditBoat(Boat boat)
        {
            //Add functionality
        }
    }
}
