﻿using System;
using System.Threading.Tasks;
using Android.OS;
using Android.App;
using Android.Content;
using Android.Util;
using Plugin.LocalNotifications;
using Plugin.Geofencing;
using Android.Telephony;
using System.Collections.Generic;

namespace CrossPlatform.Demo.Droid
{
    [Service]
    class PeriodicBackgroundService : Service
    {
        private const string Tag = "[PeriodicBackgroundService]";

        private bool _isRunning;
        private Context _context;
        private Task _task;

        #region overrides

        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        public override void OnCreate()
        {
            _context = this;
            _isRunning = false;
            _task = new Task(DoWork);
        }

        public override void OnDestroy()
        {
            _isRunning = false;

            if (_task != null && _task.Status == TaskStatus.RanToCompletion)
            {
                _task.Dispose();
            }
        }

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            if (!_isRunning)
            {
                _isRunning = true;
                _task.Start();
            }
            return StartCommandResult.Sticky;
        }

        #endregion

        private void DoWork()
        {
            try
            {
                Log.WriteLine(LogPriority.Info, Tag, "Started!");

                // Do something...
                CrossLocalNotifications.Current.Show("Invoked at", $"{DateTime.Now}");
            }
            catch (Exception e)
            {
                Log.WriteLine(LogPriority.Error, Tag, e.ToString());
            }
            finally
            {
                StopSelf();
            }
        }
    }
}
