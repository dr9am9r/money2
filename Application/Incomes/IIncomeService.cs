using System;
using System.Collections.Generic;
using Money2.Domain.Incomes;

namespace Money2.Application.Incomes
{
    public interface IIncomeService
    {
        List<IncomeDto> GetIncomes( Int32 userId, DateTime? startDate, DateTime? endDate, Int32 typeId );

        List<IncomeType> GetIncomeTypes();

        void DeleteIncome( Int32 userId, Int32 incomeId );

        void SaveIncome( IncomeDto dto );
    }
}
