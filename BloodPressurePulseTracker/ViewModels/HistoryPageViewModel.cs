using BloodPressurePulseTracker.Models;
using BloodPressurePulseTracker.Services;
using System.Collections.ObjectModel;
using User = BloodPressurePulseTracker.Models.User;

namespace BloodPressurePulseTracker.ViewModels
{
    public partial class HistoryPageViewModel : ObservableObject
    {
        private User user;
        private Realm realm;
        private PartitionSyncConfiguration config;

        [ObservableProperty]
        public ObservableCollection<BloodPressureReading> bloodPressureReadings;

        public async Task InitializeRealm()
        {
            config = new PartitionSyncConfiguration($"{App.RealmApp.CurrentUser.Id}", App.RealmApp.CurrentUser);
            realm = await RealmService.GetRealm(config);
            user = realm.Find<User>(App.RealmApp.CurrentUser.Id);

            if (user == null)
            {
                await Task.Delay(5000);
                user = realm.Find<User>(App.RealmApp.CurrentUser.Id);

                if (user == null)
                {
                    Console.WriteLine("NO USER OBJECT: This error occurs if " +
                        "you do not have the trigger configured on the backend " +
                        "or when there is a network connectivity issue. See " +
                        "https://docs.mongodb.com/realm/tutorial/realm-app/#triggers");

                    await App.Current.MainPage.DisplayAlert("No User object",
                        "The User object for this user was not found on the server. " +
                        "If this is a new user acocunt, the backend trigger may not have completed, " +
                        "or the tirgger doesn't exist. Check your backend set up and logs.", "OK");
                }
            }

            if(user != null)
            {
                BloodPressureReadings = new ObservableCollection<BloodPressureReading>(realm.All<BloodPressureReading>());
                BloodPressureReadings.Reverse();
            }
        }

    }
}
