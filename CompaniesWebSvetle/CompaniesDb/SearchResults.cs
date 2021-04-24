using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompaniesDb
{
    public class SearchResultPage
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Website { get; set; }
        public string Area { get; set; }
        public string About { get; set; }
        public DateTime Modified { get; set; }
    }

    public class SearchResults
    {
        public long Count { get; set; }
        public List<SearchResultPage> Page { get; set; }
    }

    public class SearchFilter
    {
        public string Search { get; set; }
        public int? AreaId { get; set; }
    }
}
