﻿using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Hardware;
using Android.Content;

namespace LuLuLu.Droid
{
	[Activity(Label = "LuLuLu", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
	{		
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
			global::Xamarin.Forms.Forms.Init(this, bundle);
			LoadApplication(new LuLuLu.App());
		}
	}
}

