namespace Notipet.Web.SignalR
{
    public interface IHubClient
    {
        Task InformClient(string message);
    }
}
