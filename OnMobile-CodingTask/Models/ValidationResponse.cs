using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnMobileCodingTask.Models
{
    public class ValidationResponse
    {
        

        [JsonProperty("status")]
        public object[] Status { get; set; }
        [JsonProperty("packs")]
        public Packs[] Pack { get; set; }

    }

    public partial class Packs
    {
        [JsonProperty("packid")]
        public string Packid { get; set; }

    }

    public class SubscriptionResponse
    {
        [JsonProperty("ctid")]
        public string Ctid { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
}