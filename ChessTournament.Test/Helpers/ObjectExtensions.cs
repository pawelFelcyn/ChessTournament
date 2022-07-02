using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace ChessTournament.Test.Helpers
{
    internal static class ObjectExtensions
    {
        public static HttpContent ToJsonContent(this object obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            
            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}
