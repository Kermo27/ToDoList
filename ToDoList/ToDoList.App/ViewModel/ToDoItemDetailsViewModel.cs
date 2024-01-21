using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace ToDoList.App
{
	[QueryProperty(nameof(Item), nameof(Item))]
	public class ToDoItemDetailsViewModel : INotifyPropertyChanged
	{
		private readonly ToDoItemDatabase _database;
		private ToDoItem _item;
		public ToDoItem Item
		{
			get
			{
				return _item;
			}
			set
			{
				if (_item == value) return;

				_item = value;
				OnPropertyChanged(nameof(Item));
			}
		}

		public event PropertyChangedEventHandler? PropertyChanged;
		private void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public ICommand UpdateItemCommand { get; set; }
		public ICommand DeleteItemCommand { get; set; }

		public ToDoItemDetailsViewModel(ToDoItemDatabase database)
		{
			_database = database;
			UpdateItemCommand = new Command(UpdateItem);
			DeleteItemCommand = new Command(DeleteItem);
		}

		private void UpdateItem()
		{
			Item.TaskDate = DateTime.Now;

			_database.SaveItemAsync(new ToDoItemDto
			{
				Id = Item.ID,
				Title = Item.Title,
				Description = Item.Description,
				TaskDate = Item.TaskDate,
				IsChecked = Item.IsChecked
			});
		}

		private async void DeleteItem()
		{
			await _database.DeleteItemAsync(Item.ID);
			await Shell.Current.GoToAsync("..");
		}
	}
}
