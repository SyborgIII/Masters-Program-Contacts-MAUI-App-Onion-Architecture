using Contacts.Maui.Models;
using Contacts.UseCases.Interfaces;
using Contact = Contacts.Maui.Models.Contact;

namespace Contacts.Maui.Views;
//we use this attribute to pass data to this class. i think this will happen upon creation. and thereofre i can set values 
//equal to whatever i want, however, if i dont do it thisway. make sure to instantiate in the constructor for now
[QueryProperty(nameof(ContactId), "Id")]
public partial class EditContactPage : ContentPage
{
	private CoreBuisness.Contact contact;
    private readonly IViewContactUseCase viewContactUseCase;
	private IEditContactUseCase editContactUseCase;

    public EditContactPage(IViewContactUseCase viewContactUseCase, IEditContactUseCase editContactUseCase)
	{
		InitializeComponent();
        this.viewContactUseCase = viewContactUseCase;
        this.editContactUseCase = editContactUseCase;
    }
	

	private void btnCancel_Clicked(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync("..");
	}
	//this logic will trigger each time this property gets set, which at this point at video 8
	//or so is done by the attribute binding at the top this is eseentially the query strind handeler. 
	//ContactId is the value from the query string
	public string ContactId 
	{
		set 
		{
			//setting values of the entries upon binding
			contact = viewContactUseCase.ExecuteAsync(int.Parse(value)).GetAwaiter().GetResult();
			if(contact != null) 
			{
                contactCtrl.Name = contact.Name;
                contactCtrl.Email = contact.Email;
                contactCtrl.Phone = contact.Phone;
                contactCtrl.Address = contact.Address;

            }
			
		}
	}

    

    private async void btnUpdate_Clicked(object sender, EventArgs e)
    {

		contact.Name = contactCtrl.Name;
		contact.Email = contactCtrl.Email;
		contact.Phone = contactCtrl.Phone;
		contact.Address = contactCtrl.Address;

		await editContactUseCase.ExecuteAsync(contact.ContactId, contact);
       // ContactRepository.UpdateContact(contact.ContactId, contact);
        await Shell.Current.GoToAsync("..");	
    }

    private void contactCtrl_OnError(object sender, string e)
    {
		DisplayAlert("Error", e, "Ok");
    }
}