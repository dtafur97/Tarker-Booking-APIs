using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tarker.Booking.Domain.Models.ApplicationInsights;

namespace Tarker.Booking.Application.External.ApplicationInsights
{
    public interface IInsertAplicationInsightsService
    {
        bool Execute(InsertApplicationInsightsModel metric);
    }
}
