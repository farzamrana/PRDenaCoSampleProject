using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
namespace PRDenaCo.Web.Utilities
{
    public static class SessionExtension
    {
        public static void SetObject(ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObject<T>(ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}