namespace Ambev.DeveloperEvaluation.WebApi.Responses
{
    public class AddressResponse
    {
        public string City { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public int Number { get; set; }
        public string ZipCode { get; set; } = string.Empty;
        public GeolocationResponse Geolocation { get; set; }
    }
}
