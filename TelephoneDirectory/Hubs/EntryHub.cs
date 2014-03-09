namespace TelephoneDirectory.Hubs
{
    using Microsoft.AspNet.SignalR;

    using TelephoneDirectory.Models;

    public class EntryHub : Hub
    {
        public void AddOrUpdate(TelephoneEntry telephoneEntry)
        {
            Clients.All.addOrUpdate(telephoneEntry);
        }

        public void Delete(TelephoneEntry telephoneEntry)
        {
            Clients.All.delete(telephoneEntry.Id);
        }
    }
}