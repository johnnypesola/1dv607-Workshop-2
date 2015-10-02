using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workshop2.Model.BLL;
using Workshop2;

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
            try
            {
                using (connection = CreateConnection())
                {
                    connection.Open();

                    using (command = CreateCommand())
                    {
                        // Prepare statement
                        command.CommandText = "DELETE FROM Boat WHERE BoatId = @BoatId";
                        command.Prepare();

                        // Add parameters
                        command.Parameters.AddWithValue("@MemberId", boat.BoatId);

                        // Remove from DB
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception(DAL_ERROR_MSG);
            }
        }
        public List<Boat> GetBoats(Boat boat)
        {
            try
            {
                List<Boat> _returnBoatList = new List<Boat>(10);

                using (connection = CreateConnection())
                {
                    connection.Open();

                    using (command = CreateCommand())
                    {
                        // Prepare statement
                        command.CommandText = "SELECT * FROM Boat WHERE MemberId = @MemberId";
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
                                _returnBoatList.Add(new Boat
                                {
                                    MemberId = reader.GetInt32(reader.GetOrdinal("MemberId")),
                                    Length = reader.GetDecimal(reader.GetOrdinal("BoatLength")),
                                    BoatType = reader.GetString(reader.GetOrdinal("BoatType"))  //TODO: fix extension method to work with DB data
                                });
                            }
                        }
                    }
                }

                _returnBoatList.TrimExcess();

                return _returnBoatList;
            }
            catch (Exception)
            {
                throw new Exception(DAL_ERROR_MSG);
            }
        }
        public void EditBoat(Boat boat)
        {
            try
            {
                using (connection = CreateConnection())
                {
                    connection.Open();

                    using (command = CreateCommand())
                    {
                        // Prepare statement
                        command.CommandText = "UPDATE Boat SET Length = @BoatLength, BoatType = @BoatType WHERE BoatId = @BoatId";
                        command.Prepare();

                        // Add parameters
                        command.Parameters.AddWithValue("@BoatLength", boat.Length);
                        command.Parameters.AddWithValue("@BoatType", boat.BoatType); //TODO: fix extension method to work with DB data
                        command.Parameters.AddWithValue("@BoatId", boat.BoatId);

                        // Add to DB
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception(DAL_ERROR_MSG);
            }
        }
    }
}
