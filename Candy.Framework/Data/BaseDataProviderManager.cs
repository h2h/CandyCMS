using System;
using Candy.Framework.Configuration;

namespace Candy.Framework.Data
{
    public abstract class BaseDataProviderManager
    {
        protected BaseDataProviderManager(CandyConfig config)
        {
            if (config == null)
                throw new ArgumentNullException("config");

            this.Config = config;
        }

        protected CandyConfig Config { get; private set; }

        public abstract IDataProvider LoadDataProvider();
    }
}