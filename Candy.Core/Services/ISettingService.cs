using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Candy.Core.Domain;
using Candy.Framework.Configuration;

namespace Candy.Core.Services
{
    public partial interface ISettingService
    {
        Setting GetSettingById(int settingId);

        void DeleteSetting(Setting setting);

        Setting GetSetting(string key);

        IList<Setting> GetAllSettings();

        T LoadSetting<T>() where T : ISettings, new();

        void SaveSetting<T>(T settings) where T : ISettings, new();

        void SaveSetting<T, TPropType>(T settings, Expression<Func<T, TPropType>> keySelector, bool clearCache = true) where T : ISettings, new();

        T GetSettingByKey<T>(string key, T defaultValue = default(T));

        void SetSetting<T>(string key, T value, bool clearCache = true);

        void UpdateSetting(Setting setting, bool clearCache = true);

        void InsertSetting(Setting setting, bool clearCache = true);

        void ClearCache();
    }
}