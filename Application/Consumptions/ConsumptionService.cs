using System;
using System.Collections.Generic;
using Money2.Domain.Consumptions;

namespace Money2.Application.Consumptions
{
    public class ConsumptionService : IConsumptionService
    {
        private readonly IConsumptionRepository _consumptionRepository;

        public ConsumptionService( IConsumptionRepository consumptionRepository )
        {
            _consumptionRepository = consumptionRepository;
        }

        public void DeleteConsumption( Int32 userId, Int32 consumptionId )
        {
            var consumption = _consumptionRepository.Get( consumptionId );
            if ( consumption != null || consumption.UserId == userId )
            {
                _consumptionRepository.Delete( consumption );
            }
        }

        public List<ConsumptionDto> GetConsumptions( Int32 userId, DateTime? startDate, DateTime? endDate, Int32 typeId )
        {
            throw new NotImplementedException();
        }

        public ConsumptionDto GetConsumption( Int32 userId, Int32 consumptionId )
        {
            Consumption consumption = _consumptionRepository.Get( consumptionId );
            if ( consumption == null || consumption.UserId != userId )
            {
                return null;
            }

            consumption.ConsumptionItems = _consumptionRepository.GetConsumptionItems( consumption.Id );

            return new ConsumptionDto()
            {
                Id = consumption.Id,
                Date = consumption.Date,
                Place = consumption.Place,
                Sum = consumption.Sum,
                UserId = consumption.UserId
            };
        }

        public List<String> GetConsumptionNames( Int32 userId )
        {
            return _consumptionRepository.GetConsumptionNames( userId );
        }

        public List<String> GetConsumptionPlaces( int userId )
        {
            return _consumptionRepository.GetConsumptionPlaces( userId );
        }

        public void SaveConsumption( ConsumptionDto dto )
        {
            var consumption = new Consumption()
            {
                Id = dto.Id,
                Date = dto.Date,
                Sum = dto.Sum,
                UserId = dto.UserId
            };

            //consumption.Sum = consumption.ConsumptionItems.Sum( x => x.Quantity * x.Price );

            _consumptionRepository.Save( consumption );
        }       
    }
}
