using System;

namespace Rhyous.Auth.TokenService.Interface
{
    public interface IToken
    {
        int Id { get; set; }
        string Text { get; set; }
        IUser User { get; set; }
        int UserId { get; set; }
        DateTime CreateDate { get; set; }
    }
}