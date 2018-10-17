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
                .OrderByDescending( i => i.Date )
                .ToList();
        }

        public List<IncomeReport> GetIncomeReport(Int32 userId)
        {
            return Query
                .Where( i => i.UserId == userId )
                .GroupBy( i => new { i.Name, i.Date.Year, i.Date.Month } )
                .Select( i => new IncomeReport()
                    {
                        Date = new DateTime( i.Key.Year, i.Key.Month, 1 ),
                        Name = i.Key.Name,
                        Sum = i.Sum( s => s.Sum )
                    } )
                .ToList();
        }

        public List<IncomeType> GetIncomeTypes()
        {
            return CommonQuery<IncomeType>()
                .OrderBy( i => i.Name )
                .ToList();
        }

        public Dictionary<IncomeType, Decimal> GetIncomeTypeStats( Int32 userId )
        {
            return Query
                .Where( i => i.UserId == userId )
                .GroupBy( i => i.TypeId )
                .Join( CommonSet<IncomeType>(), i => i.Key, it => it.Id, (i, it) => new { IncomeType = it, Incomes = i } )
                .ToDictionary( i => i.IncomeType, i => i.Incomes.Sum( s => s.Sum ) );
        }
    }
}