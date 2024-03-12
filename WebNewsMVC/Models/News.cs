namespace WebNewsMVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class News
    {
        [Key]
        public int New_ID { get; set; }

        [Display(Name = "Tên danh mục")]
        public int? Category_ID { get; set; }

        [Display(Name = "Tiêu đề")]
        [StringLength(255)]
        public string New_Title { get; set; }

        [Display(Name = "Mô tả")]
        public string New_Summary { get; set; }

        [Display(Name = "Ảnh đại diện")]
        [StringLength(255)]
        public string New_img { get; set; }

        [Display(Name = "Nội dung")]
        public string New_Content { get; set; }

        [Display(Name = "Ngày đăng")]
        [StringLength(50)]
        public string New_Date { get; set; }

        [Display(Name = "Trạng thái")]
        public int? New_Status { get; set; }

        [Display(Name = "Người đăng")]
        [StringLength(255)]
        public string New_User_Name { get; set; }

        public virtual Categorys Categorys { get; set; }
    }
}
