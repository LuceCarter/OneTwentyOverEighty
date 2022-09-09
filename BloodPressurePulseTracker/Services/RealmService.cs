using Android.Preferences;

namespace BloodPressurePulseTracker.Services;
public static class RealmService
{
    public static Realm GetRealm() => Realm.GetInstance();
}
