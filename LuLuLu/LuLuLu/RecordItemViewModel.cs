using System;

namespace LuLuLu
{
	/// <summary>
	/// Record item view model.Used by MainPage's list
	/// </summary>
	public class RecordItemViewModel
	{
		public int Count {get;set;}
		public string Description {get;set;}

		public RecordItemViewModel ()
		{
		}

		public RecordItemViewModel (Record rec)
		{
			Count = rec.Count;
			Description = string.Format ("Speed : {0}/min , Duration:{1} seconds", (rec.Count/rec.Duration.TotalMinutes).ToString("F1") , rec.Duration.TotalSeconds.ToString ("F0"));
		}
	}
}

