namespace BloodPressurePulseTracker;

public partial class App : Application
{
    public static Realms.Sync.App RealmApp;
    public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	}

    protected override void OnStart()
    {
        RealmApp = Realms.Sync.App.Create("<YOUR REALM APP ID HERE>");
    }
}
