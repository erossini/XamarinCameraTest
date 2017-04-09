﻿using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using Xamarin.Forms;

namespace cameratest.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init();

			LoadApplication(new App());

			return base.FinishedLaunching(app, options);
		}

		public override void ReceiveMemoryWarning(UIApplication application)
		{
			// this (MemoryWarningsHandler) is a helper that I created 
			// to capture more info when a memory warning is raised. Things like (nav Stack, running time, etc)
			GC.Collect();
			Console.WriteLine("Memory Warning!");
			MessagingCenter.Send(new MemoryMessage(), "MemoryMessage");
		}
	}
}