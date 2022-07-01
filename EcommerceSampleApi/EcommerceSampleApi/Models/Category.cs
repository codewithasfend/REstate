using System.Text.Json.Serialization;

namespace EcommerceSampleApi.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }    
        [JsonIgnore]
        public ICollection<Property> Properties { get; set; }   
    }
}
