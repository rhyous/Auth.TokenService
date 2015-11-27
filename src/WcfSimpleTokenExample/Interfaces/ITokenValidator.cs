using WcfSimpleTokenExample.Database;
namespace WcfSimpleTokenExample.Interfaces
{
    public interface ITokenValidator
    {
        bool IsValid(string token);
        Token Token { get; set; }
    }
}