using Contacts.Maui.Models;
using Contacts.UseCases.Interfaces;
using System.Collections.ObjectModel;
using Contact = Contacts.CoreBuisness.Contact;

namespace Contacts.Maui.Views;

public partial class ContactsPage : ContentPage
{
    public IViewContactsUseCase ViewContactsUseCase;

    public IDeleteContactUseCase DeleteContactUseCase;

    public ContactsPage(IViewContactsUseCase viewContactsUseCase, IDeleteContactUseCase deleteContactUseCase)
	{
		InitializeComponent();
        ViewContactsUseCase = viewContactsUseCase;
        DeleteContactUseCase = deleteContactUseCase;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        SearchBar.Text = string.Empty;
        LoadContacts(); 

    }

    private async  void  listContacts_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        //when using the ListView, it has a bug on ItemSelected, it will do it twice,
        //MAKE SURE TO PLACE THE IF LOGIC TO ELIMINATE IT
        if (listContacts.SelectedItem != null)
        {
            //needed to cast the object so c# could know what it was at complie time and then call the method
           await Shell.Current.GoToAsync($"{nameof(EditContactPage)}?Id={((Contact)listContacts.SelectedItem).ContactId}");
        }
        
    }

    private void listContacts_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        //this is how we can deselect
        listContacts.SelectedItem = null;

    }

    private void btnAdd_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync(nameof(AddContactPage));
    }

    private async void Delete_Clicked(object sender, EventArgs e)
    {
        var menuItem = sender as MenuItem;
        var contact = menuItem.CommandParameter as Contact;
        //ContactRepository.DeleteContact(contact.ContactId);
        await DeleteContactUseCase.ExecuteAsync(contact.ContactId);
        LoadContacts();
    }

    private async void LoadContacts() 
    {
        var contacts =  new ObservableCollection<Contact>(await this.ViewContactsUseCase.ExecuteAsync(string.Empty));

       
        listContacts.ItemsSource = contacts;
    }

    private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        // var contacts = new ObservableCollection<Contact>(ContactRepository.SearchContacts(((SearchBar)sender).Text));
        var contacts = new ObservableCollection<Contact>(await this.ViewContactsUseCase.ExecuteAsync(((SearchBar)sender).Text));
        listContacts.ItemsSource = contacts;

    }

}