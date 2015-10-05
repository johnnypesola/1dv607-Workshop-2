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
            string _returnValue = reader.GetString(columnIndex);

            switch (_returnValue)
            {
                case "Sailboat": return BoatType.Sailboat;
                case "Motorsailer": return BoatType.Motorsailer;
                case "Canoe": return BoatType.Canoe;
                case "Other": return BoatType.Other;
            }

            return BoatType.Other;
        }

    }
}