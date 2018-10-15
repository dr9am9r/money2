using System;
using System.Collections.Generic;

namespace Money2.Application.Consumptions
{
    public interface IConsumptionService
    {
        List<String> GetConsumptionNames( Int32 userId );

        List<String> GetConsumptionPlaces( Int32 userId );

        ConsumptionDto GetConsumption( Int32 userId, Int32 consumptionId );

        List<ConsumptionDto> GetConsumptions( Int32 userId, DateTime? startDate, DateTime? endDate, Int32 typeId );

        void DeleteConsumption( Int32 userId, Int32 consumptionId );

        void SaveConsumption( ConsumptionDto dto );
    }
}
