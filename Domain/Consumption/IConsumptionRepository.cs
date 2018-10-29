using System;
using System.Collections.Generic;

namespace Money2.Domain.Consumptions
{
    /// <summary>
    /// Репозиторий Расходов
    /// </summary>
    public interface IConsumptionRepository
    {
        void Clear( Int32 consumptionId );

        void Delete( Consumption consumption );

        Consumption Get( Int32 id );

        List<ConsumptionItem> GetConsumptionItems( Int32 consumptionId );

        List<String> GetConsumptionNames( Int32 userId );

        List<String> GetConsumptionPlaces( Int32 userId );

        List<ConsumptionReport> GetConsumptionReport( Int32 userId );

        List<Consumption> GetConsumptions( Int32 userId, DateTime? startDate, DateTime? endDate );

        Consumption GetFirstConsumption( Int32 userId );

        void Save( Consumption consumption );
    }
}
