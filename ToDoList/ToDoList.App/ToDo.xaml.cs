using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ToDoList.App;

public partial class ToDo : ContentPage
{
	private readonly ToDoItemDatabase _database;
	public ObservableCollection<ToDoItem> ToDoItems { get; set; } = new ObservableCollection<ToDoItem>();
	public ICommand DeleteItemCommand { get; set; }
	public ICommand UpdateItemCommand { get; set; }
	public ICommand CheckedChangedCommand { get; set; }

	public string Title { get; set; }
	public string Description { get; set; }
	public string TaskDate = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
	public bool IsChecked { get; set; }

	public ToDo()
	{
		InitializeComponent();
		BindingContext = this;
		DeleteItemCommand = new Command<int>(DeleteItem);
		UpdateItemCommand = new Command<ToDoItem>(UpdateItem);
		CheckedChangedCommand = new Command<ToDoItem>(OnCheckBoxCheckedChanged);
		_database = new ToDoItemDatabase();
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();
		var toDoItems = await _database.GetItemsAsync();
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

	private void AddItem(object sender, EventArgs e)
	{
		ToDoItem item = new ToDoItem();
		item.Title = Title;
		item.Description = Description;
		item.TaskDate = DateTime.Now;

		ToDoItems.Add(item);
		_database.SaveItemAsync(new ToDoItemDto
		{
			Title = item.Title,
			Description = item.Description,
			TaskDate = item.TaskDate,
			IsChecked = item.IsChecked
		});
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
}