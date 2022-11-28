using Microsoft.Extensions.Localization;

namespace E_Commerce.Data.Localization
{
    public class JsonStringLocalizerFactory : IStringLocalizerFactory
    {
        private readonly IDistributedCache _cashe;

        public JsonStringLocalizerFactory(IDistributedCache cashe)
        {
            _cashe = cashe;
        }

        public IStringLocalizer Create(Type resourceSource)
        {
            return new JsonStringLocalization(_cashe);
        }

        public IStringLocalizer Create(string baseName, string location)
        {
            return new JsonStringLocalization(_cashe);
        }
    }
}
