using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kashop.dal.DTO.Request
{
    public class CategoryRequest
    {
       public List<CategoryTranslationRequest> Translations { get; set; }
    }
}
