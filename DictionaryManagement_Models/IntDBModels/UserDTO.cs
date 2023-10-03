using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DictionaryManagement_Models.IntDBModels
{
    public class UserDTO
    {

        [Display(Name = "Ид записи")]
        [Required(ErrorMessage = "ИД обязателен")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Логин обязателен для заполнения")]
        [StringLength(250, MinimumLength = 1, ErrorMessage = "Логин может быть от 1 до 250 символов")]        
        [Display(Name = "Логин")]
        public string Login { get; set; }

        [Required(ErrorMessage = "ФИО обязателено для заполнения")]
        [StringLength(250, MinimumLength = 3, ErrorMessage = "ФИО может быть от 3 до 250 символов")]
        [Display(Name = "ФИО")]
        public string? UserName { get; set; }
                
        [Display(Name = "Описание")]
        public string? Description { get; set; }

        [Display(Name = "В архиве")]
        public bool IsArchive { get; set; }

        [Required(ErrorMessage = "Параметр является обязательным")]
        [Display(Name = "Синх с AD")]
        public bool IsSyncWithAD { get; set; } = true;

        [Required(ErrorMessage = "Параметр является обязательным")]
        [Display(Name = "Время последней синхронизации с группами AD")]
        public DateTime SyncWithADGroupsLastTime { get; set; } = (DateTime) System.Data.SqlTypes.SqlDateTime.MinValue;

        [NotMapped]
        [Display(Name = " ")]
        public bool Checked { get; set; } = false;

        [NotMapped]
        [Display(Name = "Ид записи")]
        public string ToStringId
        {
            get
            {
                return Id.ToString();
            }
            set
            {
                ToStringId = value;
            }
        }

        //[NotMapped]
        //[Display(Name = "Имя и логин")]
        //public string UserNameAndLogin
        //{
        //    get
        //    {
        //        return UserName + " " + Login;
        //    }
        //    set
        //    {
        //        UserNameAndLogin = value;
        //    }
        //}

    }
}

