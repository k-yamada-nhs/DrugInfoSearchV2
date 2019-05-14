using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DrugInfoSearchV2.Models
{
    [MetadataType(typeof(DrugsMetaData))]
    public partial class Drugs
    {

    }

    public class DrugsMetaData
    {
        [DisplayName("薬品ID")]
        public int DrugId { get; set; }

        [DisplayName("薬品コード")]
        public string DrugCode { get; set; }

        [DisplayName("薬品名")]
        [Required(ErrorMessage = "薬品名は必須項目です。")]
        public string Name { get; set; }

        [DisplayName("会社名")]
        public string Company { get; set; }
    }
}