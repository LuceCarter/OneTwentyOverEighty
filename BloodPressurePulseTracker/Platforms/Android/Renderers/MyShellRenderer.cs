using Android.Content;
using BloodPressurePulseTracker;
using BloodPressurePulseTracker.Platforms.Android.Renderers;
using Google.Android.Material.BottomNavigation;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls.Handlers.Compatibility;
using Microsoft.Maui.Controls.Platform.Compatibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: ExportRenderer(typeof(AppShell), typeof(MyShellRenderer))]
namespace BloodPressurePulseTracker.Platforms.Android.Renderers
{
    public class MyShellRenderer : ShellRenderer
    {
        public MyShellRenderer(Context context) : base(context)
        {
        }

        protected override IShellBottomNavViewAppearanceTracker CreateBottomNavViewAppearanceTracker(ShellItem shellItem)
        {
            return new CustomBottomNavAppearanceTracker(this);
        }

        internal class CustomBottomNavAppearanceTracker : IShellBottomNavViewAppearanceTracker
        {

            private MyShellRenderer myShellRenderer = null;

            public CustomBottomNavAppearanceTracker(MyShellRenderer myShellRenderer)
            {
                this.myShellRenderer = myShellRenderer;
            }

            public void Dispose()
            {
                //throw new System.NotImplementedException();
            }

            public void ResetAppearance(BottomNavigationView bottomView)
            {
                bottomView.ItemIconTintList = null;
            }

            public void SetAppearance(BottomNavigationView bottomView, ShellAppearance appearance)
            {
                bottomView.ItemIconTintList = null;
            }

            public void SetAppearance(BottomNavigationView bottomView, IShellAppearanceElement appearance)
            {
                bottomView.ItemIconTintList = null;
            }
        }
    }
}
