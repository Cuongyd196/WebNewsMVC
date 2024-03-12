namespace WebNewsMVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Categorys
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Categorys()
        {
            News = new HashSet<News>();
        }

        [Key]
        public int Category_ID { get; set; }

        [Required(ErrorMessage = "Tên danh mục là bắt buộc nhập")]
        [StringLength(255)]
        [Display(Name = "Tên danh mục")]
        public string Category_Name { get; set; }

        [StringLength(255)]
        [Display(Name = "Mô tả")]
        public string Category_Note { get; set; }

        [Display(Name = "Trạng thái")]
        public int? Category_Status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<News> News { get; set; }
    }
}
