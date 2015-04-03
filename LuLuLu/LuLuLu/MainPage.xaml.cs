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

		protected override void OnAppearing ()
		{
			base.OnAppearing ();
			DataRepository.DatabaseUpdated += DataRepository_DatabaseUpdated;
			recordList.RefreshCommand = new Command (RefreshItemSource);

			if (recordList.ItemsSource == null)
			{
				recordList.BeginRefresh();
			}
		}

		protected override void OnDisappearing ()
		{
			base.OnDisappearing ();
			DataRepository.DatabaseUpdated -= DataRepository_DatabaseUpdated;
		}

		void DataRepository_DatabaseUpdated (object sender, EventArgs e)
		{
			recordList.BeginRefresh ();
		}



		private async void RefreshItemSource(object obj)
		{
			try {

				var recordQuery = await DataRepository.GetRecord (RecordMode.Default);
				var records = await recordQuery.ToListAsync();

				recordList.ItemsSource = records.Select (rec => new RecordItemViewModel (rec)).ToList().AsReadOnly();

				Console.WriteLine("[ListView] Refresh done");
			} catch (Exception ex) {
				Console.WriteLine ("[ListView] Refresh error: " + ex.Message);
			}
			finally {
				recordList.EndRefresh ();
			}

		}

		private void OnStartButtonClicked(object sender,EventArgs args)
		{
			Navigation.PushAsync (new NavigationPage(new LuingPage ()));
		}
	}
}
