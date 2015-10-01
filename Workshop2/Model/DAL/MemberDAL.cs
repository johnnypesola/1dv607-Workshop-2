using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workshop2.Model.DAL
{
    class MemberDAL : DALBase
    {
        public void Add(Member member)
        {
            try
            {
                using (connection = CreateConnection())
                {
                    connection.Open();

                    using (command = CreateCommand())
                    {
                        // Prepare statement
                        command.CommandText = "INSERT INTO Member(MemberName, MemberPersonalNumber) VALUES(@MemberName, @MemberPersonalNumber)";
                        command.Prepare();

                        // Add parameters
                        command.Parameters.AddWithValue("@MemberName", member.Name);
                        command.Parameters.AddWithValue("@MemberPersonalNumber", member.PersonalNumber);

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

        public void Update(Member member)
        {
            try
            {
                using (connection = CreateConnection())
                {
                    connection.Open();

                    using (command = CreateCommand())
                    {
                        // Prepare statement
                        command.CommandText = "UPDATE Member SET MemberName = @MemberName, MemberPersonalNumber = @MemberPersonalNumber WHERE MemberID = @MemberId";
                        command.Prepare();

                        // Add parameters
                        command.Parameters.AddWithValue("@MemberName", member.Name);
                        command.Parameters.AddWithValue("@MemberPersonalNumber", member.PersonalNumber);
                        command.Parameters.AddWithValue("@MemberId", member.Id);

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

        public void Delete(Member member)
        {
            try
            {
                using (connection = CreateConnection())
                {
                    connection.Open();

                    using (command = CreateCommand())
                    {
                        // Prepare statement
                        command.CommandText = "DELETE FROM Member WHERE MemberPersonalNumber = @MemberPersonalNumber OR MemberID = @MemberId";
                        command.Prepare();

                        // Add parameters
                        command.Parameters.AddWithValue("@MemberPersonalNumber", member.PersonalNumber);
                        command.Parameters.AddWithValue("@MemberId", member.Id);

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

        public Member Get(Member member)
        {
            try
            {
                using (connection = CreateConnection())
                {
                    connection.Open();

                    using (command = CreateCommand())
                    {
                        // Prepare statement
                        command.CommandText = "SELECT * FROM Member WHERE MemberPersonalNumber = @MemberPersonalNumber OR MemberId = @MemberId";
                        command.Prepare();

                        // Add parameters
                        command.Parameters.AddWithValue("@MemberPersonalNumber", member.PersonalNumber);
                        command.Parameters.AddWithValue("@MemberId", member.Id);

                        // Select from DB
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            // While there are data rows in DB
                            while (reader.Read())
                            {
                                // Create object from DB row data and return it
                                return new Member
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("MemberId")),
                                    Name = reader.GetString(reader.GetOrdinal("MemberName")),
                                    PersonalNumber = reader.GetString(reader.GetOrdinal("MemberPersonalNumber"))
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

        public List<Member> GetAll()
        {
            try
            {
                // Create list to return
                List<Member> memberReturnList = new List<Member>(50);


                using (connection = CreateConnection())
                {
                    connection.Open();

                    using (command = CreateCommand())
                    {
                        // Prepare statement
                        command.CommandText = "SELECT * FROM Member";
                        command.Prepare();

                        // Select from DB
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            // While there are data rows in DB
                            while (reader.Read())
                            {
                                // Create object from DB row data and add to return list
                                memberReturnList.Add(new Member
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("MemberId")),
                                    Name = reader.GetString(reader.GetOrdinal("MemberName")),
                                    PersonalNumber = reader.GetString(reader.GetOrdinal("MemberPersonalNumber"))
                                });
                            }
                        }

                        // Remove unused list rows, free memory.
                        memberReturnList.TrimExcess();

                        // Return list
                        return memberReturnList;
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
