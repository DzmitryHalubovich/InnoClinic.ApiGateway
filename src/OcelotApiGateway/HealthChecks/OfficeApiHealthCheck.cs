using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Net;

namespace OcelotApiGateway.HealthChecks
{
    public class OfficeApiHealthCheck : IHealthCheck
    {
        private readonly HttpClient _httpClient;

        public OfficeApiHealthCheck(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7255/");
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _httpClient.GetAsync("_health");

                return HealthCheckResult.Healthy();
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy(exception: ex);
            }
        }
    }
}
