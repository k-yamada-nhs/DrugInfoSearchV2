//------------------------------------------------------------------------------
// <auto-generated>
//     このコードはテンプレートから生成されました。
//
//     このファイルを手動で変更すると、アプリケーションで予期しない動作が発生する可能性があります。
//     このファイルに対する手動の変更は、コードが再生成されると上書きされます。
// </auto-generated>
//------------------------------------------------------------------------------

namespace DrugInfoSearchV2.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Drugs
    {
        public int DrugId { get; set; }
        public string DrugCode { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public int ClassificationId { get; set; }
    
        public virtual Classifications Classifications { get; set; }
    }
}
