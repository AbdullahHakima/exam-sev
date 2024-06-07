using Hangfire.Dashboard;

namespace examservice.Infrastructure.Data
{
    public class AllowAllDashboardAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            // Allow all users to see the Dashboard (for testing purposes only)
            return true;
        }
    }
}
