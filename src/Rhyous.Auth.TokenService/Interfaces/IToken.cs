using System;

namespace Rhyous.Auth.TokenService.Interface
{
    public interface IToken
    {
        DateTime CreateDate { get; set; }
        int Id { get; set; }
        string Text { get; set; }
        IUser User { get; set; }
        int UserId { get; set; }
    }
}