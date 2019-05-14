using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace DrugInfoSearchV2.Models
{
    public class SearchViewModel
    {
        [DisplayName("薬品名")]
        public string DrugName { get; set; }

        [DisplayName("薬効分類")]
        public int ClassificationId { get; set; }

        public List<Drugs> Drugs { get; set; }
    }
}