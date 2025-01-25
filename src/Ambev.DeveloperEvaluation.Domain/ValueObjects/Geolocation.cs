using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.ValueObjects
{
    public class Geolocation : ValueObject
    {
        public string Latitude { get; private set; }
        public string Longitude { get; private set; }

        public Geolocation() { }

        public Geolocation(string latitude, string longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Latitude;
            yield return Longitude;
        }
    }
}
