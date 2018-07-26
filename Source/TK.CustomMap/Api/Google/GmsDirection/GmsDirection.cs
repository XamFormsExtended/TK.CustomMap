using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TK.CustomMap.Api.Google
{
    /// <summary>
    ///     Calls the Google Maps Directions API to get a route
    /// </summary>
    public class GmsDirection
    {
        private static string _apiKey;
        private const string _baseUrl = "https://maps.googleapis.com/maps/api/directions/";
        private static GmsDirection _instance;

        private readonly HttpClient _httpClient;

        /// <summary>
        ///     Creates a new instance of <see cref="GmsDirection" />
        /// </summary>
        private GmsDirection()
        {
            _httpClient = new HttpClient {BaseAddress = new Uri(_baseUrl)};
        }

        /// <summary>
        ///     The <see cref="GmsDirection" /> instance
        /// </summary>
        public static GmsDirection Instance => _instance ?? (_instance = new GmsDirection());

        /// <summary>
        ///     Calculates a route
        /// </summary>
        /// <param name="origin">The origin</param>
        /// <param name="destination">The destination</param>
        /// <param name="mode">The travelling mode</param>
        /// <param name="language">The language</param>
        /// <returns>A <see cref="GmsDirectionResult" /></returns>
        public async Task<GmsDirectionResult> CalculateRoute(Position origin, Position destination, GmsDirectionTravelMode mode, string language = null)
        {
            var response = await _httpClient.GetAsync(BuildQueryString(origin, destination, mode, language));

            if (!response.IsSuccessStatusCode) return null;

            var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<GmsDirectionResult>(result);
        }

        /// <summary>
        ///     Set the API key
        /// </summary>
        /// <param name="apiKey">Google Maps API key</param>
        public static void Init(string apiKey) => _apiKey = apiKey;

        /// <summary>
        ///     Builds the query string for the Google Maps Directions API call
        /// </summary>
        /// <param name="origin">The origin</param>
        /// <param name="destination">The destination</param>
        /// <param name="mode">The travelling mode</param>
        /// <param name="language">The language</param>
        /// <returns>The query string</returns>
        private static string BuildQueryString(Position origin, Position destination, GmsDirectionTravelMode mode, string language)
        {
            var baseString = $"json?origin={origin.AsString()}&destination={destination.AsString()}&mode={mode.ToString().ToLower()}";
            var strBuilder = new StringBuilder(baseString);

            if (!string.IsNullOrWhiteSpace(language)) strBuilder.AppendFormat("&language={0}", language);
            strBuilder.AppendFormat("&key={0}", _apiKey);
            return strBuilder.ToString();
        }
    }
}