namespace WebProjectAPI.Interface
{
    public interface IJwtAuthentication
    {
        string Authenticate(string userid, string password);
    }
}
