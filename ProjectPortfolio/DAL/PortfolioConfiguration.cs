using System.Data.Entity;
using System.Data.Entity.SqlServer;

namespace ProjectPortfolio.DAL
{
    public class PortfolioConfiguration : DbConfiguration
    {
        public PortfolioConfiguration()
        {
            SetExecutionStrategy("System.Data.SqlClient", () => new SqlAzureExecutionStrategy());
        }
    }
}