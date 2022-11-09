namespace WebProjectAPI.Interface
{
    public interface ICustomAuthentication 
    {
        string Authenticate(string userid, string password);
        IDictionary<string, Tuple<string,string>> Tokens { get; }

    }
}
