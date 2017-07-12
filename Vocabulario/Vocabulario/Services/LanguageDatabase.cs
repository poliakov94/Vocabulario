using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Vocabulario.Models;

namespace Vocabulario.Services
{
	public class LanguageDatabase
	{
		readonly SQLiteAsyncConnection database;

		public LanguageDatabase(string dbPath)
		{
			database = new SQLiteAsyncConnection(dbPath);
			database.CreateTableAsync<Language>().Wait();
		}

		public Task<List<Language>> GetLanugagesAsync()
		{
			return database.Table<Language>().ToListAsync();
		}

		public Task<Language> GetLanguageAsync(int ID)
		{
			return database.Table<Language>().Where(i => i.ID == ID).FirstOrDefaultAsync();
		}

		public Task<int> SaveLanguageAsync(Language language)
		{
			if (language.ID != 0)
				return database.UpdateAsync(language);
			else
				return database.InsertAsync(language);
		}

		public Task<int> DeleteLanguageAsync(Language language)
		{
			return database.DeleteAsync(language);
		}

		public Task<int> ExecuteQuery(string query, object[] args)
		{
			return database.ExecuteAsync(query, args);
		}


	}
}
