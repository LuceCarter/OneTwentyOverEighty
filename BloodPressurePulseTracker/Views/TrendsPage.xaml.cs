using System;
using System.Collections.Generic;
using BloodPressurePulseTracker.ViewModels;


namespace BloodPressurePulseTracker.Views;

public partial class TrendsPage : ContentPage
{

    public TrendsPage()
    {
        InitializeComponent();

        chartView.Source = "https://charts.mongodb.com/charts-luce-wkuif/embed/dashboards?id=61e9bf04-2312-4555-8091-4ae3aaf24d2d&theme=light&autoRefresh=true&maxDataAge=3600&showTitleAndDesc=false&scalingWidth=scale&scalingHeight=scale";
        Content = chartView;

    }
}
