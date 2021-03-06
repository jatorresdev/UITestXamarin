﻿using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace Contacts.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			// Initialize Azure Mobile Apps
			Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();
			global::Xamarin.Forms.Forms.Init();

			#if ENABLE_TEST_CLOUD
				Xamarin.Calabash.Start();
			#endif

			LoadApplication(new App());

			return base.FinishedLaunching(app, options);
		}
	}
}
