namespace WebNewsMVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Users
    {
        [Key]
        public int User_ID { get; set; }

        [Display(Name = "Họ tên")]
        [StringLength(255)]
        public string User_Name { get; set; }

        [Display(Name = "Điện thoại")]
        [StringLength(20)]
        public string User_Phone { get; set; }

        [Display(Name = "Email")]
        [StringLength(255)]
        public string User_Email { get; set; }

        [Display(Name = "Tài khoản")]
        [StringLength(50)]
        public string User_UserName { get; set; }

        [Display(Name = "Mật khẩu")]
        [StringLength(255)]
        public string User_Password { get; set; }

        [Display(Name = "Quyền")]
        [StringLength(50)]
        public string User_Role { get; set; }
    }
}
