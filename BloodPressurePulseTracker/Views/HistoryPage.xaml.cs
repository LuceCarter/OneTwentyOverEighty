using BloodPressurePulseTracker.ViewModels;

namespace BloodPressurePulseTracker.Views;

public partial class HistoryPage : ContentPage
{
    public HistoryPage()
    {
        InitializeComponent();

     }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var vm = new HistoryPageViewModel();
        await vm.InitializeRealm();
    }
}