using System;
using System.Collections.Generic;
using Money2.Domain.Incomes;

namespace Money2.Application.Incomes
{
    public class IncomeService : IIncomeService
    {
        private readonly IIncomeRepository _incomeRepository;

        public IncomeService( IIncomeRepository incomeRepository )
        {
            _incomeRepository = incomeRepository;
        }

        public List<IncomeDto> GetIncomes( Int32 userId, DateTime? startDate, DateTime? endDate, Int32 typeId )
        {
            List<Income> incomes = _incomeRepository.GetIncomes( userId, startDate, endDate, typeId );
            List<IncomeType> incomeTypes = _incomeRepository.GetIncomeTypes();

            return incomes.ConvertAll( i => new IncomeDto()
            {
                Id = i.Id,
                Date = i.Date,
                Name = i.Name,
                Sum = i.Sum,
                TypeId = i.TypeId,
                TypeName = incomeTypes.Find( it => it.Id == i.TypeId ).Name,
                UserId = i.UserId
            } );
        }

        public void DeleteIncome( Int32 userId, Int32 incomeId )
        {
            var income = _incomeRepository.Get( incomeId );
            if ( income != null || income.UserId == userId )
            {
                _incomeRepository.Delete( income );
            }
        }

        public void SaveIncome( IncomeDto dto )
        {
            var income = new Income()
            {
                Id = dto.Id,
                Date = dto.Date,
                Name = dto.Name,
                Sum = dto.Sum,
                TypeId = dto.TypeId,
                UserId = dto.UserId
            };

            _incomeRepository.Save( income );

            dto.Id = income.Id;
        }

        public List<IncomeType> GetIncomeTypes()
        {
            return _incomeRepository.GetIncomeTypes();
        }
    }
}
