using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Mirutrading.Application.ViewModel.Admin
{
    public class LoginRequest
    {
        [Required]
        [Display(Name="用户名")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name="密码")]
        public string Password { get; set; }
    }
}
