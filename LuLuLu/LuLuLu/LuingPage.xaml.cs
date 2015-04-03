using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace LuLuLu
{
	public partial class LuingPage : ContentPage
	{
		
		const float RecordInterval = 30;

		ILuDetector detector;
		Record record;
		List<RecordPoint> points;
		float currentTime = 0;
		LuingViewModel vm;
		
		public LuingPage ()
		{
			InitializeComponent ();
		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();

			var app = Application.Current as App;
			if (app != null)
			{
				detector = app.Detector;
			}
			vm = new LuingViewModel();
			countLabel.BindingContext = vm;

			if (detector!= null) {
				detector.Tick += Detector_Tick;

				detector.Start ();
			}

			record = new Record ()
			{
				StartDateTime = DateTime.Now
			};
			points = new List<RecordPoint> (512);
			Device.StartTimer (TimeSpan.FromMilliseconds (RecordInterval), DoRecord);
		}

		protected override void OnDisappearing ()
		{
			base.OnDisappearing ();
			if (detector!= null) {
				detector.Stop ();

				detector.Tick -= Detector_Tick;
			}

			//Save record
			record.Count = vm.Count;
			record.Duration = DateTime.Now - record.StartDateTime;
			DataRepository.Insert (record, points.AsReadOnly());

		}

		void Detector_Tick (object sender, EventArgs e)
		{
			vm.Count++;
		}

		bool DoRecord()
		{
			currentTime += RecordInterval;

			if (detector != null && detector.IsDetecting) {
				points.Add(
					new RecordPoint()
					{
						SensorX = detector.SensorX,
						SensorY = 	detector.SensorY,
						SensorZ =  detector.SensorZ,
						Time = currentTime
					}
				);
			}

			return IsVisible;
		}

		protected override bool OnBackButtonPressed ()
		{
			Navigation.PopModalAsync ();
			return base.OnBackButtonPressed ();
		}

		void OnBackButtonClicked(object sender,EventArgs args)
		{
			OnBackButtonPressed ();
		}
	}
}
