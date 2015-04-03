using System;
using Android.Hardware;


using Android;
using Android.Content;
using Xamarin.Forms;


namespace LuLuLu.Droid
{
	public class LuDetector:Java.Lang.Object, ILuDetector,ISensorEventListener  
	{
		readonly SensorManager sensorManager;
		const float MinMagnitutePerLu = 25f;

		float currentMagnitude = 0f;
		float totalMagnitude = 0f;
		int peekCount;
		
		public LuDetector ()
		{
			var ctx = Forms.Context; // useful for many Android SDK features
			sensorManager = (SensorManager)ctx.GetSystemService(Context.SensorService);
		}

		#region ISensorEventListener implementation

		public void OnAccuracyChanged (Sensor sensor, SensorStatus accuracy)
		{
			
		}

		public void OnSensorChanged (SensorEvent e)
		{
			float y = e.Values [1];

			if (y * SensorY < 0f)
				currentMagnitude = 0f;

			if (currentMagnitude < MinMagnitutePerLu && currentMagnitude + Math.Abs (y) >= MinMagnitutePerLu) {
				
				peekCount++;

				if (peekCount % 2 == 0) {
					if (Tick != null) {
						Tick (this, EventArgs.Empty);
					}
				}
			}

			currentMagnitude += Math.Abs(y);
			totalMagnitude += Math.Abs(y);

			SensorX = e.Values [0];
			SensorY = e.Values [1];
			SensorZ = e.Values [2];

		}

		#endregion

		#region ILuDetector implementation

		public event EventHandler Tick;

		public void Start ()
		{
			sensorManager.RegisterListener (this, sensorManager.GetDefaultSensor (SensorType.Accelerometer), SensorDelay.Normal);
			IsDetecting = true;
		}

		public void Stop ()
		{
			sensorManager.UnregisterListener (this);
			IsDetecting = false;
		}

		public float SensorX {
			get;
			private set;
		}

		public float SensorY {
			get;
			private set;
		}

		public float SensorZ {
			get;
			private set;
		}
		public bool IsDetecting
		{ get; private set; }

		#endregion
	}
}

