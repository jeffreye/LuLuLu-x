using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using CoreMotion;

namespace LuLuLu.iOS
{
	class LuDetector:ILuDetector
	{
		CMMotionManager motionManager;
		const float MinMagnitutePerLu = 5f;

		float currentMagnitude = 0f;
		float totalMagnitude = 0f;
		int peekCount;

		public LuDetector()
		{
			motionManager = new CMMotionManager();
		}

		public void Start()
		{
			motionManager.StartAccelerometerUpdates(NSOperationQueue.CurrentQueue, OnAccelerometerUpdated);
			IsDetecting = true;
		}

		public void Stop()
		{
			motionManager.StopAccelerometerUpdates();
			IsDetecting = false;
		}

		private void OnAccelerometerUpdated(CMAccelerometerData data, NSError error)
		{

			float y = (float)data.Acceleration.Y;

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

			SensorX = (float)data.Acceleration.X;
			SensorY = (float)data.Acceleration.Y;
			SensorZ = (float)data.Acceleration.Z;
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