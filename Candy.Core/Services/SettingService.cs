using System;
using System.Linq;
using System.Reflection;
using System.Linq.Expressions;
using System.Collections.Generic;

using Candy.Core.Domain;

using Candy.Framework;
using Candy.Framework.Data;
using Candy.Framework.Caching;
using Candy.Framework.Configuration;

namespace Candy.Core.Services
{
    public partial class SettingService : ISettingService
    {
        private const string SETTINGS_ALL_KEY = "Candy.Setting.All";
        private const string SETTINGS_PATTERN_KEY = "Candy.Setting.";

        private readonly IRepository<Setting> _settingRepository;
        private readonly ICacheManager _cacheManager;

        public SettingService(ICacheManager cacheManager, IRepository<Setting> settingRepository)
        {
            this._cacheManager = cacheManager;
            this._settingRepository = settingRepository;
        }

        protected virtual IDictionary<string, IList<Setting>> GetAllSettingsCached()
        {
            string key = string.Format(SETTINGS_ALL_KEY);

            return _cacheManager.Get(key, () =>
            {
                var settings = _settingRepository.TableNoTracking.ToList();
                var dictionary = new Dictionary<string, IList<Setting>>();

                foreach (var s in settings)
                {
                    var settingKey = s.Name.ToLower();

                    if (!dictionary.ContainsKey(settingKey))
                        dictionary.Add(settingKey, new List<Setting> { s });
                    else
                        dictionary[settingKey].Add(s);
                }

                return dictionary;
            });
        }

        public Setting GetSettingById(int settingId)
        {
            if (settingId == 0)
                return null;

            return _settingRepository.GetById(settingId);
        }
        public void DeleteSetting(Setting setting)
        {
            if (setting == null)
                throw new ArgumentNullException("setting");

            _settingRepository.Delete(setting);
        }
        public Setting GetSetting(string key)
        {
            if (string.IsNullOrEmpty(key))
                return null;

            var settings = GetAllSettingsCached();
            key = key.Trim().ToLower();
            if (settings.ContainsKey(key))
            {
                var settingsByKey = settings[key];
                var setting = settingsByKey.FirstOrDefault();

                if (setting != null)
                    return GetSettingById(setting.Id);
            }

            return null;
        }
        public virtual IList<Setting> GetAllSettings()
        {
            return _settingRepository.Table.ToList();
        }
        public virtual T GetSettingByKey<T>(string key, T defaultValue = default(T))
        {
            if (string.IsNullOrEmpty(key))
                return defaultValue;

            var settings = GetAllSettingsCached();
            key = key.Trim().ToLower();
            if (settings.ContainsKey(key))
            {
                var setting = settings[key].FirstOrDefault();

                if (setting != null)
                    return CommonHelper.To<T>(setting.Value);
            }
            return defaultValue;
        }
        public virtual T LoadSetting<T>() where T : ISettings, new()
        {
            var settings = Activator.CreateInstance<T>();

            foreach (var prop in typeof(T).GetProperties())
            {
                if (!prop.CanRead || !prop.CanWrite)
                    continue;

                var key = typeof(T).Name + "." + prop.Name;
                var setting = GetSettingByKey<string>(key);
                if (setting == null)
                    continue;

                if (!CommonHelper.GetCustomTypeConverter(prop.PropertyType).CanConvertFrom(typeof(string)))
                    continue;

                if (!CommonHelper.GetCustomTypeConverter(prop.PropertyType).IsValid(setting))
                    continue;

                var value = CommonHelper.GetCustomTypeConverter(prop.PropertyType).ConvertFromString(setting);

                prop.SetValue(settings, value, null);
            }
            return settings;
        }
        public virtual void SaveSetting<T>(T settings) where T : ISettings, new()
        {
            foreach (var prop in typeof(T).GetProperties())
            {
                if (!prop.CanRead || !prop.CanWrite)
                    continue;

                if (!CommonHelper.GetCustomTypeConverter(prop.PropertyType).CanConvertFrom(typeof(string)))
                    continue;

                string key = typeof(T).Name + "." + prop.Name;
                // 因为 C# 不支持 Duck typing ， 所以这里使用 Dynamic
                dynamic value = prop.GetValue(settings, null);
                if (value != null)
                    SetSetting(key, value,  false);
                else
                    SetSetting(key, "", false);
            }

            //清除缓存
            ClearCache();
        }
        public virtual void SaveSetting<T, TPropType>(T settings, Expression<Func<T, TPropType>> keySelector, bool clearCache = true) where T : ISettings, new()
        {
            var member = keySelector.Body as MemberExpression;
            if (member == null)
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a method, not a property.",
                    keySelector));

            var propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
                throw new ArgumentException(string.Format(
                       "Expression '{0}' refers to a field, not a property.",
                       keySelector));

            var key = typeof(T).Name + "." + propInfo.Name;
            // 因为 C# 不支持 Duck typing ， 所以这里使用 Dynamic
            dynamic value = propInfo.GetValue(settings, null);
            if (value != null)
                SetSetting(key, value, clearCache);
            else
                SetSetting(key, "", clearCache);
        }
        public virtual void SetSetting<T>(string key, T value, bool clearCache = true)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            key = key.Trim().ToLower();
            string valueStr = CommonHelper.GetCustomTypeConverter(typeof(T)).ConvertToInvariantString(value);

            var allSettings = GetAllSettingsCached();
            var settingForCaching = allSettings.ContainsKey(key) ? allSettings[key].FirstOrDefault() : null;
            if (settingForCaching != null)
            {
                //更新数据
                var setting = GetSettingById(settingForCaching.Id);
                setting.Value = valueStr;
                UpdateSetting(setting, clearCache);
            }
            else
            {
                //插入数据
                var setting = new Setting
                {
                    Name = key,
                    Value = valueStr,
                    AutoLoad = true
                };
                InsertSetting(setting, clearCache);
            }
        }
        public virtual void UpdateSetting(Setting setting, bool clearCache = true)
        {

            if (setting == null)
                throw new ArgumentNullException("setting");

            _settingRepository.Update(setting);

            //清除缓存
            if (clearCache)
                _cacheManager.RemoveByPattern(SETTINGS_PATTERN_KEY);
        }
        public virtual void InsertSetting(Setting setting, bool clearCache = true)
        {
            if (setting == null)
                throw new ArgumentNullException("setting");

            _settingRepository.Insert(setting);

            //清除缓存
            if (clearCache)
                _cacheManager.RemoveByPattern(SETTINGS_PATTERN_KEY);
        }

        public virtual void ClearCache()
        {
            _cacheManager.RemoveByPattern(SETTINGS_PATTERN_KEY);
        }
    }
}
