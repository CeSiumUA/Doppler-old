using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Messaging;

namespace Doppler.Droid.FireBaseService
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class FireBaseListenerService:FirebaseMessagingService
    {
        public override void OnMessageReceived(RemoteMessage p0)
        {
            base.OnMessageReceived(p0);

            RemoteMessage.Notification remoteNotification = p0.GetNotification();
            DisplayNotification(remoteNotification.Title, remoteNotification.Body);
        }

        private void DisplayNotification(string title, string text)
        {
            new Notifier().CreateNotification(title, text);
        }
    }
}