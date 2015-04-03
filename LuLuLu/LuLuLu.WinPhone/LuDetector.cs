using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Sensors;

namespace LuLuLu.WinPhone
{
	class LuDetector:ILuDetector
	{
		Accelerometer accelerometer;
		const float MinMagnitutePerLu = 0.6f;

		float currentMagnitude = 0f;
		float totalMagnitude = 0f;
		int peekCount;

		public LuDetector ()
		{
			accelerometer = Accelerometer.GetDefault();
		}

		public void Start()
		{
			accelerometer.ReadingChanged += accelerometer_ReadingChanged;
			IsDetecting = true;
		}

		void accelerometer_ReadingChanged(Accelerometer sender, AccelerometerReadingChangedEventArgs args)
		{
			float y = (float)args.Reading.AccelerationY;

			if (y * SensorY < 0f)
				currentMagnitude = 0f;

			if (currentMagnitude < MinMagnitutePerLu && currentMagnitude + Math.Abs(y) >= MinMagnitutePerLu)
			{

				peekCount++;

				if (peekCount % 2 == 0)
				{
					if (Tick != null)
					{
						Tick(this, EventArgs.Empty);
					}
				}
			}

			currentMagnitude += Math.Abs(y);
			totalMagnitude += Math.Abs(y);

			SensorX = (float)args.Reading.AccelerationX;
			SensorY = (float)args.Reading.AccelerationY;
			SensorZ = (float)args.Reading.AccelerationZ;

		}

		public void Stop()
		{
			accelerometer.ReadingChanged -= accelerometer_ReadingChanged;
			IsDetecting = false;
		}

		public float SensorX
		{
			get;
			private set;
		}

		public float SensorY
		{
			get;
			private set;
		}

		public float SensorZ
		{
			get;
			private set;
		}

		public event EventHandler Tick;

		public bool IsDetecting
		{ get; private set; }
	}
}
