using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace ToDoList.App.ViewModel
{
	public partial class ToDoViewModel : ObservableObject
	{
		private readonly ToDoItemDatabase _database;

		[ObservableProperty]
		private ObservableCollection<ToDoItem> _toDoItems = new ObservableCollection<ToDoItem>();

		public string Title { get; set; }
		public string Description { get; set; }
		public string TaskDate = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
		public bool IsChecked { get; set; }

		public ToDoViewModel()
		{
			_database = new ToDoItemDatabase();
		}

		public async void OnAppearing()
		{
			ToDoItems.Clear();

			var toDoItems = await _database.GetItemsAsync();

			toDoItems = toDoItems.OrderBy(item => item.TaskDate).ToList();

			foreach (var item in toDoItems)
			{
				ToDoItems.Add(new ToDoItem
				{
					ID = item.Id,
					Title = item.Title,
					Description = item.Description,
					TaskDate = item.TaskDate,
					IsChecked = item.IsChecked
				});
			}
		}

		[RelayCommand]
		private async Task AddItem()
		{
			ToDoItem item = new ToDoItem();
			item.Title = Title;
			item.Description = Description;
			item.TaskDate = DateTime.Now;

			ToDoItemDto toDoItemDto = new ToDoItemDto
			{
				Title = item.Title,
				Description = item.Description,
				TaskDate = item.TaskDate,
				IsChecked = item.IsChecked
			};

			ToDoItems.Add(item);
			await _database.SaveItemAsync(toDoItemDto);
			item.ID = toDoItemDto.Id;
		}

		[RelayCommand]
		private Task UpdateItem(ToDoItem item)
		{
			item.Title = Title;
			item.Description = Description;
			item.TaskDate = DateTime.Now;

			ToDoItems = new ObservableCollection<ToDoItem>(ToDoItems.OrderBy(i => i.TaskDate));

			OnPropertyChanged(nameof(ToDoItems));

			return _database.SaveItemAsync(new ToDoItemDto
			{
				Id = item.ID,
				Title = item.Title,
				Description = item.Description,
				TaskDate = item.TaskDate,
				IsChecked = item.IsChecked
			});
		}

		[RelayCommand]
		private Task DeleteItem(int id)
		{
			var item = ToDoItems.FirstOrDefault(i => i.ID == id);
			if (item is not null)
			{
				ToDoItems.Remove(item);
			}
			return _database.DeleteItemAsync(id);
		}

		[RelayCommand]
		private Task OnCheckBoxCheckedChanged(ToDoItem item)
		{
			return _database.SaveItemAsync(new ToDoItemDto
			{
				Id = item.ID,
				Title = item.Title,
				Description = item.Description,
				TaskDate = item.TaskDate,
				IsChecked = item.IsChecked
			});
		}

		[RelayCommand]
		private Task CollectionViewSelectionChanged(ToDoItem item)
		{
			if (item != null)
			{
				return Shell.Current.GoToAsync(nameof(ToDoItemDetails), new Dictionary<string, object>
			{
				{ nameof(ToDoItemDetailsViewModel.Item), item }
			});
			}

			return Task.CompletedTask;
		}
	}
}
