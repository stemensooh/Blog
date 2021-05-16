using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BLOGCORE.APPLICATION.Core.Entities
{
    public class PostVistasAnonimas
    {
        [Key]
        public long Id { get; set; }


        [ForeignKey("Post")]
        public long PostId { get; set; }


        public DateTime FechaVista { get; set; }


        [MaxLength(20)]
        public string Ip { get; set; }



        public virtual Post PostNavigation { get; set; }
    }
}
