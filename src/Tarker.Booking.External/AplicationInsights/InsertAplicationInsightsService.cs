using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Configuration;
using Tarker.Booking.Application.External.ApplicationInsights;
using Tarker.Booking.Domain.Models.ApplicationInsights;

namespace Tarker.Booking.External.AplicationInsights
{
    public class InsertAplicationInsightsService : IInsertAplicationInsightsService
    {
        private readonly IConfiguration _configuration;

        public InsertAplicationInsightsService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool Execute(InsertApplicationInsightsModel metric)
        {
            if (metric == null)
                throw new ArgumentNullException(nameof(metric));

            TelemetryConfiguration config = new TelemetryConfiguration();
            config.ConnectionString = _configuration["ApplicationInsightsConnectionString"];

            var _telemetryCliente = new TelemetryClient(config);

            var properties = new Dictionary<string, string>
            {
                {"Id", metric.Id },
                {"Type", metric.Type },
                {"Content", metric.Content },
                {"Detail", metric.Detail }
            };

            _telemetryCliente.TrackTrace(metric.Type, SeverityLevel.Information, properties);

            return true;
        }
    }
}
