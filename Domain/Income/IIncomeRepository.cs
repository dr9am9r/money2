using System;
using System.Collections.Generic;

namespace Money2.Domain.Incomes
{
    /// <summary>
    /// Репозиторий Доходов
    /// </summary>
    public interface IIncomeRepository
    {
        void Delete( Income income );

        Income Get( Int32 id );

        List<IncomeReport> GetIncomeReport( Int32 userId );

        List<Income> GetIncomes( Int32 userId, DateTime? startDate, DateTime? endDate, Int32 typeId );

        List<IncomeType> GetIncomeTypes();

        Dictionary<IncomeType, Decimal> GetIncomeTypeStats( Int32 userId );

        void Save( Income income );
    }
}
