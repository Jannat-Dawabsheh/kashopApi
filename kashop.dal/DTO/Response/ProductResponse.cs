using kashop.dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace kashop.dal.DTO.Response
{
    public class ProductResponse
    {
        public int Id { get; set; }

        public string CreatedBy { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Status Status { get; set; }
        public string MainImage {  get; set; }
        public List<CategoryTranslationResponse> Translations { get; set; }

    }
}
