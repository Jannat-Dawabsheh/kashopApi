using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kashop.dal.Models
{
    public class Category: BaseModel
    {
       
        
        public List<CaregoryTranslation> Translations { get; set; }

    }
}
