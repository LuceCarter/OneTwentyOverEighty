


using CommunityToolkit.Mvvm.Input;
using Realms.Sync;

namespace BloodPressurePulseTracker.ViewModels
{
    public partial class LoginPageViewModel : ObservableObject
    {
        private User user;
 

        [ObservableProperty]
        public string username;

        [ObservableProperty]
        public string password;


        [RelayCommand]
        public async Task Login()
        {
            if (user != null)
            {
                await AppShell.Current.GoToAsync("///Main");
            }

            else
            {
                try
                {
                    user = await App.RealmApp.LogInAsync(Credentials.EmailPassword(Username, Password));

                    if (user != null)
                    {
                        await AppShell.Current.GoToAsync("///Main");
                        Username = "";
                        Password = "";
                    
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                catch (Exception ex)
                {
                    await App.Current.MainPage.DisplayAlert("Error logging in!", "Error: " + ex.Message, "OK");
                }
            }
        }

        [RelayCommand]
        private async Task CreateAccount()
        {
            try
            {

                await App.RealmApp.EmailPasswordAuth.RegisterUserAsync(Username, Password);

                await Login();

            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error creating account!", "Error: " + ex.Message, "OK");
            }
        }

        public bool CheckIsLoggedIn()
        {
            if (App.RealmApp.CurrentUser != null)
            {
                user = App.RealmApp.CurrentUser;

                if (user.State == UserState.LoggedIn)
                    return true;
            }

            return false;
        }
    }
}
