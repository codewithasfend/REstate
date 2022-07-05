using System.Text.Json.Serialization;

namespace EcommerceSampleApi.Models
{
    public class Property
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
