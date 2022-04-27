using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SimbioMed.MultiTenancy.HostDashboard.Dto;

namespace SimbioMed.MultiTenancy.HostDashboard
{
    public interface IIncomeStatisticsService
    {
        Task<List<IncomeStastistic>> GetIncomeStatisticsData(DateTime startDate, DateTime endDate,
            ChartDateInterval dateInterval);
    }
}