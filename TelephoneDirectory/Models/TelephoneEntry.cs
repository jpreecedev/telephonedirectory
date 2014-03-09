namespace TelephoneDirectory.Models
{
    using Newtonsoft.Json;

    public class TelephoneEntry
    {
        #region Public Properties

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("number")]
        public string Number { get; set; }

        #endregion
    }
}