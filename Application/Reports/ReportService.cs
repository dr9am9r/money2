using System;
using System.Collections.Generic;
using System.Linq;
using Money2.Application.Reports.Charts;
using Money2.Application.Reports.Incomes;
using Money2.Application.Reports.Products;
using Money2.Domain.Consumptions;
using Money2.Domain.Incomes;
using Money2.Domain.Products;

namespace Money2.Application.Reports
{
    /// <summary>
    /// Сервис отчетов
    /// </summary>
    public class ReportService : IReportService
    {
        private const Int32 MonthsCount = 3;
        private Int32 _userId;

        private IConsumptionRepository _consumptionRepository;
        private IIncomeRepository _incomeRepository;
        private IProductRepository _productRepository;

        public ReportService(
            IConsumptionRepository consumptionRepository,
            IIncomeRepository incomeRepository,
            IProductRepository productRepository )
        {
            _consumptionRepository = consumptionRepository;
            _incomeRepository = incomeRepository;
            _productRepository = productRepository;
        }

        public ReportsDto GetReports( Int32 userId )
        {
            _userId = userId;

            var consumptions = _consumptionRepository.GetConsumptionReport( userId );
            var incomes = _incomeRepository.GetIncomeReport( userId );

            var report = new ReportsDto()
            {
                IncomeStats = _incomeRepository.GetIncomeTypeStats( userId )
                    .Select( x => new IncomeStatDto()
                    {
                        IncomeType = x.Key.Name,
                        Sum = x.Value
                    } ).OrderByDescending( x => x.Sum ).ToList(),
                ProductStats = GetProductStats( consumptions, userId ),
                Months = DateTimeHelper.GetLastMonths( MonthsCount )
            };

            var firstConsumption = _consumptionRepository.GetFirstConsumption( userId );
            if ( firstConsumption != null )
            {
                report.TotalMonths = DateTime.Now.Month - firstConsumption.Date.Month + 1;
            }
            else
            {
                report.TotalMonths = 1;
            }

            var charts = new List<ChartDto>();

            charts.Add( GetComplexChartDto( incomes, consumptions ) );
            charts.Add( GetIncomesChartDto( incomes ) );
            charts.Add( GetConsumptionsChartDto( consumptions ) );

            report.Charts = charts.ToArray();

            return report;
        }

        private List<Product> _products;
        protected List<Product> Products
        {
            get
            {
                if ( _products == null )
                {
                    _products = _productRepository.GetProducts( _userId );
                }

                return _products;
            }
        }

        private List<ProductType> _productTypes;
        protected List<ProductType> ProductTypes
        {
            get
            {
                if ( _productTypes == null )
                {
                    _productTypes = _productRepository.GetProductTypes();
                }

                return _productTypes;
            }
        }

        private ChartDto GetComplexChartDto( List<IncomeReport> incomes, List<ConsumptionReport> consumptions )
        {
            var model = new ChartDto();

            var dates = incomes.Select( x => x.Date ).Union( consumptions.Select( x => x.Date ) ).Distinct().OrderBy( x => x ).ToList();
            model.Labels = dates.Select( x => x.ToString( "MMM yyyy" ) ).ToArray();

            var series = new List<SeriesDto>();

            var incomePoints = new List<Decimal>();
            var incomeReport = incomes
                   .GroupBy( x => x.Date )
                   .ToDictionary( x => x.Key, x => x.Sum( y => y.Sum ) );
            foreach ( var date in dates )
            {
                var sum = incomeReport.ContainsKey( date ) ? incomeReport[ date ] : 0;
                incomePoints.Add( sum );
            }

            series.Add( new SeriesDto()
            {
                Name = "Доходы",
                Points = incomePoints.ToArray()
            } );

            var consumptionPoints = new List<Decimal>();
            var consumptionReport = consumptions
                   .GroupBy( x => x.Date )
                   .ToDictionary( x => x.Key, x => x.Sum( y => y.Sum ) );
            foreach ( var date in dates )
            {
                var sum = consumptionReport.ContainsKey( date ) ? consumptionReport[ date ] : 0;
                consumptionPoints.Add( sum );
            }

            series.Add( new SeriesDto()
            {
                Name = "Расходы",
                Points = consumptionPoints.ToArray()
            } );

            model.Series = series.ToArray();

            return model;
        }

