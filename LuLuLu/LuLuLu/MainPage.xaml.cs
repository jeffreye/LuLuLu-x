using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace LuLuLu
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent ();
		}

		private void OnStartButtonClicked(object sender,EventArgs args)
		{
			Navigation.PushModalAsync (new LuingPage ());
		}
	}
}
