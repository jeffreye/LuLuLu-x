using System;
using SQLite;

namespace LuLuLu
{
	[Table("RecordPoint")]
	public class RecordPoint
	{
		[Column("ID"),PrimaryKey,AutoIncrement]
		public int Id {
			get;
			set;
		}

		//@DatabaseField(canBeNull = false,foreign = true,foreignAutoRefresh = true,columnDefinition =RECORD_FK_COLUMN_NAME)
		//private Record Group;
		[Column("RecordId"),NotNull]
		public int RecordId{
			get;
			set;
		}

		[Column("SensorX"),NotNull]
		public float SensorX{
			get;
			set;
		}

		[Column("SensorY"),NotNull]
		public float SensorY{
			get;
			set;
		}

		[Column("SensorZ"),NotNull]
		public float SensorZ{
			get;
			set;
		}

		[Column("Time"),NotNull]
		public float Time{
			get;
			set;
		}
		
		public RecordPoint ()
		{
			
		}
	}
}

