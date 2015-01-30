using Candy.Framework.Configuration;

namespace Candy.Framework.Data.DbWrench
{
    public partial class DbWrenchDataProviderManager : BaseDataProviderManager
    {
        public DbWrenchDataProviderManager(CandyConfig config)
            : base(config)
        { }

        public override IDataProvider LoadDataProvider()
        {
            var providerName = Config.DataProviderName;

            if (string.IsNullOrWhiteSpace(providerName))
                throw new CandyException("Config doesn't contain a providerName");

            switch (providerName.ToLowerInvariant())
            {
                case "sqlserver":
                    return new SqlServerDataProvider();

                default:
                    throw new CandyException(string.Format("Not supported dataprovider name: {0}", providerName));
            }
        }
    }
}