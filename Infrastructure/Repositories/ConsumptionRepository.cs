using System;
using System.Collections.Generic;
using System.Linq;
using Money2.Domain.Consumptions;

namespace Money2.Infrastructure
{
    public class ConsumptionRepository : Repository<Consumption>, IConsumptionRepository
    {
        public ConsumptionRepository( Money2Context context ) : base( context ) { }

        public Consumption GetFirstConsumption( Int32 userId )
        {
            return Query.FirstOrDefault( c => c.UserId == userId );
        }

        public List<ConsumptionItem> GetConsumptionItems( Int32 consumptionId )
        {
            return CommonQuery<ConsumptionItem>()
                .Where( c => c.ConsumptionId == consumptionId )
                .ToList();
        }

        public List<String> GetConsumptionNames( Int32 userId )
        {
            return Query
                .Where( c => c.UserId == userId )
                .SelectMany( c => c.ConsumptionItems.Select( ci => ci.Name ) )
                .Distinct()
                .OrderBy( c => c )
                .ToList();
        }

        public List<String> GetConsumptionPlaces( Int32 userId )
        {
            return Query
                .Where( c => c.UserId == userId )
                .Select( c => c.Place )
                .Distinct()
                .OrderBy( c => c )
                .ToList();
        }

        public List<Consumption> GetConsumptions( Int32 userId, DateTime? startDate, DateTime? endDate )
        {
            var query = Query
                .Where( c => c.UserId == userId );
            
            if( startDate != null )
            {
                query = query.Where( c => c.Date >= startDate.Value );
            }

            if ( endDate != null )
            {
                query = query.Where( c => c.Date <= endDate.Value );
            }

            return query
                .OrderBy( c => c.Date )
                .ToList();
        }

        public List<ConsumptionReport> GetConsumptionReport( Int32 userId )
        {
            return null;

            //var result = new List<ConsumptionReport>();

            //var cmd = Connection.CreateCommand();
            //cmd.CommandText = @"SELECT ci.name as Name, YEAR(c.Date) as Year, MONTH(c.date) as Month, SUM(ci.Quantity * ci.Price) as Sum
            //                    FROM Consumptions c 
            //                    JOIN ConsumptionItems ci ON ci.ConsumptionId = c.Id 
            //                    WHERE c.UserId = @userId
            //                    GROUP BY ci.Name, YEAR(c.Date), MONTH(c.date)";
            //cmd.Parameters.Add( "@userId", MySqlDbType.Int32 ).Value = userId;

            //using ( var reader = cmd.ExecuteReader() )
            //{
            //    while ( reader.Read() )
            //    {
            //        var report = new ConsumptionReport()
            //        {
            //            Date = new DateTime( Convert.ToInt32( reader[ "Year" ] ), Convert.ToInt32( reader[ "Month" ] ), 1 ),
            //            Name = Convert.ToString( reader[ "Name" ] ),
            //            Sum = Convert.ToDecimal( reader[ "Sum" ] )
            //        };

            //        result.Add( report );
            //    }
            //}

            //return result;
        }
    }
}
