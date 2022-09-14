using BloodPressurePulseTracker.Config;
using BloodPressurePulseTracker.Models;
using BloodPressurePulseTracker.Services;
using CommunityToolkit.Mvvm.Input;
using User = BloodPressurePulseTracker.Models.User;

namespace BloodPressurePulseTracker.ViewModels
{
    public partial class LogReadingPageViewModel : ObservableObject
    {
        private Realms.Sync.App app;
        private Realm realm;
        private PartitionSyncConfiguration config;
        private User user;
        

        [ObservableProperty]
        public string systolic;

        [ObservableProperty]
        public string diastolic;

        [ObservableProperty]
        public string pulse;

        [RelayCommand]
        public async Task LogReading()
        {
           try
            {
                var currentTime = DateTime.Now.ToShortTimeString();
                var timeOfDay = "AM";
                int systolicInt;
                int diastolicInt;
                int pulseInt;

                systolicInt = Int32.Parse(Systolic);
                diastolicInt = Int32.Parse(Diastolic);
                pulseInt = Int32.Parse(Pulse);

                if (currentTime.ToLower().Contains("pm"))
                {
                    timeOfDay = "PM";
                }

                app = Realms.Sync.App.Create(AppConfig.RealmAppId);
                config = new PartitionSyncConfiguration($"{app.CurrentUser.Id}", app.CurrentUser);
                realm = await Realm.GetInstanceAsync(config);
                user = realm.Find<User>(app.CurrentUser.Id);

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

                var reading = new BloodPressureReading
                {
                    Date = DateTimeOffset.Parse(currentTime),
                    TimeOfDay = timeOfDay,
                    Systolic = systolicInt,
                    Diastolic = diastolicInt,
                    Pulse = pulseInt,
                    PartitionKey = user.Id
                };
              

                realm.Write(() =>
                {
                    realm.Add(reading);
                });

              
            } catch(Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            } finally
            {
                Systolic = "";
                Diastolic = "";
                Pulse = "";
            }
        }

    }
}
