using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace ToDoList.App.ViewModel
{
	public class ToDoViewModel : INotifyPropertyChanged
	{
		private readonly ToDoItemDatabase _database;

		public ObservableCollection<ToDoItem> ToDoItems { get; set; } = new ObservableCollection<ToDoItem>();

		public ICommand AddItemCommand { get; set; }
		public ICommand DeleteItemCommand { get; set; }
		public ICommand UpdateItemCommand { get; set; }
		public ICommand CheckedChangedCommand { get; set; }
		public ICommand CollectionViewSelectionChangedCommand { get; set; }

		public string Title { get; set; }
		public string Description { get; set; }
		public string TaskDate = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
		public bool IsChecked { get; set; }
		public object SelectedItem { get; set; }

		public ToDoViewModel()
		{
			AddItemCommand = new Command(AddItem);
			DeleteItemCommand = new Command<int>(DeleteItem);
			UpdateItemCommand = new Command<ToDoItem>(UpdateItem);
			CheckedChangedCommand = new Command<ToDoItem>(OnCheckBoxCheckedChanged);
			CollectionViewSelectionChangedCommand = new Command(CollectionViewSelectionChanged);
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

		private async void AddItem()
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

		private void UpdateItem(ToDoItem item)
		{
			item.Title = Title;
			item.Description = Description;
			item.TaskDate = DateTime.Now;

			_database.SaveItemAsync(new ToDoItemDto
			{
				Id = item.ID,
				Title = item.Title,
				Description = item.Description,
				TaskDate = item.TaskDate,
				IsChecked = item.IsChecked
			});

			ToDoItems = new ObservableCollection<ToDoItem>(ToDoItems.OrderBy(i => i.TaskDate));

			OnPropertyChanged(nameof(ToDoItems));
		}

		private void DeleteItem(int id)
		{
			_database.DeleteItemAsync(id);
			var item = ToDoItems.FirstOrDefault(i => i.ID == id);
			if (item is not null)
			{
				ToDoItems.Remove(item);
			}
		}

		private void OnCheckBoxCheckedChanged(ToDoItem item)
		{
			_database.SaveItemAsync(new ToDoItemDto
			{
				Id = item.ID,
				Title = item.Title,
				Description = item.Description,
				TaskDate = item.TaskDate,
				IsChecked = item.IsChecked
			});
		}

		private async void CollectionViewSelectionChanged()
		{
			if (SelectedItem != null)
			{
				await Shell.Current.GoToAsync(nameof(ToDoItemDetails), new Dictionary<string, object>
			{
				{ nameof(ToDoItemDetailsViewModel.Item), SelectedItem }
			});
			}

			SelectedItem = null;
		}

		public event PropertyChangedEventHandler? PropertyChanged;
		private void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
