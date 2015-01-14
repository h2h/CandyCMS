using System;
using Candy.Framework.Configuration;
using Candy.Framework.Data;

namespace Candy.Framework.Data.EF
{
    public partial class EfDataProviderManager : BaseDataProviderManager
    {
        public EfDataProviderManager(CandyConfig config)
            :base(config)
        { 
        }

        public override IDataProvider LoadDataProvider()
        {
            var providerName = Config.DataProviderName;
            if (string.IsNullOrWhiteSpace(providerName))
                throw new CandyException("Data Settings doesn't contain a providerName");

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
