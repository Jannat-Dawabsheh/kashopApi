using kashop.dal.DTO.Request;
using kashop.dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kashop.dal.DTO.Response
{
    public class CategoryResponse
    {
        public int Id { get; set; }


        public Status Status { get; set; }
        public List<CategoryTranslationResponse> Translations { get; set; }
    }
}
