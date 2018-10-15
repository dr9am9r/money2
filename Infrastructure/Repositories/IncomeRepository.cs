using System;
using System.Collections.Generic;
using System.Linq;
using Money2.Domain.Incomes;

namespace Money2.Infrastructure
{
    public class IncomeRepository : Repository<Income>, IIncomeRepository
    {
        public IncomeRepository( Money2Context context ) : base( context ) { }

        public List<Income> GetIncomes( Int32 userId, DateTime? startDate, DateTime? endDate, Int32 typeId )
        {
            var query = Query
                .Where( i => i.UserId == userId );

            if ( typeId != 0 ) query = query.Where( i => i.TypeId == typeId );
            if ( startDate != null ) query = query.Where( i => i.Date >= startDate.Value );
            if ( endDate != null ) query = query.Where( i => i.Date <= endDate.Value );

            return query
                .OrderBy( i => i.Date )
                .ToList();
        }

        public List<IncomeReport> GetIncomeReport(Int32 userId)
        {
            return null;

            //var result = new List<IncomeReport>();

            //var cmd = Connection.CreateCommand();
            //cmd.CommandText = @"SELECT Name, YEAR(Date) as Year, MONTH(Date) as Month, SUM(Sum) as Sum
            //                    FROM Incomes
            //                    WHERE UserId = @userId
            //                    GROUP BY Name, YEAR(Date), MONTH(Date)";
            //cmd.Parameters.Add("@userId", MySqlDbType.Int32).Value = userId;

            //using (var reader = cmd.ExecuteReader())
            //{
            //    while (reader.Read())
            //    {
            //        var report = new IncomeReport()
            //        {
            //            Date = new DateTime(Convert.ToInt32(reader["Year"]), Convert.ToInt32(reader["Month"]), 1),
            //            Name = Convert.ToString(reader["Name"]),
            //            Sum = Convert.ToDecimal(reader["Sum"])
            //        };

            //        result.Add(report);
            //    }
            //}

            //return result;
        }

        public List<IncomeType> GetIncomeTypes()
        {
            return CommonQuery<IncomeType>()
                .OrderBy( i => i.Name )
                .ToList();
        }

        public Dictionary<IncomeType, Decimal> GetIncomeTypeStats( Int32 userId )
        {
            return null;

            //var result = new Dictionary<IncomeType, Decimal>();

            //using ( var cmd = Connection.CreateCommand() )
            //{
            //    cmd.CommandText = @"SELECT it.Id, it.Name, Sum(i.Sum) As Sum FROM Incomes i
            //                    JOIN IncomeTypes it ON i.TypeId = it.Id
            //                    WHERE i.UserId = @userId
            //                    GROUP BY it.Id, it.Name";
            //    cmd.Parameters.Add( "@userId", MySqlDbType.Int32 ).Value = userId;

            //    using ( var reader = cmd.ExecuteReader() )
            //    {
            //        while ( reader.Read() )
            //        {
            //            var type = FetchType( reader );

            //            result.Add( type, Convert.ToDecimal( reader[ "Sum" ] ) );
            //        }
            //    }
            //}

            //return result;
        }
    }
}