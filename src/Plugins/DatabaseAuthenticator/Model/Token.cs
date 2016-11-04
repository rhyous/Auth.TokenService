using Rhyous.Auth.TokenService.Interface;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseAuthenticator
{
    [Table("Token")]
    public partial class Token : IToken
    {
        public int Id { get; set; }

        [Column("Token")]
        [Required]
        [StringLength(250)]
        public string Text { get; set; }

        public int UserId { get; set; }

        public DateTime CreateDate { get; set; }

        public virtual User User { get; set; }

        [NotMapped]
        IUser IToken.User
        {
            get { return User; }
            set { throw new NotImplementedException(); }
        }
        
    }
}
