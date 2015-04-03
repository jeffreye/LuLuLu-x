using System;

namespace LuLuLu
{
	public interface ILuDetector
	{
		bool IsDetecting
		{ get; }

		void Start();
		void Stop();

		float SensorX{ get; }
		float SensorY{ get; }
		float SensorZ{ get; }

		event EventHandler Tick;
	}
}

