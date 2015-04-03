using System;
using System.Collections.Generic;
using SQLite;

namespace LuLuLu
{
	public enum RecordMode
	{
		Speed = 0,
		Survival,
		Ghost
	}
	
	[Table("Record")]
	public class Record
	{
		[Column("ID"),PrimaryKey,AutoIncrement]
		public int Id {
			get;
			set;
		}

		[Column("Count"),NotNull]
		public int Count {
			get;
			set;
		}

		[Column("Duration"),NotNull]
		public TimeSpan Duration {
			get;
			set;
		}

		[Column("StartDateTime"),NotNull]
		public DateTime StartDateTime {
			get;
			set;
		}

		[Column("Mode"),NotNull]
		internal int RecordMode {
			get;
			set;
		}

		[Ignore]
		public RecordMode Mode {
			get	{
				return (RecordMode)RecordMode;
			}

			set {
				RecordMode = (int)value;
			}
				
		}

		//@ForeignCollectionField(eager = true,columnName = RecordPoint.RECORD_FK_COLUMN_NAME)
		//private ForeignCollection<RecordPoint> Entries;

		public Record ()
		{
		}
	}
}

