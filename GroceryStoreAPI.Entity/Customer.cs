using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace GroceryStoreAPI.Entity
{
    public class Customer  
    {
        [Key]
        public int Id { get; set; }
        [JsonProperty("name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name is required")]
        public string Name { get; set; }
    }
}
