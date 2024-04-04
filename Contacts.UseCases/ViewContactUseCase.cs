using Contacts.UseCases.Interfaces;
using Contacts.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contact = Contacts.CoreBuisness.Contact;

namespace Contacts.UseCases
{
    public class ViewContactUseCase : IViewContactUseCase
    {
        public IContactRepository ContactRepository { get; }
        public ViewContactUseCase(IContactRepository contactRepository)
        {
            ContactRepository = contactRepository;
        }



        public async Task<Contact> ExecuteAsync(int contactId)
        {
            return await this.ContactRepository.GetContactByIdAsync(contactId);
        }
    }
}
