using SQLite;

namespace ToDoList.App
{
	public class ToDoItemDatabase
	{
		SQLiteAsyncConnection Database;

		public ToDoItemDatabase()
		{
		}

		async Task Init()
		{
			if (Database is not null)
			{
				return;
			}

			Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
			var result = await Database.CreateTableAsync<ToDoItemDto>();
		}

		public async Task<List<ToDoItemDto>> GetItemsAsync()
		{
			await Init();
			return await Database.Table<ToDoItemDto>().ToListAsync();
		}

		public async Task<List<ToDoItemDto>> GetItemsNotDoneAsync()
		{
			await Init();
			return await Database.Table<ToDoItemDto>().Where(t => !t.IsChecked).ToListAsync();
		}

		public async Task<ToDoItemDto> GetItemAsync(int id)
		{
			await Init();
			return await Database.Table<ToDoItemDto>().Where(i => i.Id == id).FirstOrDefaultAsync();
		}

		public async Task<int> SaveItemAsync(ToDoItemDto item)
		{
			await Init();
			if (item.Id != 0)
			{
				return await Database.UpdateAsync(item);
			}
			else
			{
				return await Database.InsertAsync(item);
			}
		}

		public async Task<int> DeleteItemAsync(int id)
		{
			await Init();
			return await Database.DeleteAsync<ToDoItemDto>(id);
		}

		public async Task<string> SaveItemImagePathAsync(int id, string imagePath)
		{
			await Init();

			var item = await Database.Table<ToDoItemDto>().Where(i => i.Id == id).FirstOrDefaultAsync();

			if (item != null)
			{
				item.ImagePath = imagePath;
				await Database.UpdateAsync(item);
			}

			return string.Empty;
		}

		public async Task<string> GetItemImagePathAsync(int id)
		{
			await Init();

			var item = await Database.Table<ToDoItemDto>().Where(i => i.Id == id).FirstOrDefaultAsync();

			if (item != null)
			{
				return item.ImagePath;
			}

			return string.Empty;
		}
	}
}
