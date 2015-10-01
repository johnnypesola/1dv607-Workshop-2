using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workshop2.Extensions
{
    class DALExtensions
    {
        enum BoatType { Sailboat, Motorsailer, Kayak, Other };

        // Custom Safe SqlDataReader methods, with null checks
        public static BoatType GetBoatTypeEnum(this SQLiteDataReader reader, int columnIndex)
        {
            string _returnValue = reader.GetString(columnIndex);
            
            switch (_returnValue)
            {
                case "Sailboat": return BoatType.Sailboat;
                case "Motorsailer": return BoatType.Motorsailer;
                case "Kayak": return BoatType.Kayak;
                case "Other": return BoatType.Other;
            }

            return BoatType.Other;
        }

    }
}
