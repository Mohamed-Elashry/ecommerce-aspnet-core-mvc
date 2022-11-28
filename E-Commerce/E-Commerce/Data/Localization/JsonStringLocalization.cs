using Newtonsoft.Json;
using System.IO;

namespace E_Commerce.Data.Localization
{
    public class JsonStringLocalization : IStringLocalizer
    {
        private readonly JsonSerializer _serializers = new();
        private readonly IDistributedCache _cache;
        public JsonStringLocalization(IDistributedCache cache)
        {
            _cache = cache;
        }

        public LocalizedString this[string name]
        {
            get
            {
                var value = GetString(name);
                return new(name, value);
            }
        }

        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                var actualValue = this[name];
                return !actualValue.ResourceNotFound
                    ? new(name, string.Format(actualValue, arguments))
                    : actualValue;
            }
        }

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            var filePath = $"Resources/{Thread.CurrentThread.CurrentCulture.Name}.json";
            using FileStream fileStream = new(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            using StreamReader streamReader = new(fileStream);
            using JsonTextReader reader = new(streamReader);
            while (reader.Read())
            {
                if (reader.TokenType != JsonToken.PropertyName)
                    continue;

                var key = reader.Value as string;
                reader.Read();
                var value = _serializers.Deserialize<string>(reader);
                yield return new(key, value);
            }
        }
        private string GetString(string Key)
        {
            var filePath = Path.GetFullPath($"Resources/{Thread.CurrentThread.CurrentCulture.Name}.json");
            if (File.Exists(filePath))
            {
                // Cashe Work
                var casheKey = $"local_{Thread.CurrentThread.CurrentCulture.Name}_{Key}";
                var casheValue = _cache.GetString(casheKey);

                if(!string.IsNullOrEmpty(casheValue))
                    return casheValue;

                var result =  GetValueFromString(Key, filePath);
                if(!string.IsNullOrEmpty(result))
                    _cache.SetString(casheKey, result);

                return result;
            }
               
            return string.Empty;

        }
        private string GetValueFromString(string propertyName, string filePath)
        {
            try
            {
                if (string.IsNullOrEmpty(propertyName) || string.IsNullOrEmpty(filePath))
                    return string.Empty;
                using FileStream fileStream = new(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                using StreamReader streamReader = new(fileStream);
                using JsonTextReader reader = new(streamReader);
                while (reader.Read())
                {
                    if (reader.TokenType == JsonToken.PropertyName && reader?.Value.ToString() == propertyName)
                    {
                        reader.Read();
                        return _serializers.Deserialize<string>(reader);
                    }
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
    }
}
