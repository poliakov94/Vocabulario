using System.Collections.Generic;
using System.Threading.Tasks;
using Vocabulario.Models;

namespace Vocabulario.Services
{
	public interface IDataStore<T>
	{
		Task<bool> AddLanguageAsync(T language);
		Task<bool> UpdateLanguageAsync(T language);
		Task<bool> DeleteLanguageAsync(T language);
		Task<T> GetLanguageAsync(int id);
		Task<IEnumerable<T>> GetLanguagesAsync(bool forceRefresh = false);
		Task<IEnumerable<Rank>> GetRanksAsync(T language);

		Task InitializeAsync();
		Task<bool> PullLatestAsync();
		Task<bool> SyncAsync();
	}
}