        private ChartDto GetIncomesChartDto( List<IncomeReport> incomes )
        {
            incomes = incomes.OrderBy( x => x.Date ).ToList();

            var model = new ChartDto();

            var dates = incomes.Select( x => x.Date ).Distinct();
            model.Labels = dates.Select( x => x.ToString( "MMM yyyy" ) ).ToArray();

            var series = new List<SeriesDto>();
            var names = incomes.Select( x => x.Name ).Distinct();
            foreach ( var name in names )
            {
                var report = incomes
                   .Where( x => x.Name == name )
                   .GroupBy( x => x.Date )
                   .ToDictionary( x => x.Key, x => x.Sum( y => y.Sum ) );

                var points = new List<Decimal>();
                foreach ( var date in dates )
                {
                    var sum = report.ContainsKey( date ) ? report[ date ] : 0;
                    points.Add( sum );
                }

                series.Add( new SeriesDto()
                {
                    Name = name,
                    Points = points.ToArray()
                } );
            }

            model.Series = series.ToArray();

            return model;
        }

        private ChartDto GetConsumptionsChartDto( List<ConsumptionReport> consumptions )
        {
            consumptions = consumptions.OrderBy( x => x.Date ).ToList();

            var model = new ChartDto();

            var dates = consumptions.Select( x => x.Date ).Distinct();
            model.Labels = dates.Select( x => x.ToString( "MMM yyyy" ) ).ToArray();

            var series = new List<SeriesDto>();
            foreach ( var type in ProductTypes )
            {
                var names = Products.Where( x => x.TypeId == type.Id ).Select( x => x.Name ).ToList();

                var report = consumptions
                    .Where( x => names.Contains( x.Name ) )
                    .GroupBy( x => x.Date )
                    .ToDictionary( x => x.Key, x => x.Sum( y => y.Sum ) );
                var points = new List<Decimal>();

                foreach ( var date in dates )
                {
                    var sum = report.ContainsKey( date ) ? report[ date ] : 0;
                    points.Add( sum );
                }

                series.Add( new SeriesDto()
                {
                    Name = type.Name,
                    Points = points.ToArray()
                } );
            }

            model.Series = series.ToArray();

            return model;
        }

        private List<ProductStatDto> GetProductStats( List<ConsumptionReport> consumptions, Int32 userId )
        {
            var result = new List<ProductStatDto>();

            var dates = DateTimeHelper.GetLastMonthYears( 3 );

            var types = _productRepository.GetProductTypes();

            var totals = new Dictionary<ProductType, Decimal>();
            var sums1 = new Dictionary<ProductType, Decimal>();
            var sums2 = new Dictionary<ProductType, Decimal>();
            var sums3 = new Dictionary<ProductType, Decimal>();

            foreach ( var type in ProductTypes )
            {
                var names = Products.Where( x => x.TypeId == type.Id ).Select( x => x.Name ).ToList();

                var typeConsumptions = consumptions.Where( x => names.Contains( x.Name ) ).ToList();

                totals.Add( type, typeConsumptions.Sum( x => x.Sum ) );
                sums1.Add( type, typeConsumptions.Where( x => x.Date == dates[ 2 ] ).Sum( x => x.Sum ) );
                sums2.Add( type, typeConsumptions.Where( x => x.Date == dates[ 1 ] ).Sum( x => x.Sum ) );
                sums3.Add( type, typeConsumptions.Where( x => x.Date == dates[ 0 ] ).Sum( x => x.Sum ) );
            }

            foreach ( var total in totals )
            {
                var model = new ProductStatDto()
                {
                    Type = total.Key.Name,
                    Total = total.Value
                };

                model.Sums.Add( sums1.Where( x => x.Key.Id == total.Key.Id ).Sum( x => x.Value ) );
                model.Sums.Add( sums2.Where( x => x.Key.Id == total.Key.Id ).Sum( x => x.Value ) );
                model.Sums.Add( sums3.Where( x => x.Key.Id == total.Key.Id ).Sum( x => x.Value ) );

                result.Add( model );
            }

            return result.OrderByDescending( x => x.Total ).ToList();
        }
    }
}