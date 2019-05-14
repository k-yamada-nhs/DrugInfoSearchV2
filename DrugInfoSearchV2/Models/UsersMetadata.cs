using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DrugInfoSearchV2.Models
{
    [MetadataType(typeof(UsersMetadata))]
    public partial class Users
    {
        [Required]
        [NotMapped]
        [DisplayName("ロール")]
        public List<int> RoleId { get; set; }
    }

    public class UsersMetadata
    {
        public int UserId { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}