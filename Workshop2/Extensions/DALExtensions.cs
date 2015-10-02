using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Workshop2
{
    public static class DALExtensions
    {
        public enum BoatType { Sailboat, Motorsailer, Canoe, Other };

        // Custom enum reader method
        public static BoatType GetBoatTypeEnum(this SQLiteDataReader reader, int columnIndex)
        {
            // Get string to parse from SQLiteDataReader
            string _stringToParse = reader.GetString(columnIndex);

            // Parse string to BoatType enum
            return (BoatType)Enum.Parse(typeof(BoatType), _stringToParse);
        }
    }
}
