namespace Hangfire.Server.options
{
    public class ServerOptions
    {
        public const string ServerOptionKey = "ServerOptions";
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

    }
}
