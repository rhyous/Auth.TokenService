using Rhyous.Auth.TokenService.Interface;
using System;

namespace Rhyous.Auth.TokenService.Database
{
    public partial class Token : IToken
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public int UserId { get; set; }

        public DateTime CreateDate { get; set; }

        public virtual IUser User { get; set; }
    }
}
