using Android.Preferences;

namespace BloodPressurePulseTracker.Services;
public static class RealmService
{
    public static Realm GetRealm() => Realm.GetInstance();
    public static Task<Realm> GetRealm(PartitionSyncConfiguration config)
    {
        return Realm.GetInstanceAsync(config);
    }
}