using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kashop.dal.DTO.Response
{
    public class PaginationResponse<T>
    {
        public int TotalCount {  get; set; }
        public int Page { get; set; }
        public int Limit { get; set; }
        public List<T> Data { get; set; }
    }
}
