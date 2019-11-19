using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Diagnostics;
using System.Windows;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace WindowsNotification
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// //https://www.thomasclaudiushuber.com/2019/04/23/net-core-3-use-uwp-controls-in-wpf-with-xaml-islands/
    /// https://blogs.windows.com/windowsdeveloper/2017/01/25/calling-windows-10-apis-desktop-application/
    /// https://www.thomasclaudiushuber.com/2019/04/26/calling-windows-10-apis-from-your-wpf-application/
    public partial class MainWindow : Window
    {
        private const String APP_ID = "WindowsNotification";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string title = "The current time is";
            string timeString = $"{DateTime.Now:HH:mm:ss}";

            //    string toastXmlString =
            //    $@"<toast><visual>
            //    <binding template='ToastGeneric'>
            //    <text>{title}</text>
            //    <text>{timeString}</text>
            //    </binding>
            //</visual></toast>";

            //https://docs.microsoft.com/en-us/windows/uwp/design/shell/tiles-and-notifications/send-local-toast-desktop

            //https://docs.microsoft.com/en-us/windows/uwp/design/shell/tiles-and-notifications/adaptive-interactive-toasts
            ToastContent toastContent = new ToastContent()
            {
                // Arguments when the user taps body of toast
                Launch = "action=viewConversation&conversationId=5",
                DisplayTimestamp = new DateTime(2017, 04, 15, 19, 45, 00, DateTimeKind.Utc),

                Visual = new ToastVisual()
                {
                    BindingGeneric = new ToastBindingGeneric()
                    {

                        AppLogoOverride = new ToastGenericAppLogo()
                        {
                            Source = "https://picsum.photos/48?image=883",
                            HintCrop = ToastGenericAppLogoCrop.Circle
                        },
                        Children =
                        {
                            new AdaptiveText()
                            {
                                Text = "Adaptive Tiles Meeting",
                                HintMaxLines = 1
                            },

                            new AdaptiveText()
                            {
                                Text = "Conf Room 2001 / Building 135"
                            },

                            new AdaptiveText()
                            {
                                Text = "10:00 AM - 10:30 AM"
                            }
                        }
                    }
                }
            };

            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(toastContent.GetContent());

            var toastNotification = new ToastNotification(xmlDoc);

            toastNotification.Activated += ToastActivated;
            toastNotification.Dismissed += ToastDismissed;
            toastNotification.Failed += ToastFailed;
            var toastNotifier = ToastNotificationManager.CreateToastNotifier();
            toastNotifier.Show(toastNotification);
        }

        private void ToastFailed(ToastNotification sender, ToastFailedEventArgs args)
        {
            Debug.WriteLine("hello");
        }

        private void ToastDismissed(ToastNotification sender, ToastDismissedEventArgs args)
        {
            Debug.WriteLine("hello");
        }

        private void ToastActivated(ToastNotification sender, object args)
        {
            Debug.WriteLine("hi");
        }
    }
}