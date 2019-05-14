using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// 自動生成されたクラスに手を入れることなくDisplayNameなどを追加できる
/// </summary>

namespace DrugInfoSearchV2.Models
{
    [MetadataType(typeof(ClassificationMetadata))]
    public partial class Classifications
    {

    }

    public class ClassificationMetadata
    {
        [DisplayName("薬効分類ID")]
        public int ClassificationId { get; set; }

        [DisplayName("薬効分類コード")]
        [Required(ErrorMessage ="薬効分類コードは必須項目です。")]
        [StringLength(3, ErrorMessage ="薬効分類コードは3桁以内で入力してください。")]
        public string ClassificationCode { get; set; }

        [DisplayName("薬効分類")]
        [Required(ErrorMessage ="薬効分類名称は必須項目です。")]
        public string Name { get; set; }
    }
}