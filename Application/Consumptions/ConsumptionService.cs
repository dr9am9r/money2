using System;
using System.Collections.Generic;
using System.Linq;
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
            if ( consumption != null && consumption.UserId == userId )
            {
                _consumptionRepository.Delete( consumption );
            }
        }

        public List<ConsumptionDto> GetConsumptions( Int32 userId, DateTime? startDate, DateTime? endDate, Int32 typeId )
        {
            return _consumptionRepository.GetConsumptions( userId, startDate, endDate )
                .ConvertAll( c => new ConsumptionDto()
                {
                    Id = c.Id,
                    Date = c.Date,
                    Place = c.Place,
                    Sum = c.Sum,
                    UserId = c.UserId,
                    ConsumptionItems = c.ConsumptionItems.ConvertAll( ci => new ConsumptionItemDto()
                    {
                        Id = ci.Id,
                        Name = ci.Name,
                        Price = ci.Price,
                        Quantity = ci.Quantity
                    } )
                } );
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
                UserId = consumption.UserId,
                ConsumptionItems = consumption.ConsumptionItems.ConvertAll( ci => new ConsumptionItemDto()
                {
                    Id = ci.Id,
                    Name = ci.Name,
                    Price = ci.Price,
                    Quantity = ci.Quantity
                } )
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
                Place = dto.Place,
                UserId = dto.UserId,
                ConsumptionItems = dto.ConsumptionItems.ConvertAll( ci => new ConsumptionItem()
                {
                    Name = ci.Name,
                    Price = ci.Price,
                    Quantity = ci.Quantity
                } )
            };

            consumption.Sum = dto.ConsumptionItems.Sum( x => x.Quantity * x.Price );

            if( consumption.Id != 0 )
            {
                _consumptionRepository.Clear( consumption.Id );
            }

            _consumptionRepository.Save( consumption );

            dto.Id = consumption.Id;
        }       
    }
}
