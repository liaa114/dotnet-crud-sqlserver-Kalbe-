using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Employe.Models
{
    public class MST_Asset
    {
        public long Id { get; set; }  // PK, bigint
        public string? NewId { get; set; }  // varchar(100)
        public string? AssetNumber { get; set; }  // nvarchar(50)
        public string? Area { get; set; }  // nvarchar(50)
        public string? OwningDepartment { get; set; }  // nvarchar(50)
        public bool? IsActive { get; set; }  // bit
        public string? CreatedBy { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime? CreatedDate { get; set; }  // datetime
        public string? UpdatedBy { get; set; }  // varchar(50)
        public DateTime? UpdatedDate { get; set; }  // datetime
    }
}