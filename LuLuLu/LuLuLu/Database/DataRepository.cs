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
				var path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, DatabaseName);
				#endif
				return path;
			}
		}

		private static readonly SQLiteAsyncConnection conn;
		private static Task createTask;

		public static event EventHandler DatabaseUpdated;
		
		static DataRepository()
		{
			conn = new SQLiteAsyncConnection (DatabasePath);
			createTask = conn.CreateTablesAsync<Record, RecordPoint>();
		}

		public static async Task<AsyncTableQuery<Record>> GetRecord(RecordMode mode)
		{
			await createTask;
			return from r in conn.Table<Record>()
					where r.Mode == mode
				select r;			
		}

		public static async Task<AsyncTableQuery<RecordPoint>> GetRecordData(Record rec)
		{
			await createTask;
			return from r in conn.Table<RecordPoint>()
					where r.RecordId == rec.Id
					select r;			
		}

		public static async Task Insert(Record rec,IEnumerable<RecordPoint> points)
		{
			await conn.RunInTransactionAsync (new Action<SQLiteConnection>((connection) =>   {
				connection.Insert(rec);
				foreach (var p in points) {
					p.RecordId = rec.Id;
					connection.Insert(p);
				}
			}));

			if (DatabaseUpdated != null) {
				DatabaseUpdated (null, EventArgs.Empty);
			}
		}

		public static async Task Delete(Record rec)
		{
			await conn.RunInTransactionAsync (new Action<SQLiteConnection>((connection) =>   {
				connection.Execute("DELETE FROM RecordPoint where RecordId = ? ",rec.Id);
				connection.Delete(rec);
			}));

			if (DatabaseUpdated != null) {
				DatabaseUpdated (null, EventArgs.Empty);
			}
		}

	}
}

