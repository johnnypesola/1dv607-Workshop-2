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
            try
            {
                using (connection = CreateConnection())
                {
                    connection.Open();

                    using (command = CreateCommand())
                    {
                        // Prepare statement
                        command.CommandText = "SELECT * FROM Boat WHERE Id = @MemberId";
                        command.Prepare();

                        // Add parameters
                        command.Parameters.AddWithValue("@MemberId", boat.BoatId);

                        // Select from DB
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            // While there are data rows in DB
                            while (reader.Read())
                            {
                                // Create object from DB row data and return it
                                return new Boat
                                {
                                    MemberId = reader.GetInt32(reader.GetOrdinal("MemberId")),
                                    Length = reader.GetDecimal(reader.GetOrdinal("BoatLength")),
                                    BoatType = reader.GetEnumerator(reader.GetOrdinal("BoatType"))  //TODO: make sure correct data is retrieved from DB
                                };
                            }
                        }
                    }
                }

                return null;
            }
            catch (Exception)
            {
                throw new Exception(DAL_ERROR_MSG);
            }
        }
        public void EditBoat(Boat boat)
        {
            //Add functionality
        }
    }
}
