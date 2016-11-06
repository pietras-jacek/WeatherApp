using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using System.Threading.Tasks;
using Windows.UI.ViewManagement;
using NotificationsExtensions.ToastContent;
using Windows.UI.Notifications;

namespace WeatherApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            
            ApplicationView.PreferredLaunchViewSize = new Size(770, 550);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;

            IToastImageAndText02 trying_toast = ToastContentFactory.CreateToastImageAndText02();
            trying_toast.TextHeading.Text = "Aplikacja pogodowa załadowana poprawnie";
            trying_toast.TextBodyWrap.Text = "WeatherApp";
            ScheduledToastNotification giveittime;
            giveittime = new ScheduledToastNotification(trying_toast.GetXml(), DateTime.Now.AddSeconds(2));
            giveittime.Id = "Any_ID";
            ToastNotificationManager.CreateToastNotifier().AddToSchedule(giveittime);

        }
    }

    
}
