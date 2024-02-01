using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ToDoList.App
{
	[QueryProperty(nameof(Item), nameof(Item))]
	public partial class ToDoItemDetailsViewModel : ObservableObject
	{
		private readonly ToDoItemDatabase _database;

		[ObservableProperty]
		private ToDoItem _item;

		public ToDoItemDetailsViewModel(ToDoItemDatabase database)
		{
			_database = database;
		}

		[RelayCommand]
		private Task UpdateItem()
		{
			Item.TaskDate = DateTime.Now;

			return _database.SaveItemAsync(new ToDoItemDto
			{
				Id = Item.ID,
				Title = Item.Title,
				Description = Item.Description,
				TaskDate = Item.TaskDate,
				IsChecked = Item.IsChecked
			});
		}

		[RelayCommand]
		private async Task DeleteItem()
		{
			await _database.DeleteItemAsync(Item.ID);
			await Shell.Current.GoToAsync("..");
		}
	}
}
