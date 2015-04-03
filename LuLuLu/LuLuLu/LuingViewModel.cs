using System;
using System.ComponentModel;

namespace LuLuLu
{
	public class LuingViewModel: INotifyPropertyChanged
	{
		#region INotifyPropertyChanged implementation

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion

		int count;
		
		public int Count
		{
			get {
				return count;
			}
			set {
				if (count != value) {
					count = value;

					if (PropertyChanged != null)
					{
						PropertyChanged(this, 
							new PropertyChangedEventArgs("Count"));
					}
				}
			}
		}

		public LuingViewModel ()
		{
		}
	}
}

