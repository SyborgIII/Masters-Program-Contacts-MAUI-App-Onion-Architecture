using Contacts.UseCases.PluginInterfaces;

namespace Contacts.UseCases.Interfaces
{
    public interface IViewContactUseCase
    {
        IContactRepository ContactRepository { get; }

        Task<CoreBuisness.Contact> ExecuteAsync(int contactId);
    }
}