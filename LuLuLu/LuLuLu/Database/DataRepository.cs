using System;
using System.IO;
using System.Collections.Generic;
using SQLite;
using System.Threading.Tasks;

namespace LuLuLu
{
	public static class DataRepository
	{
		private const string DatabaseName = "lululu.db";

		static string DatabasePath {
			get { 
				#if __IOS__
				string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal); // Documents folder
				string libraryPath = Path.Combine (documentsPath, "..", "Library"); // Library folder
				var path = Path.Combine(libraryPath, DatabaseName);
				#elif __ANDROID__
				string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal); // Documents folder
				var path = Path.Combine(documentsPath, DatabaseName);
				#elif WINDOWS_PHONE
				var path = Path.Combine(ApplicationData.Current.LocalFolder.Path, DatabaseName);
				#endif
				return path;
			}
		}

		private static readonly SQLiteAsyncConnection conn;
		
		static DataRepository()
		{
			conn = new SQLiteAsyncConnection (DatabasePath);
			conn.CreateTablesAsync<Record,RecordPoint> ();
		}

		public static AsyncTableQuery<Record> GetRecord(RecordMode mode)
		{
			var recMode = (int)mode;
			return from r in conn.Table<Record>()
					where r.RecordMode == recMode
				select r;			
		}

		public static AsyncTableQuery<RecordPoint> GetRecordData(Record rec)
		{
			return from r in conn.Table<RecordPoint>()
					where r.RecordId == rec.Id
					select r;			
		}

		public static Task Insert(Record rec,IEnumerable<RecordPoint> points)
		{
			return conn.RunInTransactionAsync (new Action<SQLiteConnection>((connection) =>   {
				connection.Insert(rec);
				foreach (var p in points) {
					p.RecordId = rec.Id;
					connection.Insert(p);
				}
			}));
		}

		public static Task Delete(Record rec)
		{
			return conn.RunInTransactionAsync (new Action<SQLiteConnection>((connection) =>   {
				connection.Execute("DELETE FROM RecordPoint where RecordId = ? ",rec.Id);
				connection.Delete(rec);
			}));
		}

	}
}

