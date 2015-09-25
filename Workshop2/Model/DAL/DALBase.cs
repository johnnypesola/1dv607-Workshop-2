using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Workshop2.Model.DAL
{
    public abstract class DALBase
    {
        // Fields
        protected const string DAL_ERROR_MSG = "An error occured in DAL.";

        // Properties
        static protected SQLiteConnection connection { get; set; }
        static protected SQLiteCommand command { get; set; }

        // Constructor
        static DALBase() {
            
        }

        // Methods
        static protected SQLiteConnection CreateConnection()
        {
            try
            {
                return new SQLiteConnection(ConfigurationManager.ConnectionStrings[0].ConnectionString);
            }
            catch (Exception) 
            {
                throw new Exception(DAL_ERROR_MSG);
            }
        }

        static protected SQLiteCommand CreateCommand()
        {
            try
            {
                return new SQLiteCommand(connection);
            }
            catch (Exception) 
            {
                throw new Exception(DAL_ERROR_MSG);
            }
        }

        static public void SetupTables()
        {
            try
            {
                using (connection = CreateConnection())
                {
                    connection.Open();

                    using (command = CreateCommand())
                    {
                        /* Member */
                        // Drop existing table if it exists
                        command.CommandText = "DROP TABLE IF EXISTS Member";
                        command.ExecuteNonQuery();

                        // Create new member table
                        command.CommandText = "CREATE TABLE Member(MemberId INTEGER PRIMARY KEY, MemberName VARCHAR(30), MemberPersonalNumber VARCHAR(11))";
                        command.ExecuteNonQuery();

                        // Add some default members
                        command.CommandText = "INSERT INTO Member (MemberName, MemberPersonalNumber) VALUES ('Tyrion Lannister', '771012-6999')";
                        command.ExecuteNonQuery();

                        command.CommandText = "INSERT INTO Member (MemberName, MemberPersonalNumber) VALUES ('Heisenberg', '661010-6909')";
                        command.ExecuteNonQuery();

                        command.CommandText = "INSERT INTO Member (MemberName, MemberPersonalNumber) VALUES ('Jon Snow', '810610-1256')";
                        command.ExecuteNonQuery();

                        /* Boat */
                        // Drop existing table if it exists
                        command.CommandText = "DROP TABLE IF EXISTS Boat";
                        command.ExecuteNonQuery();

                        // Create new boat table
                        command.CommandText = "CREATE TABLE Boat(BoatId INTEGER PRIMARY KEY, MemberId INT, BoatTypeId INT, BoatLength REAL)";
                        command.ExecuteNonQuery();

                        // Add some default boats
                        command.CommandText = "INSERT INTO Boat (MemberId, BoatTypeId, BoatLength) VALUES (1, 1, 6.3)";
                        command.ExecuteNonQuery();

                        /* BoatType */
                        // Drop existing table if it exists
                        command.CommandText = "DROP TABLE IF EXISTS BoatType";
                        command.ExecuteNonQuery();

                        // Create new boat table
                        command.CommandText = "CREATE TABLE BoatType(BoatTypeId INTEGER PRIMARY KEY, BoatTypeName VARCHAR(30))";
                        command.ExecuteNonQuery();

                        // Add some default boat types
                        command.CommandText = "INSERT INTO BoatType (BoatTypeName) VALUES ('Sailboat')";
                        command.ExecuteNonQuery();

                        command.CommandText = "INSERT INTO BoatType (BoatTypeName) VALUES ('Motorsailer')";
                        command.ExecuteNonQuery();

                        command.CommandText = "INSERT INTO BoatType (BoatTypeName) VALUES ('Kayak/Canoe')";
                        command.ExecuteNonQuery();

                        command.CommandText = "INSERT INTO BoatType (BoatTypeName) VALUES ('Other')";
                        command.ExecuteNonQuery();
                    }

                    connection.Close();
                }
            }
            catch (Exception)
            {
                throw new Exception(DAL_ERROR_MSG);
            }

        }
    }
}
