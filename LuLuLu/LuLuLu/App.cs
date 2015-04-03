using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace LuLuLu
{
	public class App : Application
	{
		public ILuDetector Detector {
			get;
			private set;
		}

		public App ()
		{
			// The root page of your application

			MainPage = new LuLuLu.MainPage ();

		}

		protected override void OnStart ()
		{
			// Handle when your app starts
			Detector = DependencyService.Get<ILuDetector>();
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
